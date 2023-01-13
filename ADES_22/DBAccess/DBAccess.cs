using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADES_22.Model;
using Org.BouncyCastle.Crypto.IO;

namespace ADES_22.DBAccess
{
    public class DBAccess
    {
        public static string[] formats = new string[] { "d/MM/yyyy", "d/M/yy", "dd/M/yyyy", "dd-MM-yy", "dd/MM/yy", "d-M-yy", "d-MM-yy", "d/M/yyyy", "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd", "DD/MM/yyyy", "dd/MMM/yyyy", "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy HH:mm", "dd-MM-yyyy", "dd-MMM-yyyy", "dd-MMM-yyyy HH:mm", "dd-MMM-yyyy HH:mm:ss", "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "dd-MM-yyyyTHH:mm:ss", "dd-MM-yyyyTHH:mm", "dd-MMM-yyyyTHH:mm", "dd-MMM-yyyyTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm" };

        #region ----- Login -----

        internal static string CheckLoginData(string username)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string passwd = "";

            try
            {
                cmd = new SqlCommand("select upassword from Employee_Information where Employeeid=@Employeeid", con);
                cmd.Parameters.AddWithValue("@Employeeid", username);
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        passwd = (sdr["upassword"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("CheckLoginData: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return HelperClass.DecodeFrom64(passwd);
        }

        internal static List<EmpDetails> CheckUserRole(EmpDetails inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<EmpDetails> list = new List<EmpDetails>();
            EmpDetails emp;

            try
            {
                cmd = new SqlCommand("select Department,Role,Name,Employeeid from Employee_Information where Employeeid=@Employeeid and upassword=@upassword", con);
                cmd.Parameters.AddWithValue("@Employeeid", inputtext.EmpID);
                cmd.Parameters.AddWithValue("@upassword", HelperClass.EncodePasswordToBase64(inputtext.Password));
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            emp = new EmpDetails();
                            emp.Role = sdr["Role"].ToString();
                            emp.Dept = sdr["Department"].ToString();
                            emp.EmpName = sdr["Name"].ToString();
                            emp.EmpID = sdr["Employeeid"].ToString();
                            list.Add(emp);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("CheckUserRole: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("CheckUserRole: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static string CheckUserID(string UserID)
        {
            string EmailID = string.Empty;

            try
            {
                SqlConnection con = ConnectionManager.GetConnection();
                SqlCommand cmd;

                cmd = new SqlCommand("select Email from Employee_Information where Employeeid=@Employeeid", con);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Employeeid", UserID);

                EmailID = (String)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("CheckUserID: " + ex.Message);
            }

            return EmailID;
        }

        internal static void ChangeUserPassword(string pass, string user)
        {
            try
            {
                SqlConnection con = ConnectionManager.GetConnection();
                SqlCommand cmd;

                cmd = new SqlCommand("update Employee_Information set upassword=@upassword where Employeeid=@Employeeid", con);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@upassword", HelperClass.EncodePasswordToBase64(pass));
                cmd.Parameters.AddWithValue("@Employeeid", user);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ChangeUserPassword: " + ex.Message);
            }
        }

        internal static void UpdateExpiryDate(string Param, DateTime dt, string UserID)
        {
            try
            {
                SqlConnection con = ConnectionManager.GetConnection();
                SqlCommand cmd;

                cmd = new SqlCommand("update Employee_Information set ExpiryDate=@ExpiryDate where Employeeid=@Employeeid", con);
                cmd.Parameters.Clear();

                if (Param == "ValidityFailed" || Param == "Updated")
                    cmd.Parameters.AddWithValue("@ExpiryDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ExpiryDate", dt);

                cmd.Parameters.AddWithValue("@Employeeid", UserID);

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("UpdateExpiryDate: " + ex.Message);
            }
        }

        internal static DateTime GetExpiryDateTime(string UserID)
        {
            DateTime dt = new DateTime();

            try
            {
                SqlConnection con = ConnectionManager.GetConnection();
                SqlCommand cmd;

                cmd = new SqlCommand("select ExpiryDate from Employee_Information where Employeeid=@Employeeid", con);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Employeeid", UserID);

                dt = Convert.ToDateTime(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetExpiryDateTime: " + ex.Message);
            }

            return dt;
        }

        #endregion

        #region ----- Employee Information -----

        internal static List<EmpDetails> GetEmpInformation(EmpDetails inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<EmpDetails> list = new List<EmpDetails>();
            EmpDetails empInfo;

            try
            {
                cmd = new SqlCommand("S_Get_EmployeeDetailsSaveUpdate", con);
                cmd.Parameters.AddWithValue("@empid", inputtext.EmpID);
                cmd.Parameters.AddWithValue("@Department", inputtext.Dept);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            empInfo = new EmpDetails();
                            empInfo.EmpID = sdr["Employeeid"].ToString();
                            empInfo.EmpName = sdr["Name"].ToString();
                            empInfo.Dept = sdr["Department"].ToString();
                            empInfo.Role = sdr["Role"].ToString();
                            empInfo.Email = sdr["Email"].ToString();
                            empInfo.Password = sdr["upassword"].ToString();
                            empInfo.ReportingTo = sdr["ReportingTo"].ToString();
                            empInfo.Region = sdr["Region"].ToString();
                            empInfo.Password.Equals("*");
                            list.Add(empInfo);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetEmpInformation: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetEmpInformation: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static int ExistsUserID(String UserID)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            int count = 0;

            try
            {
                cmd = new SqlCommand("select count(*) from Employee_Information where Employeeid=@Employeeid", con);
                cmd.Parameters.AddWithValue("@Employeeid", UserID);
                cmd.CommandType = CommandType.Text;
                count = Convert.ToInt16(cmd.ExecuteScalar());
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("ExistsUserID: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return count;
        }

        internal static string InsertUpdateEmp(EmpDetails inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string success = "";
            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_EmployeeDetailsSaveUpdate", con);
                cmd.Parameters.AddWithValue("@empid", inputtext.EmpID);
                cmd.Parameters.AddWithValue("@Name", inputtext.EmpName);
                cmd.Parameters.AddWithValue("@Role", inputtext.Role);
                cmd.Parameters.AddWithValue("@Department", inputtext.Dept);
                cmd.Parameters.AddWithValue("@Email", inputtext.Email);
                cmd.Parameters.AddWithValue("@password", inputtext.Password);
                cmd.Parameters.AddWithValue("@ReportingTo", inputtext.ReportingTo);
                cmd.Parameters.AddWithValue("@Region", inputtext.Region);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = (sdr["SaveFlag"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("InsertUpdateEmp: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return success;
        }

        internal static List<string> GetUsersList()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<string> list = new List<string>();

            try
            {
                cmd = new SqlCommand("select Employeeid from Employee_Information", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            list.Add(sdr["Employeeid"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetUsersList: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetUsersList: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<string> GetDepartmentHeads()
        {
            List<string> list = new List<string>();

            try
            {
                SqlConnection con = ConnectionManager.GetConnection();
                SqlCommand cmd;
                SqlDataReader sdr;

                cmd = new SqlCommand("select Employeeid from Employee_Information where Role in ('Team Leader','Team Manager')", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(sdr["Employeeid"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetDepartmentHeads: " + ex.Message);
            }

            return list;
        }

        internal static List<string> GetEmpIDList()
        {
            List<string> list = new List<string>();

            try
            {
                SqlConnection con = ConnectionManager.GetConnection();
                SqlCommand cmd;
                SqlDataReader sdr;

                cmd = new SqlCommand("select distinct(Employeeid) from Employee_Information", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(sdr["Employeeid"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetEmpIDList: " + ex.Message);
            }

            return list;
        }

        #endregion

        #region ----- Customer Information -----

        internal static List<Customer> GetCustomerInfo()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<Customer> list = new List<Customer>();
            Customer custInfo;

            try
            {
                cmd = new SqlCommand("select id,Customer,CustomerDescription,Region from Customer", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            custInfo = new Customer();
                            custInfo.ID = Convert.ToInt16(sdr["id"].ToString());
                            custInfo.CName = sdr["Customer"].ToString();
                            custInfo.CDescription = sdr["CustomerDescription"].ToString();
                            custInfo.CRegion = sdr["Region"].ToString();
                            list.Add(custInfo);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetCustomerInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetCustomerInfo: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static string SaveCustomerDetails(Customer inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr;

            string success = String.Empty;
            int ID = 0;

            try
            {
                cmd = new SqlCommand("select ID from Customer where Customer=@Customer and Region=@Region", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Customer", inputtext.CName);
                cmd.Parameters.AddWithValue("@Region", inputtext.CRegion);
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows) //Update
                {
                    while (sdr.Read())
                    {
                        ID = Convert.ToInt16(sdr["ID"].ToString());
                    }

                    con.Close();
                    con.Open();

                    cmd = new SqlCommand("update Customer set Customer=@Customer,CustomerDescription=@CustomerDescription,Region=@Region where ID=@ID", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Customer", inputtext.CName);
                    cmd.Parameters.AddWithValue("@CustomerDescription", inputtext.CDescription);
                    cmd.Parameters.AddWithValue("@Region", inputtext.CRegion);
                    cmd.ExecuteNonQuery();

                    success = "Updated";

                    con.Close();
                }
                else //Insert
                {
                    con.Close();
                    con.Open();
                    cmd = new SqlCommand("insert into Customer (Customer,CustomerDescription,Region) values (@Customer,@CustomerDescription,@Region)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Customer", inputtext.CName);
                    cmd.Parameters.AddWithValue("@CustomerDescription", inputtext.CDescription);
                    cmd.Parameters.AddWithValue("@Region", inputtext.CRegion);
                    cmd.ExecuteNonQuery();

                    success = "Inserted";

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SaveCustomerDetails: " + ex.Message);
            }

            return success;
        }

        internal static void UpdateCustomerDetails(Customer inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("update Customer set Customer=@Customer,CustomerDescription=@CustomerDescription,Region=@Region where ID=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", inputtext.ID);
                cmd.Parameters.AddWithValue("@Customer", inputtext.CName);
                cmd.Parameters.AddWithValue("@CustomerDescription", inputtext.CDescription);
                cmd.Parameters.AddWithValue("@Region", inputtext.CRegion);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("UpdateCustomerDetails: " + ex.Message);
            }
        }

        internal static void DeleteCustomer(Customer inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Delete from Customer where ID=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", inputtext.ID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("DeleteCustomer: " + ex.Message);
            }
        }

        internal static List<string> BindCRegion()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<string> list = new List<string>();

            try
            {
                cmd = new SqlCommand("select Region from Region", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            list.Add(sdr["Region"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("BindCRegion: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindCRegion: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<Customer> FilterCustomerInfo(Customer inputtext, int cnt)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<Customer> list = new List<Customer>();
            Customer custInfo;
            string query = string.Empty;

            string query1 = "select id,Customer,CustomerDescription,Region from Customer where Customer=@Customer and Region=@Region";
            string query2 = "select id,Customer,CustomerDescription,Region from Customer where Customer=@Customer";
            string query3 = "select id,Customer,CustomerDescription,Region from Customer where Region=@Region";
            string query4 = "select id,Customer,CustomerDescription,Region from Customer";

            if (cnt == 1)
                query = query1;
            else if (cnt == 2)
                query = query2;
            else if (cnt == 3)
                query = query3;
            else
                query = query4;
            try
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Customer", inputtext.CName);
                cmd.Parameters.AddWithValue("@Region", inputtext.CRegion);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            custInfo = new Customer();
                            custInfo.ID = Convert.ToInt16(sdr["id"].ToString());
                            custInfo.CName = sdr["Customer"].ToString();
                            custInfo.CDescription = sdr["CustomerDescription"].ToString();
                            custInfo.CRegion = sdr["Region"].ToString();
                            list.Add(custInfo);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("FilterCustomerInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("FilterCustomerInfo: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        #endregion

        #region ----- Import MJ Data -----

        internal static List<string> POAccess(string poNum)
        {
            List<string> result = new List<string>();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader dr = null;

            try
            {
                cmd = new SqlCommand("S_Get_ImportMJReport", conn);
                cmd.Parameters.AddWithValue("@PONumber", poNum);
                cmd.Parameters.AddWithValue("@Param", "POList");
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    result.Add(dr["PONumber"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("POAccess: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (dr != null) dr.Close();
            }

            return result;
        }

        internal static DataTable POCheck(Pono inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            DataTable dt = new DataTable();

            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_ImportMJReport", con);
                cmd.Parameters.AddWithValue("@PONumber", inputtext.PoNo);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("POCheck: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }

            return dt;
        }

        internal static List<purchaseOrderDetails> ViewPoDetails(filtervalues inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<purchaseOrderDetails> list = new List<purchaseOrderDetails>();
            purchaseOrderDetails purchaseOrder = null;

            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_ImportMJReport", con);
                cmd.Parameters.AddWithValue("@PONumber", inputtext.PoNumber);
                cmd.Parameters.AddWithValue("@MJ", inputtext.MJnumber);
                cmd.Parameters.AddWithValue("@ProductName", inputtext.Product);
                DateTime.TryParse(inputtext.PoDate, out now);
                cmd.Parameters.AddWithValue("@PODate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            purchaseOrder = new purchaseOrderDetails();
                            purchaseOrder.itemName = sdr["ItemName"].ToString();
                            purchaseOrder.goDown = sdr["GoDown"].ToString();
                            purchaseOrder.SlNo = sdr["SerialNumber"].ToString();
                            purchaseOrder.quantity = sdr["Quantity"].ToString();
                            purchaseOrder.unit = sdr["Unit"].ToString();
                            list.Add(purchaseOrder);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog("ViewPoDetails: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static bool DeletePoDetails(filtervalues inputtext, string param)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            bool success = false;

            try
            {
                cmd = new SqlCommand("S_Get_ImportMJReport", conn);
                cmd.Parameters.AddWithValue("@PONumber", inputtext.PoNumber);
                cmd.Parameters.AddWithValue("@MJ", inputtext.MJnumber);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = Convert.ToBoolean(sdr["DeleteFlag"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("DeletePoDetails: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return success;
        }

        internal static string GetMjNum(string ponum, string prodname, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            DataTable dt = new DataTable();
            string mjnum = "";

            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_ImportMJReport", con);
                cmd.Parameters.AddWithValue("@PONumber", ponum);
                cmd.Parameters.AddWithValue("@ProductName", prodname);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            mjnum = sdr["MJNumber"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetMjNum: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return mjnum;
        }

        internal static string GetMjProduct(string ponum, string pomj, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            DataTable dt = new DataTable();
            string mjprod = "";

            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_ImportMJReport", con);
                cmd.Parameters.AddWithValue("@PONumber", ponum);
                cmd.Parameters.AddWithValue("@MJ", pomj);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            mjprod = sdr["ProductName"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getMjProduct: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return mjprod;
        }

        internal static string SaveImportDataTodb(DataRow dtrow)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string success = "";

            try
            {
                cmd = new SqlCommand("S_Get_ImportMJReport", conn);
                DateTime now = DateTime.Now;
                cmd.Parameters.AddWithValue("@MJ", dtrow["MJNo"].ToString());
                cmd.Parameters.AddWithValue("@ItemName", dtrow["ItemName"].ToString());
                cmd.Parameters.AddWithValue("@GoDown", dtrow["GoDown"]);
                cmd.Parameters.AddWithValue("@SerialNumber", dtrow["SerialNo"].ToString());
                cmd.Parameters.AddWithValue("@Quantity", dtrow["Quantity"].ToString());
                cmd.Parameters.AddWithValue("@Unit", dtrow["Unit"].ToString());
                cmd.Parameters.AddWithValue("@ProductName", dtrow["ProductName"].ToString());
                cmd.Parameters.AddWithValue("@BatchNo", dtrow["BatchNo"].ToString());
                cmd.Parameters.AddWithValue("@ProductQty", dtrow["ProductQuantity"].ToString());
                cmd.Parameters.AddWithValue("@PONumber", dtrow["PONumber"].ToString());
                DateTime.TryParse(dtrow["PODate"].ToString().Trim(), out now);
                cmd.Parameters.AddWithValue("@PODate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Param", "Save");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = sdr["Flag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SaveImportDataTodb: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }

        internal static bool Verify(DataTable dt)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader rdr = null;

            bool success = false;

            try
            {
                cmd = new SqlCommand("S_Get_ImportMJReport", conn);
                cmd.Parameters.AddWithValue("@PONumber", dt.Rows[0]["PONumber"].ToString());
                cmd.Parameters.AddWithValue("@MJ", dt.Rows[0]["MJNo"].ToString());
                cmd.Parameters.AddWithValue("@Param", "Existance");
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        success = Convert.ToBoolean(rdr["Flag"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Verify: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }

            return success;
        }

        #endregion

        #region ----- Defect Tracker -----

        #region ----- Project -----

        internal static List<DefectTracker> GetEmpDashboardInfo(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<DefectTracker> list = new List<DefectTracker>();
            DefectTracker defectTracker;

            try
            {
                cmd = new SqlCommand("S_Get_Project_Backlog_Details", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.PID);
                cmd.Parameters.AddWithValue("@DevAssignedTo", inputtext.Assignee);
                cmd.Parameters.AddWithValue("@ProblemType", inputtext.IssueType);
                cmd.Parameters.AddWithValue("@Priority", inputtext.Priority);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);

                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            defectTracker = new DefectTracker();
                            defectTracker.PID = sdr["ProjectID"].ToString();
                            defectTracker.PName = sdr["ProjectName"].ToString();
                            defectTracker.CName = sdr["CustomerName"].ToString();
                            defectTracker.Module = sdr["Module"].ToString();
                            defectTracker.IssueID = sdr["ProblemID"].ToString();
                            defectTracker.IssueTypeDisplayName = HelperClass.IssueTypeDisplayName(sdr["ProblemType"].ToString());
                            defectTracker.IssueType = sdr["ProblemType"].ToString();
                            defectTracker.IssueName = sdr["Problem"].ToString();
                            defectTracker.IssueDesc = sdr["ProblemDescription"].ToString();
                            defectTracker.Priority = sdr["Priority"].ToString();
                            defectTracker.Steps = sdr["Steps_To_Reproduce"].ToString();
                            defectTracker.Changes = sdr["Changes_Done_Before_This_Issue"].ToString();
                            defectTracker.ReporterType = sdr["ReporterType"].ToString();
                            defectTracker.ReportedBy = sdr["ReportedBy"].ToString();
                            defectTracker.ReportedDate = Convert.ToDateTime(sdr["ReporteDate"]);
                            defectTracker.Environment = sdr["Environment"].ToString();
                            defectTracker.Status = sdr["Status"].ToString();
                            list.Add(defectTracker);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetEmpDashboardInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetEmpDashboardInfo" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<DefectTracker> GetProjectInfo(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<DefectTracker> list = new List<DefectTracker>();
            DefectTracker defectTracker = null;

            try
            {
                cmd = new SqlCommand("S_Get_ProjectDetails", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.PID);
                cmd.Parameters.AddWithValue("@CustomerName", inputtext.CName);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            defectTracker = new DefectTracker();
                            defectTracker.PID = sdr["ProjectID"].ToString();
                            defectTracker.PName = sdr["ProjectName"].ToString();
                            defectTracker.CName = sdr["CustomerName"].ToString();
                            defectTracker.Users = sdr["UserIDs"].ToString();
                            defectTracker.ProjectOwner = sdr["ProjectOwner"].ToString();
                            defectTracker.ProjectCreatedBy = sdr["CreatedBy"].ToString();
                            defectTracker.ProjectStatus = sdr["Status"].ToString();
                            defectTracker.EstimatedEffort = sdr["EstimatedEffort"].ToString();

                            string DDate = sdr["DeliveryDate"].ToString();
                            string[] Datetime = DDate.Split(' ');
                            string Date = Datetime[0];
                            defectTracker.Date = Date;

                            DateTime Date2 = Convert.ToDateTime(sdr["DeliveryDate"]);
                            Date2 = Date2.Date;
                            defectTracker.DeliveryDate = Convert.ToDateTime(sdr["DeliveryDate"].ToString());
                            list.Add(defectTracker);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetProjectInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetProjectInfo: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<string> BindCustomerNames()
        {
            List<string> result = new List<string>();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            try
            {
                cmd = new SqlCommand("select Customer from Customer", conn);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    result.Add(sdr["Customer"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindCustomerNames: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return result;
        }

        internal static string InsertUpdateProject(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string success = "";

            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_ProjectDetails", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.PID);
                cmd.Parameters.AddWithValue("@ProjectName", inputtext.PName);
                cmd.Parameters.AddWithValue("@CustomerName", inputtext.CName);
                cmd.Parameters.AddWithValue("@ProjectOwner", inputtext.ProjectOwner);

                if (inputtext.Param == "Save")
                {
                    //inputtext.EstimatedEffort = inputtext.EstimatedEffort.Replace(':', '.');
                    cmd.Parameters.AddWithValue("@EstimatedEffort", Convert.ToDouble(inputtext.EstimatedEffort));
                    cmd.Parameters.AddWithValue("@CreatedBy", inputtext.ProjectCreatedBy);
                    cmd.Parameters.AddWithValue("@DeliveryDate", inputtext.DeliveryDate);
                }

                cmd.Parameters.AddWithValue("@UserIDs", inputtext.Users);
                cmd.Parameters.AddWithValue("@FileName", inputtext.DocumentName);
                cmd.Parameters.AddWithValue("@FileData", inputtext.Document);
                cmd.Parameters.AddWithValue("@FileType", inputtext.FileType);
                cmd.Parameters.AddWithValue("@Status", inputtext.ProjectStatus);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (inputtext.Param == "Delete")
                        {
                            success = (sdr["DeleteFlag"].ToString());
                            return success;
                        }

                        success = (sdr["SaveFlag"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("InsertUpdateProject: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return success;
        }

        internal static List<string> GetProjectIDList()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<string> list = new List<string>();

            try
            {
                cmd = new SqlCommand("select distinct(ProjectID) from ProjectDetails", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    list.Add(sdr["ProjectID"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetProjectIDList" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<Files> GetFileInfo(string param, string prjID)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<Files> list = new List<Files>();
            Files File;

            try
            {
                cmd = new SqlCommand("S_Get_ProjectDetails", con);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@ProjectID", prjID);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            File = new Files();
                            File.IDD = Convert.ToInt16(sdr["IDD"]);
                            File.ProjID = sdr["ProjectID"].ToString();
                            File.FName = sdr["FileName"].ToString();
                            File.FileType = sdr["FileType"].ToString();
                            list.Add(File);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetFileInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetFileInfo" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static string DeleteFile(Files file)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            string success = "";

            try
            {
                cmd = new SqlCommand("S_Get_ProjectDetails", con);
                cmd.Parameters.AddWithValue("@IDD", file.IDD);
                cmd.Parameters.AddWithValue("@ProjectID", file.ProjID);
                cmd.Parameters.AddWithValue("@FileName", file.FName);
                cmd.Parameters.AddWithValue("@Param", file.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = sdr["DeleteFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("DeleteFile: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return success;
        }

        internal static byte[] GetFileInfo(Files filedata)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;
            Byte[] bytedata = null;

            try
            {
                cmd = new SqlCommand("select FileData from ProjectFileDetails where IDD=@IDD and ProjectID=@ProjectID and FileName=@FileName and FileType=@FileType", con);
                cmd.Parameters.AddWithValue("@IDD", filedata.IDD);
                cmd.Parameters.AddWithValue("@ProjectID", filedata.ProjID);
                cmd.Parameters.AddWithValue("@FileName", filedata.FName);
                cmd.Parameters.AddWithValue("@FileType", filedata.FileType);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            bytedata = ((byte[])(sdr["FileData"]));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetFileInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetFileInfo" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return bytedata;
        }

        internal static List<string> GetCNames()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<string> list = new List<string>();

            try
            {
                cmd = new SqlCommand("select distinct(CustomerName) from ProjectDetails", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            list.Add(sdr["CustomerName"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetCNames: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("GetCNames: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<string> GetApplicationTeamNames()
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            List<string> list = new List<string>();

            try
            {
                cmd = new SqlCommand("select Employeeid from Employee_Information where Department='Application Team' and Role='Team Manager'", conn);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    list.Add(sdr["Employeeid"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetApplicationTeamNames: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<string> BindStatus(string category)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            List<string> list = new List<string>();

            try
            {
                cmd = new SqlCommand("select Status from Status where Category='" + category + "'", conn);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    list.Add(sdr["Status"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindStatus: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static int SaveMainTaskData(Taskdetails inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;

            int Result = 0;

            try
            {
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.Projectid);
                cmd.Parameters.AddWithValue("@MainTask", inputtext.Maintask);
                cmd.Parameters.AddWithValue("@Status", inputtext.MaintaskStatus);

                if (inputtext.DeliveryDate == "" || inputtext.DeliveryDate == null)
                    cmd.Parameters.AddWithValue("@DeliveryDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@DeliveryDate", Convert.ToDateTime(inputtext.DeliveryDate));

                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                Result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SaveMainTaskData:" + ex.Message);
            }

            return Result;
        }

        internal static List<ListItem> BindIssueTypeValues()
        {
            List<ListItem> list = new List<ListItem>();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            try
            {
                cmd = new SqlCommand("select Status,Value from Status where Category='Request'", conn);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    list.Add(new ListItem() { Text = sdr["Status"].ToString(), Value = sdr["Value"].ToString() });
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindIssueTypeValues: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        #endregion

        #region ----- Project Backlog ------ 

        #region ----- Internal Project Backlog -----

        internal static List<string> GetAssigneeList_I()
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            List<string> list = new List<string>();

            try
            {
                cmd = new SqlCommand("select Employeeid from Employee_Information where Department in ('Development Team','QA Team') and Role='Team Member'", conn);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    list.Add(sdr["Employeeid"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetAssigneeList_I: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<string> GetProjectModules_Internal()
        {
            List<string> result = new List<string>();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            try
            {
                cmd = new SqlCommand("select distinct(Module) from Internal_Project_Backlog_Details", conn);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    result.Add(sdr["Module"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetProjectModules_Internal: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return result;
        }

        internal static List<string> GetIssueIDList(DefectTracker inputtext)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            List<string> list = new List<string>();

            try
            {
                cmd = new SqlCommand("select ProblemID from External_Project_Backlog_Details where ProjectName=@ProjectName and CustomerName=@CustomerName and Module=@Module", conn);
                cmd.Parameters.AddWithValue("@ProjectName", inputtext.PName);
                cmd.Parameters.AddWithValue("@CustomerName", inputtext.CName);
                cmd.Parameters.AddWithValue("@Module", inputtext.Module);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    list.Add(sdr["ProblemID"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetIssueIDList: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static string GetUserIDs(string PName)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string userIDs = null;

            try
            {
                cmd = new SqlCommand("select UserIDs from ProjectDetails where ProjectName=@ProjectName", conn);
                cmd.Parameters.AddWithValue("@ProjectName", PName);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    userIDs = sdr["UserIDs"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetUserIDs: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return userIDs;
        }

        internal static string GetEmpEmailID(string[] Users)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string MailIDs = "";

            try
            {
                for (int i = 0; i < Users.Length; i++)
                {
                    cmd = new SqlCommand("select Email from Employee_Information where Employeeid=@Employeeid", conn);
                    cmd.Parameters.AddWithValue("@Employeeid", Users[i]);
                    cmd.CommandType = CommandType.Text;
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            if (MailIDs == "")
                                MailIDs = sdr["Email"].ToString();
                            else
                                MailIDs += "," + sdr["Email"].ToString();
                        }
                        sdr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetEmpEmailID: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return MailIDs;
        }

        internal static string GetEmailID(string EmpName)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string empID = null;

            try
            {
                cmd = new SqlCommand("select Email from Employee_Information where Employeeid=@Employeeid", conn);
                cmd.Parameters.AddWithValue("@Employeeid", EmpName);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    empID = sdr["Email"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetEmailID: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return empID;
        }

        internal static string InsertUpdateIssues_I(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string success = "";

            try
            {
                DateTime dateTime = DateTime.Now;
                cmd = new SqlCommand("S_Get_Project_Backlog_Details", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.PID);
                cmd.Parameters.AddWithValue("@CustomerName", inputtext.CName);
                cmd.Parameters.AddWithValue("@Module", inputtext.Module);
                cmd.Parameters.AddWithValue("@ProblemID", inputtext.IssueID);
                cmd.Parameters.AddWithValue("@Problem", inputtext.IssueName);
                cmd.Parameters.AddWithValue("@ReporterType", inputtext.ReporterType);
                cmd.Parameters.AddWithValue("@ReportedBy", inputtext.ReportedBy);

                if (inputtext.Param == "Insert_InternalBacklogDetails")
                {
                    cmd.Parameters.AddWithValue("@ReporteDate", inputtext.ReportedDate);
                }

                cmd.Parameters.AddWithValue("@ProjectName", inputtext.PName);
                cmd.Parameters.AddWithValue("@ProblemDescription", inputtext.IssueDesc);
                cmd.Parameters.AddWithValue("@ProblemType", inputtext.IssueType);
                cmd.Parameters.AddWithValue("@DevAssignedTo", inputtext.Assignee);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@Changes_Done_Before_This_Issue", inputtext.Changes);
                cmd.Parameters.AddWithValue("@Steps_To_Reproduce", inputtext.Steps);
                cmd.Parameters.AddWithValue("@Priority", inputtext.Priority);
                cmd.Parameters.AddWithValue("@FileName", inputtext.DocumentName);
                cmd.Parameters.AddWithValue("@FileData", inputtext.Document);

                if (inputtext.Param == "Insert_InternalBacklogDetails")
                {
                    cmd.Parameters.AddWithValue("@UpdatedBy", inputtext.ReportedBy);
                    cmd.Parameters.AddWithValue("@UpdatedTS", inputtext.UpdatedTS);
                }

                if (inputtext.Param == "Update_InternalBacklogDetails")
                {
                    cmd.Parameters.AddWithValue("@UpdatedBy", inputtext.UpdatedBy);
                    cmd.Parameters.AddWithValue("@UpdatedTS", inputtext.UpdatedTS);
                }

                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = (sdr["SaveFlag"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("InsertUpdateIssues_I: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return success;
        }

        internal static List<string> GetProjectModules_I(string Type, string PName)
        {
            List<string> result = new List<string>();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            try
            {
                cmd = new SqlCommand("select distinct(Module) from " + Type + "_Project_Backlog_Details where ProjectName=@ProjectName", conn);
                cmd.Parameters.AddWithValue("@ProjectName", PName);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    result.Add(sdr["Module"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetProjectModules_I: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return result;
        }

        internal static string GetIssueID(string PID, string PName, string CustName, string Module, string IssueName, string ReporterType, string Assignee, string IssueType)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string IssueID = string.Empty;
            try
            {
                cmd = new SqlCommand("select ProblemID from " + ReporterType + "_Project_Backlog_Details where ProjectID=@ProjectID and ProjectName=@ProjectName and CustomerName=@CustomerName and Module=@Module and Problem=@Problem and ReporterType=@ReporterType and DevAssignedTo=@DevAssignedTo and ProblemType=@ProblemType", conn);
                cmd.Parameters.AddWithValue("@ProjectID", PID);
                cmd.Parameters.AddWithValue("@ProjectName", PName);
                cmd.Parameters.AddWithValue("@CustomerName", CustName);
                cmd.Parameters.AddWithValue("@Module", Module);
                cmd.Parameters.AddWithValue("@Problem", IssueName);
                cmd.Parameters.AddWithValue("@ReporterType", ReporterType);
                cmd.Parameters.AddWithValue("@DevAssignedTo", Assignee);
                cmd.Parameters.AddWithValue("@ProblemType", IssueType);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        IssueID = sdr["ProblemID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetIssueID: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return IssueID;
        }

        internal static List<DefectTracker> GetIssueDetails(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<DefectTracker> list = new List<DefectTracker>();
            DefectTracker df;

            try
            {
                cmd = new SqlCommand("select Problem,ProblemDescription,ProblemType,Steps_To_Reproduce,Changes_Done_Before_This_Issue from External_Project_Backlog_Details where ProjectName=@ProjectName and CustomerName=@CustomerName and Module=@Module and ProblemID=@ProblemID", con);
                cmd.Parameters.AddWithValue("@ProjectName", inputtext.PName);
                cmd.Parameters.AddWithValue("@CustomerName", inputtext.CName);
                cmd.Parameters.AddWithValue("@Module", inputtext.Module);
                cmd.Parameters.AddWithValue("@ProblemID", inputtext.IssueID);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            df = new DefectTracker();
                            df.IssueName = sdr["Problem"].ToString();
                            df.IssueDesc = sdr["ProblemDescription"].ToString();
                            df.IssueType = sdr["ProblemType"].ToString();
                            df.Steps = sdr["Steps_To_Reproduce"].ToString();
                            df.Changes = sdr["Changes_Done_Before_This_Issue"].ToString();
                            list.Add(df);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetIssueDetails: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("GetIssueDetails: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        #endregion

        #region ----- External Project Backlog -----

        internal static List<DefectTracker> GetIssueInfo(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<DefectTracker> list = new List<DefectTracker>();
            DefectTracker defectTracker = null;

            try
            {
                cmd = new SqlCommand("S_Get_Project_Backlog_Details", con);

                if (inputtext.StartDate != null && inputtext.StartDate != "")
                {
                    cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(inputtext.StartDate));
                }

                if (inputtext.EndDate != null && inputtext.EndDate != "")
                {
                    DateTime dt1 = Convert.ToDateTime(inputtext.EndDate);
                    cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(dt1.ToString("yyyy-MM-dd" + " 23:59:59")));
                }

                cmd.Parameters.AddWithValue("@ProjectID", inputtext.PID);
                cmd.Parameters.AddWithValue("@ProblemType", inputtext.IssueType);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@DevAssignedTo", inputtext.Assignee);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            defectTracker = new DefectTracker();
                            defectTracker.PID = sdr["ProjectID"].ToString();
                            defectTracker.PName = sdr["ProjectName"].ToString();
                            defectTracker.CName = sdr["CustomerName"].ToString();
                            defectTracker.Module = sdr["Module"].ToString();
                            defectTracker.IssueID = sdr["ProblemID"].ToString();
                            defectTracker.IssueName = sdr["Problem"].ToString();
                            defectTracker.IssueDesc = sdr["ProblemDescription"].ToString();
                            defectTracker.IssueType = sdr["ProblemType"].ToString();
                            defectTracker.IssueTypeDisplayName = HelperClass.IssueTypeDisplayName(sdr["ProblemType"].ToString());
                            defectTracker.Changes = sdr["Changes_Done_Before_This_Issue"].ToString();
                            defectTracker.Steps = sdr["Steps_To_Reproduce"].ToString();
                            defectTracker.ReporterType = sdr["ReporterType"].ToString();
                            defectTracker.ReportedBy = sdr["ReportedBy"].ToString();
                            defectTracker.ReportedDate = Convert.ToDateTime(sdr["ReporteDate"]);
                            defectTracker.Priority = sdr["Priority"].ToString();
                            defectTracker.Assignee = sdr["DevAssignedTo"].ToString();

                            if (inputtext.Param == "View_ExternalBacklogDetails")
                            {
                                defectTracker.Environment = sdr["Environment"].ToString();
                            }

                            defectTracker.DocumentName = sdr["FileName"].ToString();
                            defectTracker.Status = sdr["Status"].ToString();

                            if (defectTracker.Status == "Closed")
                            {
                                if (defectTracker.FilterSearch == "Yes")
                                    list.Add(defectTracker);
                                else
                                    continue;
                            }
                            else
                                list.Add(defectTracker);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetIssueInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetIssueInfo: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static DataTable GetHistoryIssueInfo(string ID, string Name, string IID, string IType, string Param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("S_Get_Project_Backlog_Details", con);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ProjectID", ID);
                cmd.Parameters.AddWithValue("@ProjectName", Name);
                cmd.Parameters.AddWithValue("@ProblemID", IID);
                cmd.Parameters.AddWithValue("@ProblemType", IType);
                cmd.Parameters.AddWithValue("@Param", Param);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetHistoryIssueInfo: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }

            return dt;
        }

        internal static List<string> GetProjectsList()
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            List<string> list = new List<string>();

            try
            {
                cmd = new SqlCommand("select distinct(ProjectName) from ProjectDetails", conn);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    list.Add(sdr["ProjectName"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetProjectsList: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<string> GetProjectModules()
        {
            List<string> result = new List<string>();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            try
            {
                cmd = new SqlCommand("select distinct(Module) from External_Project_Backlog_Details", conn);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    result.Add(sdr["Module"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetProjectModules: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return result;
        }

        internal static List<string> GetExternalEnvironmentList()
        {
            List<string> result = new List<string>();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            try
            {
                cmd = new SqlCommand("select distinct(Environment) from External_Project_Backlog_Details", conn);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    result.Add(sdr["Environment"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetExternalEnvironmentList: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return result;
        }

        internal static string InsertUpdateIssues(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string success = "";

            try
            {
                cmd = new SqlCommand("S_Get_Project_Backlog_Details", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.PID);
                cmd.Parameters.AddWithValue("@CustomerName", inputtext.CName);
                cmd.Parameters.AddWithValue("@Module", inputtext.Module);
                cmd.Parameters.AddWithValue("@ProblemID", inputtext.IssueID);
                cmd.Parameters.AddWithValue("@Problem", inputtext.IssueName);
                cmd.Parameters.AddWithValue("@ReporterType", inputtext.ReporterType);
                cmd.Parameters.AddWithValue("@ReportedBy", inputtext.ReportedBy);

                if (inputtext.Param == "Insert_ExternalBacklogDetails")
                {
                    cmd.Parameters.AddWithValue("@ReporteDate", inputtext.ReportedDate);
                }

                cmd.Parameters.AddWithValue("@ProjectName", inputtext.PName);
                cmd.Parameters.AddWithValue("@ProblemDescription", inputtext.IssueDesc);
                cmd.Parameters.AddWithValue("@ProblemType", inputtext.IssueType);
                cmd.Parameters.AddWithValue("@DevAssignedTo", inputtext.Assignee);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@Changes_Done_Before_This_Issue", inputtext.Changes);
                cmd.Parameters.AddWithValue("@Steps_To_Reproduce", inputtext.Steps);
                cmd.Parameters.AddWithValue("@Priority", inputtext.Priority);
                cmd.Parameters.AddWithValue("@Environment", inputtext.Environment);
                cmd.Parameters.AddWithValue("@FileName", inputtext.DocumentName);
                cmd.Parameters.AddWithValue("@FileData", inputtext.Document);

                if (inputtext.Param == "Insert_ExternalBacklogDetails")
                {
                    cmd.Parameters.AddWithValue("@UpdatedBy", inputtext.ReportedBy);
                    cmd.Parameters.AddWithValue("@UpdatedTS", inputtext.UpdatedTS);
                }

                if (inputtext.Param == "Update_ExternalBacklogDetails")
                {
                    cmd.Parameters.AddWithValue("@UpdatedBy", inputtext.UpdatedBy);
                    cmd.Parameters.AddWithValue("@UpdatedTS", inputtext.UpdatedTS);
                }

                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = (sdr["SaveFlag"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("InsertUpdateIssues: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return success;
        }

        internal static void DeleteIssueFiles(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("delete from " + inputtext.ReporterType + "_Project_Backlog_File_Details where ProjectID=@ProjectID and CustomerName=@CustomerName and Module=@Module and ProblemID=@ProblemID and Problem=@Problem and ReporterType=@ReporterType and ReportedBy=@ReportedBy", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.PID);
                cmd.Parameters.AddWithValue("@CustomerName", inputtext.CName);
                cmd.Parameters.AddWithValue("@Module", inputtext.Module);
                cmd.Parameters.AddWithValue("@ProblemID", inputtext.IssueID);
                cmd.Parameters.AddWithValue("@Problem", inputtext.IssueName);
                cmd.Parameters.AddWithValue("@ReporterType", inputtext.ReporterType);
                cmd.Parameters.AddWithValue("@ReportedBy", inputtext.ReportedBy);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("DeleteIssueFiles: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        internal static string GetCustName(string pname)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string CName = "";

            try
            {
                cmd = new SqlCommand("select CustomerName from ProjectDetails where ProjectName=@ProjectName", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProjectName", pname);
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        CName = sdr["CustomerName"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetCustName: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return CName;
        }

        internal static string GetProjectID(string pname)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string PID = "";
            try
            {
                cmd = new SqlCommand("select ProjectID from ProjectDetails where ProjectName=@ProjectName", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProjectName", pname);
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        PID = sdr["ProjectID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetProjectID: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return PID;
        }

        internal static List<string> GetAssigneeList()
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            List<string> list = new List<string>();

            try
            {
                cmd = new SqlCommand("select Employeeid from Employee_Information where Role='Team Manager' and Department in ('Development Team','Application Team','QA Team')", conn);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    list.Add(sdr["Employeeid"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetAssigneeList: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<BacklogFiles> GetBacklogFileInfo(BacklogFiles inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<BacklogFiles> list = new List<BacklogFiles>();
            BacklogFiles File;

            try
            {
                cmd = new SqlCommand("S_Get_Project_Backlog_Details", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.PID);
                cmd.Parameters.AddWithValue("@Module", inputtext.Module);
                cmd.Parameters.AddWithValue("@CustomerName", inputtext.CName);
                cmd.Parameters.AddWithValue("@ProblemID", inputtext.IssueID);
                cmd.Parameters.AddWithValue("@Problem", inputtext.IssueName);
                cmd.Parameters.AddWithValue("@ReporterType", inputtext.ReporterType);
                cmd.Parameters.AddWithValue("@ReportedBy", inputtext.ReportedBy);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            File = new BacklogFiles();
                            File.IDD = Convert.ToInt16(sdr["IDD"]);
                            File.FName = sdr["FileName"].ToString();
                            list.Add(File);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetBacklogFileInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetBacklogFileInfo: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static string DeleteBacklogFile(BacklogFiles file)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            string success = "";

            try
            {
                cmd = new SqlCommand("S_Get_Project_Backlog_Details", con);
                cmd.Parameters.AddWithValue("@IDD", file.IDD);
                cmd.Parameters.AddWithValue("@Param", file.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = sdr["DeleteFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("DeleteBacklogFile: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return success;
        }

        internal static byte[] GetBacklogByte(BacklogFiles backlog)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;
            Byte[] bytedata = null;

            try
            {
                cmd = new SqlCommand("select FileData from " + backlog.ReporterType + "_Project_Backlog_File_Details where IDD=@IDD and ProjectID=@ProjectID and CustomerName=@CustomerName and Module=@Module and ProblemID=@ProblemID and Problem=@Problem and ProblemType=@ProblemType and ReporterType=@ReporterType and ReportedBy=@ReportedBy and FileName=@FileName", con);
                cmd.Parameters.AddWithValue("@IDD", backlog.IDD);
                cmd.Parameters.AddWithValue("@ProjectID", backlog.PID);
                cmd.Parameters.AddWithValue("@CustomerName", backlog.CName);
                cmd.Parameters.AddWithValue("@Module", backlog.Module);
                cmd.Parameters.AddWithValue("@ProblemID", backlog.IssueID);
                cmd.Parameters.AddWithValue("@Problem", backlog.IssueName);
                cmd.Parameters.AddWithValue("@ProblemType", backlog.IssueType);
                cmd.Parameters.AddWithValue("@ReporterType", backlog.ReporterType);
                cmd.Parameters.AddWithValue("@ReportedBy", backlog.ReportedBy);
                cmd.Parameters.AddWithValue("@FileName", backlog.FName);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            bytedata = ((byte[])(sdr["FileData"]));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetBacklogByte: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetBacklogByte" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return bytedata;
        }

        internal static string GetProjectAssigneeList(string ProjectID)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            string AssigneeList = String.Empty;

            try
            {
                cmd = new SqlCommand("select UserIDs from ProjectDetails where ProjectID=@ProjectID", con);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        AssigneeList = sdr["UserIDs"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetProjectAssigneeList" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return AssigneeList;
        }

        #endregion

        #region ----- Sub Task -----

        internal static List<Subtaskdetails> GetMainTaskDetails(Subtaskdetails subtask)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;


            List<Subtaskdetails> list = new List<Subtaskdetails>();
            Subtaskdetails sub;

            try
            {
                cmd = new SqlCommand("Select IDD,EstimatedEffort from Project_MainTask_MasterDetails where ProjectID=@ProjectID and MainTask= @MainTask", con);
                cmd.Parameters.AddWithValue("@ProjectID", subtask.Projectid);
                cmd.Parameters.AddWithValue("@MainTask", subtask.Maintask);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    sub = new Subtaskdetails();
                    sub.MainTaskIDD = sdr["IDD"].ToString();
                    sub.MainTaskEstimatedeffort = sdr["EstimatedEffort"].ToString();
                    list.Add(sub);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetMainTaskDetails: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static int SaveSubTaskInfo(Subtaskdetails inputtext, string param)
        {
            int Result = 0;

            List<Subtaskdetails> list = new List<Subtaskdetails>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@Projectid", inputtext.Projectid);
                cmd.Parameters.AddWithValue("@Weekno", inputtext.Weekno);
                cmd.Parameters.AddWithValue("@YearNo", inputtext.Year);
                cmd.Parameters.AddWithValue("@MainTaskIDD", inputtext.MainTaskIDD);
                cmd.Parameters.AddWithValue("@MainTaskestimatedEffort", inputtext.MainTaskEstimatedeffort);
                cmd.Parameters.AddWithValue("@Maintask", inputtext.Maintask);
                cmd.Parameters.AddWithValue("@SubTask", inputtext.Subtask);

                inputtext.Estimatedeffortsub = inputtext.Estimatedeffortsub.Replace(':', '.');

                if (inputtext.Estimatedeffortsub == "" || inputtext.Estimatedeffortsub == null)
                    cmd.Parameters.AddWithValue("@EstimatedEffort ", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Estimatedeffort", Convert.ToDouble(inputtext.Estimatedeffortsub));

                cmd.Parameters.AddWithValue("@Tasktype", inputtext.Tasktype);
                cmd.Parameters.AddWithValue("@AssignedTo", inputtext.Assignedto);
                cmd.Parameters.AddWithValue("@ProblemType", inputtext.Request);
                cmd.Parameters.AddWithValue("@Dependency", inputtext.Dependencies);
                cmd.Parameters.AddWithValue("@Status", inputtext.SubtaskStatus);
                cmd.Parameters.AddWithValue("@ManualEntryRemarks", inputtext.ManualEntryRemark);
                cmd.Parameters.AddWithValue("@ProblemID", inputtext.ProblemID);
                cmd.Parameters.AddWithValue("@DeliveryDate", Convert.ToDateTime(inputtext.DeliveryDate));
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;

                Result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("SaveSubTaskInfo" + e.Message);
            }

            return Result;
        }

        #endregion

        #endregion

        #region ----- Filters -----

        internal static List<DefectTracker> GetFilterDashboardInfo(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<DefectTracker> list = new List<DefectTracker>();
            DefectTracker defectTracker = null;

            try
            {
                cmd = new SqlCommand("S_Get_Project_Backlog_Details", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.PID);
                cmd.Parameters.AddWithValue("@DevAssignedTo", inputtext.Assignee);
                cmd.Parameters.AddWithValue("@ProblemType", inputtext.IssueType);
                cmd.Parameters.AddWithValue("@Priority", inputtext.Priority);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);

                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            defectTracker = new DefectTracker();
                            defectTracker.PID = sdr["ProjectID"].ToString();
                            defectTracker.PName = sdr["ProjectName"].ToString();
                            defectTracker.CName = sdr["CustomerName"].ToString();
                            defectTracker.IssueID = sdr["ProblemID"].ToString();
                            defectTracker.IssueType = sdr["ProblemType"].ToString();
                            defectTracker.IssueName = sdr["Problem"].ToString();
                            defectTracker.IssueDesc = sdr["ProblemDescription"].ToString();
                            defectTracker.Priority = sdr["Priority"].ToString();
                            defectTracker.Steps = sdr["Steps_To_Reproduce"].ToString();
                            defectTracker.Changes = sdr["Changes_Done_Before_This_Issue"].ToString();
                            defectTracker.ReporterType = sdr["ReporterType"].ToString();
                            defectTracker.ReportedBy = sdr["ReportedBy"].ToString();
                            defectTracker.ReportedDate = Convert.ToDateTime(sdr["ReporteDate"]);
                            defectTracker.Environment = sdr["Environment"].ToString();
                            defectTracker.Status = sdr["Status"].ToString();
                            list.Add(defectTracker);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetFilterDashboardInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetFilterDashboardInfo: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<DefectTracker> GetFilterProjectInfo(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<DefectTracker> list = new List<DefectTracker>();
            DefectTracker defectTracker = null;

            try
            {
                cmd = new SqlCommand("S_Get_ProjectDetails", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.PID);
                cmd.Parameters.AddWithValue("@ProjectName", inputtext.PName);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            defectTracker = new DefectTracker();
                            defectTracker.PID = sdr["ProjectID"].ToString();
                            defectTracker.PName = sdr["ProjectName"].ToString();
                            defectTracker.CName = sdr["CustomerName"].ToString();
                            defectTracker.Email = sdr["Email"].ToString();
                            defectTracker.DocumentName = sdr["FileName"].ToString();
                            list.Add(defectTracker);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetFilterProjectInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetFilterProjectInfo: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        internal static List<DefectTracker> GetFilterIssueInfo(DefectTracker inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            List<DefectTracker> list = new List<DefectTracker>();
            DefectTracker defectTracker = null;

            try
            {
                cmd = new SqlCommand("S_Get_Project_Backlog_Details", con);
                cmd.Parameters.AddWithValue("@StartDate", inputtext.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", inputtext.EndDate);/*HelperClass.GetDateTime(inputtext.StartDate).ToString("yyyy-MM-dd"));*/
                cmd.Parameters.AddWithValue("@ProjectName", inputtext.PName);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            defectTracker = new DefectTracker();
                            defectTracker.PID = sdr["ProjectID"].ToString();
                            defectTracker.PName = sdr["ProjectName"].ToString();
                            defectTracker.CName = sdr["CustomerName"].ToString();
                            defectTracker.Module = sdr["Module"].ToString();
                            defectTracker.IssueID = sdr["ProblemID"].ToString();
                            defectTracker.IssueName = sdr["Problem"].ToString();
                            defectTracker.IssueDesc = sdr["ProblemDescription"].ToString();
                            defectTracker.IssueType = sdr["ProblemType"].ToString();
                            defectTracker.Changes = sdr["Changes_Done_Before_This_Issue"].ToString();
                            defectTracker.Steps = sdr["Steps_To_Reproduce"].ToString();
                            defectTracker.ReporterType = sdr["ReporterType"].ToString();
                            defectTracker.ReportedBy = sdr["ReportedBy"].ToString();
                            defectTracker.ReportedDate = Convert.ToDateTime(sdr["ReporteDate"]);
                            defectTracker.Priority = sdr["Priority"].ToString();
                            defectTracker.Assignee = sdr["DevAssignedTo"].ToString();

                            if (inputtext.Param != "View_InternalBacklogDetails")
                            {
                                defectTracker.Environment = sdr["Environment"].ToString();
                            }

                            defectTracker.DocumentName = sdr["FileName"].ToString();
                            defectTracker.Status = sdr["Status"].ToString();
                            list.Add(defectTracker);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("GetFilterIssueInfo: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetFilterIssueInfo: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }

        #endregion

        #endregion

        #region ----- Web Service -----

        internal static void InsertEmailDetails(WService ws)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("insert into MailDetails ([From],[To],BodyMsg,RequestedTime,Subject,Flag) values (@From,@To,@BodyMsg,@RequestedTime,@Subject,@Flag)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@From", ws.From);
                cmd.Parameters.AddWithValue("@To", ws.To);
                cmd.Parameters.AddWithValue("@BodyMsg", ws.MsgBody);
                cmd.Parameters.AddWithValue("@RequestedTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Subject", ws.Subject);
                cmd.Parameters.AddWithValue("@Flag", 0);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("InsertEmailDetails: " + ex.Message);
            }
        }

        #endregion

        #region ----- Week Definition -----

        internal static void DeleteFromCalender(int year)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;

            string query = @"delete from Calender where YearNo=@yrNo";

            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@yrNo", year);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("DeleteFromCalender: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        internal static DataTable GetWeekInformationFromDB(int year)
        {
            DataTable dtWkInfo = new DataTable();

            dtWkInfo.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("WeekDate", typeof(string)),
                new DataColumn("WeekNumber", typeof(int)),
                new DataColumn("MonthVal", typeof(int)),
                new DataColumn("YearNo", typeof(int))
            });

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd;
            SqlDataReader sdr = null;

            string query = @"select * from Calender where YearNo=@year";

            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@year", year);
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        DataRow dataRow = dtWkInfo.NewRow();
                        dataRow["WeekDate"] = Convert.ToDateTime(sdr["WeekDate"].ToString()).ToString("dd-MMM-yyyy");
                        dataRow["WeekNumber"] = Convert.ToInt32(sdr["WeekNumber"].ToString());
                        dataRow["MonthVal"] = Convert.ToInt32(sdr["MonthVal"].ToString());
                        dataRow["YearNo"] = Convert.ToInt32(sdr["YearNo"]);
                        dtWkInfo.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetWeekInformationFromDB: " + ex.Message);
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }

            return dtWkInfo;
        }

        internal static void BulkInsertIntoCalender(DataTable dtInsertInToCalender)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlBulkCopy sqlBulkCopy = null;

            try
            {
                sqlBulkCopy = new SqlBulkCopy(conn);
                sqlBulkCopy.DestinationTableName = "Calender";
                sqlBulkCopy.ColumnMappings.Add("WeekDate", "WeekDate");
                sqlBulkCopy.ColumnMappings.Add("WeekNumber", "WeekNumber");
                sqlBulkCopy.ColumnMappings.Add("MonthVal", "MonthVal");
                sqlBulkCopy.ColumnMappings.Add("YearNo", "YearNo");
                sqlBulkCopy.WriteToServer(dtInsertInToCalender);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BulkInsertIntoCalender: " + ex.Message);
            }
            finally
            {
                if (sqlBulkCopy != null) sqlBulkCopy.Close();
                if (conn != null) conn.Close();
            }
        }

        #endregion

        #region "-----Import Invoice -----------"

        internal static List<string> getNewInvoiceList(string param, string ponum)
        {
            List<string> result = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_DispatchDetailsReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@PONumber", ponum);

                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result.Add(sdr["InvoiceNumber"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }
        internal static List<InvoiceDetails1> ViewInvoiceDetails(filterValues1 inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<InvoiceDetails1> list = new List<InvoiceDetails1>();
            InvoiceDetails1 purchaseOrder = null;
            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_ImportMJInvoiceReport", con);
                cmd.Parameters.AddWithValue("@PONumber", inputtext.PoNumber);
                cmd.Parameters.AddWithValue("@InvoiceNumber", inputtext.InvoiceNo);

                //cmd.Parameters.AddWithValue("@MJ", inputtext.MJnumber);
                //cmd.Parameters.AddWithValue("@ProductName", inputtext.Product);
                DateTime.TryParse(inputtext.InvoiceDt, out now);
                cmd.Parameters.AddWithValue("@Invoicedate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                // cmd.Parameters.AddWithValue("@Invoicedate", inputtext.InvoiceDt);
                //cmd.Parameters.AddWithValue("@PODate", inputtext.PoDate);

                DateTime.TryParse(inputtext.PoDate, out now);
                cmd.Parameters.AddWithValue("@PODate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                //cmd.Parameters.AddWithValue("@PODate", now.ToString("yyyy-MM-dd"));

                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            purchaseOrder = new InvoiceDetails1();
                            purchaseOrder.productname = sdr["ProductionName"].ToString();
                            purchaseOrder.quantity = sdr["Quantity"].ToString();
                            purchaseOrder.unit = sdr["Unit"].ToString();
                            purchaseOrder.mjNumber = sdr["MJNumber"].ToString();
                            purchaseOrder.InvoiceValue = sdr["InvoiceValue"].ToString();
                            purchaseOrder.FileUpload = sdr["Attachment_FilePath"].ToString();
                            if (sdr["attachmentfile"].ToString() == "" || sdr["attachmentfile"] == DBNull.Value)
                            {
                                purchaseOrder.attachedFile = null;
                            }
                            else
                            {
                                if (sdr["Attachment_FilePath"].ToString().EndsWith(".pdf"))
                                {

                                    purchaseOrder.attachedFile = (byte[])sdr["attachmentfile"];
                                    byte[] bytes = (byte[])sdr["attachmentfile"];
                                    purchaseOrder.AttachmentFileBase64 = Convert.ToBase64String(bytes);
                                }
                                else
                                {
                                    byte[] bytes = (byte[])sdr["attachmentfile"];
                                    purchaseOrder.attachedFile = bytes;
                                    string strBase64 = Convert.ToBase64String(bytes);
                                    purchaseOrder.attachmentfile = "data:Image/png;base64," + strBase64;
                                    purchaseOrder.AttachmentFileBase64 = Convert.ToBase64String(bytes);

                                }
                            }
                            list.Add(purchaseOrder);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static List<string> ViewMJList(string ponum, string prodName, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            List<string> list = new List<string>();
            //InvoiceDetails1 purchaseOrder = null;
            try
            {
                cmd = new SqlCommand("S_Get_ImportMJInvoiceReport", con);
                cmd.Parameters.AddWithValue("@PONumber", ponum);
                cmd.Parameters.AddWithValue("@ProductionName", prodName);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //sda.Fill(dt);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            //purchaseOrder = new InvoiceDetails1();
                            //purchaseOrder.mjNumber = sdr["MJNumber"].ToString();
                            list.Add(sdr["MJNumber"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return list;
        }
        internal static bool DeleteInvoiceDetails(filterValues1 inputtext, string param)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            string Query = @"";
            Query = @"S_Get_ImportMJInvoiceReport";
            bool success = false;
            try
            {
                cmd = new SqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@PONumber", inputtext.PoNumber);
                cmd.Parameters.AddWithValue("@InvoiceNumber", inputtext.InvoiceNo);
                cmd.Parameters.AddWithValue("@Param", param);

                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        success = Convert.ToBoolean(rdr["DeleteFlag"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return success;
        }
        internal static List<string> getPONumber(string poNum)
        {
            List<string> result = new List<string>();

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlDataReader dr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_ImportMJInvoiceReport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PONumber", poNum);
                cmd.Parameters.AddWithValue("@Param", "POList");
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["PONumber"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (dr != null) dr.Close();
            }
            return result;
        }
        internal static List<string> InvoiceAccess1(string invNum)
        {
            List<string> result = new List<string>();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string Query = @"";
            SqlDataReader dr = null;
            Query = @"S_Get_ImportMJInvoiceReport";
            try
            {
                cmd = new SqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@PONumber", invNum);
                cmd.Parameters.AddWithValue("@Param", "InvoiceList");
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["InvoiceNumber"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (dr != null) dr.Close();
            }
            return result;
        }
        internal static DataTable InvoiceCheck(InvNo inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            //SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<dataListInvoice> list = new List<dataListInvoice>();
            DataTable dt = new DataTable();
            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_ImportMJInvoiceReport", con);
                cmd.Parameters.AddWithValue("@InvoiceNumber", inputtext.Invoice);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return dt;
        }
        internal static DataTable POCheckInvoice(Pono inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_ImportMJInvoiceReport", con);
                cmd.Parameters.AddWithValue("@PONumber", inputtext.PoNo);
                cmd.Parameters.AddWithValue("@InvoiceNumber", "");
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return dt;
        }
        internal static bool SaveInvoiceDataTodb(DataRow dtrow)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string Query = @"";
            Query = @"S_Get_ImportMJInvoiceReport";
            bool success = false;
            try
            {
                cmd = new SqlCommand(Query, conn);
                DateTime now = DateTime.Now;
                //string s = dtrow["MJNo"].ToString();
                cmd.Parameters.AddWithValue("@InvoiceNumber", dtrow["InvoiceNumber"].ToString());
                cmd.Parameters.AddWithValue("@Invoicedate", dtrow["Invoicedate"].ToString());
                cmd.Parameters.AddWithValue("@Suppliercode", dtrow["Suppliercode"].ToString());
                cmd.Parameters.AddWithValue("@ProductionName", dtrow["ProductionName"].ToString());
                cmd.Parameters.AddWithValue("@ProductDescription", dtrow["ProductDescription"].ToString());
                cmd.Parameters.AddWithValue("@Quantity", dtrow["Quantity"].ToString());
                cmd.Parameters.AddWithValue("@Unit", dtrow["Unit"].ToString());
                cmd.Parameters.AddWithValue("@InvoiceValue", dtrow["InvoiceValue"].ToString());
                cmd.Parameters.AddWithValue("@PONumber", dtrow["PONumber"].ToString());
                DateTime.TryParse(dtrow["PODate"].ToString(), out now);
                cmd.Parameters.AddWithValue("@PODate", now.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.Parameters.AddWithValue("@Param", "Save");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }
        internal static bool VerifyInvoice(DataTable dt)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            string Query = @"";
            Query = @"S_Get_ImportMJInvoiceReport";
            bool success = false;
            try
            {
                cmd = new SqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@PONumber", dt.Rows[0]["PONumber"].ToString());
                cmd.Parameters.AddWithValue("@InvoiceNumber", dt.Rows[0]["InvoiceNumber"].ToString());
                cmd.Parameters.AddWithValue("@Param", "Existance");
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        success = Convert.ToBoolean(rdr["Flag"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(" VerifyInvoice:" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return success;
        }
        internal static bool UpdateInvoice(Invoicetable parameter)
        {
            bool success = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"[S_Get_ImportMJInvoiceReport]";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "Update");
                cmd.Parameters.AddWithValue("@InvoiceNumber", parameter.invoiceno);
                cmd.Parameters.AddWithValue("@PONumber", parameter.pono);
                cmd.Parameters.AddWithValue("@ProductionName", parameter.production);
                cmd.Parameters.AddWithValue("@MJNumber", parameter.mjno);
                cmd.Parameters.AddWithValue("@Customer", parameter.customer);

                int val = cmd.ExecuteNonQuery();
                if (val > 0)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }
        internal static void SavePdfFile(Invoicetable parameter)
        {
            // bool success = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"[S_Get_ImportMJInvoiceReport]";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "Update");
                cmd.Parameters.AddWithValue("@InvoiceNumber", parameter.invoiceno);
                cmd.Parameters.AddWithValue("@PONumber", parameter.pono);
                cmd.Parameters.AddWithValue("@ProductionName", parameter.production);
                //cmd.Parameters.AddWithValue("@MJNumber", parameter.mjno);
                //cmd.Parameters.AddWithValue("@Customer", parameter.customer);
                cmd.Parameters.AddWithValue("@Attachment_FilePath", parameter.FileUpload);
                cmd.Parameters.AddWithValue("@attachmentfile", parameter.attachedFile);

                cmd.ExecuteNonQuery();
                //if (val > 0)
                // {
                // success = true;
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            // return success;
        }

        #endregion

        #region "-----Dispatch Details -----------"
        internal static List<DispatchDetails1> getDispatchDetailValue(FilterDisDetails inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            List<DispatchDetails1> list = new List<DispatchDetails1>();
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_DispatchDetailsReport", con);
                cmd.Parameters.AddWithValue("@PONumber", inputtext.PoNo);
                cmd.Parameters.AddWithValue("@InvoiceNumber", inputtext.InvoiceNo);
                cmd.Parameters.AddWithValue("@StartDate", inputtext.FromDate);
                cmd.Parameters.AddWithValue("@EndDate", inputtext.ToDate);

                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            DispatchDetails1 dispatchDetails = new DispatchDetails1();
                            dispatchDetails.PoNumber = sdr["PoNumber"].ToString();
                            if (sdr["PoDate"] == DBNull.Value)
                            {
                                dispatchDetails.PODate = "";
                            }
                            else
                            {
                                dispatchDetails.PODate = Convert.ToDateTime(sdr["PoDate"]).ToString("yyyy-MM-dd");
                            }
                            dispatchDetails.InvoiceNo = sdr["InvoiceNumber"].ToString();
                            if (sdr["Invoicedate"] == DBNull.Value)
                            {
                                dispatchDetails.InvoiceDate = "";
                            }
                            else
                            {
                                dispatchDetails.InvoiceDate = Convert.ToDateTime(sdr["Invoicedate"]).ToString("yyyy-MM-dd");
                            }
                            dispatchDetails.Customer = sdr["CustomerName"].ToString();
                            dispatchDetails.Region = sdr["Region"].ToString();
                            dispatchDetails.InvoiceValue = sdr["InvoiceValue"].ToString();
                            dispatchDetails.ProdName = sdr["ProductName"].ToString();
                            dispatchDetails.ConsignName = sdr["ConsignmentName"].ToString();
                            dispatchDetails.CourierName = sdr["CourierName"].ToString();
                            dispatchDetails.Content = sdr["Content"].ToString();
                            dispatchDetails.Email = sdr["Email"].ToString();
                            dispatchDetails.Status = sdr["Status"].ToString();
                            if (sdr["DeliveryDate"] == DBNull.Value)
                            {
                                dispatchDetails.DeliveryDate = "";
                            }
                            else
                            {
                                dispatchDetails.DeliveryDate = Convert.ToDateTime(sdr["DeliveryDate"]).ToString("yyyy-MM-dd");
                            }
                            dispatchDetails.RecievedBy = sdr["ReceivedBy"].ToString();
                            dispatchDetails.FileUpload = sdr["Attachment"].ToString();
                            if (sdr["attachmentfile"].ToString() == "" || sdr["attachmentfile"] == DBNull.Value)
                            {
                                dispatchDetails.AttachedFile = null;
                            }
                            else
                            {
                                if (sdr["Attachment"].ToString().EndsWith(".pdf"))
                                {
                                    dispatchDetails.AttachedFile = (byte[])sdr["attachmentfile"];
                                    byte[] bytes = (byte[])sdr["attachmentfile"];
                                    dispatchDetails.AttachmentFileBase64 = Convert.ToBase64String(bytes);
                                }
                                else
                                {
                                    byte[] bytes = (byte[])sdr["attachmentfile"];
                                    dispatchDetails.AttachedFile = bytes;
                                    string strBase64 = Convert.ToBase64String(bytes);
                                    dispatchDetails.AttachmentFile = "data:Image/png;base64," + strBase64;
                                    dispatchDetails.AttachmentFileBase64 = Convert.ToBase64String(bytes);
                                }
                            }
                            list.Add(dispatchDetails);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("getDispatchDetailValue:" + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting Purchase details" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static List<string> getPONO(string param, string name)
        {
            List<string> result = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_DispatchDetailsReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@PONumber", name);
                cmd.Parameters.AddWithValue("@InvoiceNumber", name);
                //cmd.Parameters.AddWithValue("@Customer", name);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result.Add(sdr["PoNumber"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("getPONO" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }
        internal static DataTable getAddablePoNo(string param)
        {

            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt3 = new DataTable();
            SqlDataReader sdr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_DispatchDetailsReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                sdr = cmd.ExecuteReader();
                dt3.Load(sdr);
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("getAddablePoNo: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return dt3;
        }
        internal static List<string> getInvoiceNo(string param, string name)
        {
            List<string> result = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_DispatchDetailsReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@PONumber", name);

                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result.Add(sdr["InvoiceNumber"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("getInvoiceNo" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }
        internal static DataTable getNewDisDetails(string param, string ponum, string invNo)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_DispatchDetailsReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@PONumber", ponum);
                cmd.Parameters.AddWithValue("@InvoiceNumber", invNo);

                sdr = cmd.ExecuteReader();
                dt1.Load(sdr);
                dt2.Load(sdr);
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("getNewDisDetails" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return dt2;
        }
        internal static void saveUpdateDispatch(DispatchDetails1 inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            List<DispatchDetails1> list = new List<DispatchDetails1>();
            try
            {
                DateTime now = DateTime.Now;
                SqlCommand cmd = new SqlCommand("S_Get_DispatchDetailsReport", con);
                cmd.Parameters.AddWithValue("@PONumber", inputtext.PoNumber);
                cmd.Parameters.AddWithValue("@InvoiceNumber", inputtext.InvoiceNo);
                cmd.Parameters.AddWithValue("@Customer", inputtext.Customer);
                if (inputtext.PODate == "" || inputtext.PODate == null)
                {
                    cmd.Parameters.AddWithValue("@PODate", DBNull.Value);
                }
                else
                {
                    DateTime.TryParseExact(inputtext.PODate.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out now);
                    //DateTime.TryParse(inputtext.InvoiceDate, out now);
                    Logger.WriteDebugLog(now.ToString());
                    cmd.Parameters.AddWithValue("@PODate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (inputtext.InvoiceDate == "" || inputtext.InvoiceDate == null)
                {
                    cmd.Parameters.AddWithValue("@Invoicedate", DBNull.Value);
                }
                else
                {
                    DateTime.TryParseExact(inputtext.InvoiceDate.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out now);
                    //DateTime.TryParse(inputtext.InvoiceDate, out now);
                    Logger.WriteDebugLog(now.ToString());
                    cmd.Parameters.AddWithValue("@Invoicedate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                cmd.Parameters.AddWithValue("@region", inputtext.Region);
                cmd.Parameters.AddWithValue("@InvoiceValue", inputtext.InvoiceValue);
                cmd.Parameters.AddWithValue("@Productname", inputtext.ProdName);
                cmd.Parameters.AddWithValue("@ConsignmentName", inputtext.ConsignName);
                cmd.Parameters.AddWithValue("@CourierName", inputtext.CourierName);
                cmd.Parameters.AddWithValue("@Content", inputtext.Content);
                cmd.Parameters.AddWithValue("@Email", inputtext.Email);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                if (inputtext.DeliveryDate == "" || inputtext.DeliveryDate == null)
                {
                    cmd.Parameters.AddWithValue("@DeliveryDate", DBNull.Value);
                }
                else
                {
                    DateTime.TryParse(inputtext.DeliveryDate, out now);
                    cmd.Parameters.AddWithValue("@DeliveryDate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                cmd.Parameters.AddWithValue("@ReceivedBy", inputtext.RecievedBy);
                cmd.Parameters.AddWithValue("@Attachment", inputtext.FileUpload);
                cmd.Parameters.AddWithValue("@attachmentfile", inputtext.AttachedFile);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveUpdateDispatch: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }
        #endregion

        #region "------ Planned Down Time ---------"

        internal static List<HolidayListDetails> getHolidayList(string value, string value2, string reason, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<HolidayListDetails> listParameter = new List<HolidayListDetails>();
            HolidayListDetails parameter = null;
            try
            {
                if (param == "ByDate")
                {
                    string fromdateout, todateout;
                    fromdateout = GetPhysicalDate(value, value2, out todateout);

                    cmd = new SqlCommand("select * from HolidayList where Holiday>=@value and Holiday<=@toDate and (Reason=@reason or ISNULL(@reason,'')='')   order by Holiday", con);

                    cmd.Parameters.AddWithValue("@value", Util.GetDateTime(fromdateout).ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@toDate", Util.GetDateTime(todateout).ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@reason", reason);
                }
                else if (param == "ByReason")
                {
                    cmd = new SqlCommand("select * from HolidayList where  (Reason=@value or ISNULL(@value,'')='')    order by Holiday", con);
                    cmd.Parameters.AddWithValue("@value", value);
                }

                else
                {
                    cmd = new SqlCommand("select * from HolidayList  order by Holiday", con);
                }

                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            parameter = new HolidayListDetails();
                            if (sdr["Holiday"] != null)
                            {
                                if (sdr["Holiday"].ToString() != "")
                                {
                                    //DateTime date = DateTime.Now.Date;
                                    //string s = sdr["Holiday"].ToString();
                                    //date = Util.GetDateTime(sdr["Holiday"].ToString());
                                    //parameter.Holiday = date.ToString("dd-MMM-yyyy");
                                    parameter.Holiday = convertDateTimeToSpecificFormat(sdr["Holiday"].ToString());
                                    parameter.Date = Util.GetDateTime(sdr["Holiday"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    parameter.Holiday = sdr["Holiday"].ToString();
                                    parameter.Date = sdr["Holiday"].ToString();
                                }

                            }
                            else
                            {
                                parameter.Holiday = sdr["Holiday"].ToString();
                            }

                            parameter.Reason = sdr["Reason"].ToString();
                            listParameter.Add(parameter);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog("Error in getHolidayList- " + ex.Message);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error in getAuditDateDetails - " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return listParameter;
        }
        internal static string GetPhysicalDate(string fromdateinput, string todateinput, out string toDate)
        {
            string fromDate = "";
            toDate = "";
            try
            {
                DateTime startdate;
                startdate = Util.GetDateTime(fromdateinput.Trim());
                TimeSpan time = new TimeSpan(0, 0, 0);
                DateTime combinedStartDate = startdate.Add(time);
                fromDate = combinedStartDate.ToString("yyyy-MM-dd HH:mm:ss");
                // DateTime enddate = Convert.ToDateTime(txtToDate.Text);
                DateTime enddate;
                enddate = Util.GetDateTime(todateinput.Trim());
                DateTime combinedEndDate = enddate.AddDays(1).AddTicks(-1);
                toDate = combinedEndDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            return fromDate;
        }
        internal static List<string> getAllReasons()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> listParameter = new List<string>();
            try
            {
                cmd = new SqlCommand("select distinct Reason from HolidayList", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            listParameter.Add(sdr["Reason"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog("Error in getAllReasons- " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error in getAllReasons - " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return listParameter;
        }
        public static int deletePORawDataRecords()
        {
            int recordAffected = 0;
            string cmdStr = System.String.Format("delete from TempPlannedDownTimes");
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            SqlCommand command = new SqlCommand(cmdStr, sqlConn);
            command.CommandType = System.Data.CommandType.Text;
            try
            {
                recordAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.ToString());
            }
            finally
            {
                if (sqlConn != null)
                    sqlConn.Close();
            }
            return recordAffected;
        }
        public static List<PDTData> savePDTDetails(DataTable dt, string param)
        {
            List<PDTData> pdtissuedetails = new List<PDTData>();
            SqlBulkCopy bulkCopy = default(SqlBulkCopy);
            SqlConnection con = ConnectionManager.GetConnection();
            // string conString = WebConfigurationManager.ConnectionStrings["ConString"].ToString();
            try
            {
                bulkCopy = new SqlBulkCopy(con);
                bulkCopy.BulkCopyTimeout = 300;
                bulkCopy.DestinationTableName = "[dbo].[TempPlannedDownTimes]";
                bulkCopy.ColumnMappings.Add("StartTime", "StartTime");
                bulkCopy.ColumnMappings.Add("EndTime", "EndTime");
                bulkCopy.ColumnMappings.Add("DownReason", "DownReason");
                bulkCopy.ColumnMappings.Add("DownType", "DownType");
                bulkCopy.ColumnMappings.Add("DayName", "DayName");
                bulkCopy.NotifyAfter = 20;
                bulkCopy.SqlRowsCopied += delegate (object sender, SqlRowsCopiedEventArgs e)
                {
                    Logger.WriteDebugLog(string.Format("Row insertion Notifed : {0} rows copied to Table dbo.PO_RawData .", e.RowsCopied));
                };

                bulkCopy.WriteToServer(dt);
                if (bulkCopy != null) bulkCopy.Close();
                if (con != null) con.Close();
                pdtissuedetails = getPDTIssueDetails(param);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(string.Format("Exception in ProcessAlarmFile() method. Message :{0}", ex.ToString()));
            }
            finally
            {
                if (bulkCopy != null) bulkCopy.Close();

            }
            return pdtissuedetails;
        }
        public static List<PDTData> getPDTIssueDetails(string param)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            List<PDTData> list = new List<PDTData>();
            PDTData data = null;
            SqlCommand command = new SqlCommand("[dbo].[s_GetDownReasons]", sqlConn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Param", param);
            command.CommandTimeout = 360;
            try
            {
                sdr = command.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        data = new PDTData();
                        data.FromDateTime = sdr["StartTime"].ToString();
                        data.ToDateTime = sdr["EndTime"].ToString();
                        data.Reason = sdr["DownReason"].ToString();
                        list.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.ToString());
            }
            finally
            {
                if (sqlConn != null)
                    sqlConn.Close();
            }
            return list;
        }
        internal static int deleteHolidayDetails(string query)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            int result = 0;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result = -2;
                Logger.WriteErrorLog("Error in deleteHolidayDetails - " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return result;
        }
        internal static List<string> getPDTDownReason(string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            try
            {
                cmd = new SqlCommand(@"select distinct DownReason from PlannedDownTimes where DownType=@param", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@param", param);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(sdr["DownReason"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error in getDailyDownDetails - " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static List<PDTData> getWeeklyOffDetails(string fromDate, string toDate, string reason, string viewType)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<PDTData> list = new List<PDTData>();
            PDTData data = null;
            try
            {
                if (viewType == "ByDate")
                {
                    cmd = new SqlCommand(@"select * from PlannedDownTimes where DownType='weeklyoff'  and StartTime>=@fromdate and EndTime<=@toDate and (DownReason=@reason or ISNULL(@reason,'')='')   order by StartTime", con);

                }
                else if (viewType == "ByReason")
                {
                    cmd = new SqlCommand(@"select * from PlannedDownTimes where DownType='weeklyoff'  and (DownReason=@reason or ISNULL(@reason,'')='') order by StartTime", con);
                }

                else
                {

                    cmd = new SqlCommand(@"select * from PlannedDownTimes where DownType='weeklyoff'  order by StartTime", con);

                }
                string fromdateout, todateout;
                fromdateout = GetPhysicalDate(fromDate, toDate, out todateout);
                cmd.Parameters.AddWithValue("@fromdate", Util.GetDateTime(fromdateout).ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@toDate", Util.GetDateTime(todateout).ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@reason", reason);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            data = new PDTData();
                            DateTime fromd = Util.GetDateTime(sdr["StartTime"].ToString());
                            string s1 = sdr["StartTime"].ToString();
                            string str = fromd.ToString("dd-MMM-yyyy");
                            data.FromDateTime = Util.GetDateTime(Convert.ToDateTime(sdr["StartTime"].ToString()).ToString("yyyy-MM-dd")).ToString("dd-MMM-yyyy");
                            data.ToDateTime = sdr["EndTime"].ToString();
                            data.FromDateTime1 = sdr["StartTime"].ToString();
                            data.ToDateTime1 = sdr["EndTime"].ToString();
                            data.Reason = sdr["DownReason"].ToString();
                            data.Day = Util.GetDateTime(data.FromDateTime).ToString("dddd");
                            data.ID = sdr["ID"].ToString();
                            list.Add(data);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog("Error in getWeeklyOff details- " + ex.Message);
                        }
                    }
                }
                //else
                //{
                //    data = new PDTData();
                //    list.Add(data);
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error in getWeeklyOffDetails - " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static int deletePDTDetails(string id, bool isHolidayData)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            int result = 0;
            SqlCommand cmd = null;
            try
            {
                if (isHolidayData)
                {

                    cmd = new SqlCommand(@"delete from HolidayList where Holiday in (" + id + ")", con);
                }
                else
                {
                    cmd = new SqlCommand(@"delete from PlannedDownTimes  where ID in (" + id + ")", con);
                }
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result = -2;
                Logger.WriteErrorLog("Error in deleteShiftDownDetails - " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return result;
        }
        internal static List<PDTData> getAllDownDetails(DateTime fromDate, DateTime toDate, string downtype)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<PDTData> list = new List<PDTData>();
            PDTData data = null;
            try
            {
                if (downtype == "Holidays")
                {
                    cmd = new SqlCommand(@"select * from HolidayList where Holiday>=@fromdate and Holiday<=@toDate   order by Holiday", con);
                }
                else
                {
                    cmd = new SqlCommand(@"select * from PlannedDownTimes where StartTime>=@fromdate and StartTime<=@toDate and DownType=@downtype  order by StartTime", con);
                }
                cmd.Parameters.AddWithValue("@fromdate", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@toDate", toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@downtype", downtype);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            data = new PDTData();
                            if (downtype == "Holidays")
                            {

                                data.FromDateTime = Util.GetDateTime(Convert.ToDateTime(sdr["Holiday"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")).ToString("dd-MMM-yyyy HH:mm:ss");
                                data.Reason = sdr["Reason"].ToString();
                                data.DownType = "Holidays";
                                data.ID = sdr["Holiday"].ToString();
                            }
                            else
                            {
                                data.FromDateTime = Util.GetDateTime(Convert.ToDateTime(sdr["StartTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")).ToString("dd-MMM-yyyy HH:mm:ss") + " - " + Util.GetDateTime(Convert.ToDateTime(sdr["EndTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")).ToString("dd-MMM-yyyy HH:mm:ss");
                                data.ToDateTime = sdr["EndTime"].ToString();
                                data.Reason = sdr["DownReason"].ToString();
                                data.DownType = sdr["DownType"].ToString();
                                data.ID = sdr["ID"].ToString();
                            }
                            list.Add(data);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog("Error in getWeeklyOff details- " + ex.Message);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error in getAuditDateDetails - " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        private static string convertDateTimeToSpecificFormat(string datetime)
        {

            string formateddatetime = "";
            try
            {
                formateddatetime = Convert.ToDateTime(datetime).ToString("dd-MM-yyyy");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            return formateddatetime;
        }
        internal static List<MenuShowHide> getMenuListForLoginUser(string company, string userid, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            List<MenuShowHide> list = new List<MenuShowHide>();
            MenuShowHide data = null;
            try
            {


                if (HttpContext.Current.Session["UserRole"].ToString() == "superadmin")
                {
                    string query = "";
                    if (HttpContext.Current.Session["MTBorCutomerView"].ToString() == "MTBView")
                    {
                        if (param == "ScreenView")
                        {
                            query = "select * from ModuleScreenDetails where MTBView=1";
                        }
                        else
                        {
                            query = "select distinct Module,'' as Screen from ModuleScreenDetails";
                        }
                    }
                    else
                    {
                        if (param == "ScreenView")
                        {
                            query = "select * from ModuleScreenDetails where CustomerView = 1";
                        }
                        else
                        {
                            query = "select distinct Module,'' as Screen from ModuleScreenDetails";
                        }

                    }
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            data = new MenuShowHide();
                            data.Module = sdr["Module"].ToString();
                            data.Screen = sdr["Screen"].ToString();
                            data.Value = "Modify";
                            data.Visible = true;
                            list.Add(data);
                        }
                    }
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(@"[s_Masters_TPM_GetMenuBasedOnUserRights]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyId", company);
                    cmd.Parameters.AddWithValue("@UserID", userid);
                    cmd.Parameters.AddWithValue("@Param", param);

                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            data = new MenuShowHide();
                            data.Module = sdr["Module"].ToString();
                            data.Screen = sdr["Screen"].ToString();
                            data.Value = sdr[3].ToString();
                            if (data.Value == "Read" || data.Value == "Modify")
                            {
                                data.Visible = true;
                            }
                            else
                            {
                                data.Visible = false;
                            }
                            list.Add(data);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getMenuListForLoginUser - " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) { sdr.Close(); }
            }
            return list;
        }
        internal static void ShowHideColumnOfGrid(GridView gridView, bool isVisible, string columnName)
        {
            foreach (DataControlField col in gridView.Columns)
            {
                if (col.HeaderText == columnName)
                {
                    col.Visible = isVisible;
                }
            }
        }
        internal static int saveUserPreferences(ColumnList inputdata)
        {
            SqlConnection con = ConnectionManager.GetConnection();

            int success = 0;
            try
            {
                string userid = HttpContext.Current.Session["Username"].ToString();
                SqlCommand cmd = new SqlCommand("[S_GetUserPreferences]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", inputdata.CompanyID);
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@Module", inputdata.Module);
                cmd.Parameters.AddWithValue("@Screen", inputdata.Screen);
                cmd.Parameters.AddWithValue("@objectType", inputdata.ObjectType);
                cmd.Parameters.AddWithValue("@FieldName", inputdata.FieldName);
                cmd.Parameters.AddWithValue("@FieldDisplayName", inputdata.FieldDisplayName);
                cmd.Parameters.AddWithValue("@FieldOrder", inputdata.FieldOrder);
                cmd.Parameters.AddWithValue("@FieldVisibility", inputdata.FieldVisibility);
                cmd.Parameters.AddWithValue("@Param", "Insert");
                success = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveUserPreferences - " + ex.Message);
                success = -1;
            }
            finally
            {
                if (con != null) con.Close();
            }
            return success;
        }
        internal static List<ColumnList> getUserPreferences(string company, string module, string screen)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            List<ColumnList> list = new List<ColumnList>();
            ColumnList data = null;
            try
            {
                string userid = HttpContext.Current.Session["Username"].ToString();
                SqlCommand cmd = new SqlCommand("[S_GetUserPreferences]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", company);
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@Module", module);
                cmd.Parameters.AddWithValue("@Screen", screen);
                cmd.Parameters.AddWithValue("@Param", "View");
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        data = new ColumnList();
                        data.FieldName = sdr["FieldName"].ToString();
                        data.FieldOrder = sdr["FieldOrder"] == null || sdr["FieldOrder"] == DBNull.Value || sdr["FieldOrder"].ToString() == "" ? 0 : Convert.ToInt16(sdr["FieldOrder"].ToString());
                        data.DataValueField = data.FieldName + "," + data.FieldOrder;
                        data.FieldDisplayName = sdr["FieldDisplayName"].ToString();
                        if (sdr["FieldVisibility"].ToString() == "1" || sdr["FieldVisibility"].ToString() == "True")
                        {
                            data.FieldVisibility = true;
                        }
                        else
                        {
                            data.FieldVisibility = false;
                        }

                        list.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getUserPreferences - " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) { sdr.Close(); }
            }
            return list;
        }

        #endregion

        #region"-----Packing List--------"


        #region"---------KitMaster Details------------"
        internal static List<KitMaster1> GetKitDetails(string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            List<KitMaster1> list = new List<KitMaster1>();
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_Kit_Item_MasterDetails", con);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        KitMaster1 kitM = new KitMaster1();
                        kitM.KitNo = sdr["KitNo"].ToString();
                        kitM.KitName = sdr["KitName"].ToString();
                        list.Add(kitM);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetKitDetails: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static string InsertUpdateKitMaster(string kitno, string kitname, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            string sucess = "";
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_Kit_Item_MasterDetails", con);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@KitNo", kitno);
                cmd.Parameters.AddWithValue("@KitName", kitname);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        sucess = sdr["SaveFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("InsertUpdateKitMaster: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return sucess;
        }
        internal static string DeleteKitMaster(KitMaster1 inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            string sucess = "";
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_Kit_Item_MasterDetails", con);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@KitNo", inputtext.KitNo);
                cmd.Parameters.AddWithValue("@KitName", inputtext.KitName);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        sucess = sdr["DeleteFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("DeleteKitMaster: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return sucess;
        }
        #endregion

        #region "--------Item Master Details-----------"
        internal static List<ItemMaster> GetItemMaster(string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            List<ItemMaster> list = new List<ItemMaster>();
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_Kit_Item_MasterDetails", con);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        ItemMaster ItemM = new ItemMaster();
                        ItemM.ItemNo = sdr["ItemNo"].ToString();
                        ItemM.ItemName = sdr["ItemName"].ToString();
                        ItemM.ItemDescription = sdr["ItemDescription"].ToString();
                        ItemM.IsAccessories = (bool)sdr["IsAccessories"];
                        ItemM.partNo = sdr["PartNo"].ToString();
                        ItemM.supplierName = sdr["SupplierName"].ToString();
                        list.Add(ItemM);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetItemMaster: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static string InsertUpdateItemMaster(ItemMaster inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            string sucess = "";
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_Kit_Item_MasterDetails", con);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@ItemNo", inputtext.ItemNo);
                cmd.Parameters.AddWithValue("@ItemName", inputtext.ItemName);
                cmd.Parameters.AddWithValue("@ItemDescription", inputtext.ItemDescription);
                cmd.Parameters.AddWithValue("@IsAccessories", inputtext.IsAccessories);
                cmd.Parameters.AddWithValue("@SupplierName", inputtext.supplierName);
                cmd.Parameters.AddWithValue("@PartNo", inputtext.partNo);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        sucess = sdr["SaveFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("InsertUpdateItemMaster: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return sucess;
        }
        internal static string DeleteItemMaster(ItemMaster inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            string sucess = "";
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_Kit_Item_MasterDetails", con);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@ItemNo", inputtext.ItemNo);
                cmd.Parameters.AddWithValue("@ItemName", inputtext.ItemName);
                cmd.Parameters.AddWithValue("@ItemDescription", inputtext.ItemDescription);
                cmd.Parameters.AddWithValue("@IsAccessories", inputtext.IsAccessories);
                cmd.Parameters.AddWithValue("@SupplierName", inputtext.supplierName);
                cmd.Parameters.AddWithValue("@PartNo", inputtext.partNo);


                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        sucess = sdr["DeleteFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("DeleteItemMaster: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return sucess;
        }
        #endregion

        #region"----------BOM Details------------"
        internal static string AddBOMdetails(BOMDetails inputtext)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            string success = null;
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_BOM_Details", conn);
                cmd.Parameters.AddWithValue("@KitNo", inputtext.kitno);
                cmd.Parameters.AddWithValue("@KitName", inputtext.kitname);
                cmd.Parameters.AddWithValue("@ItemName", inputtext.itemName);
                cmd.Parameters.AddWithValue("@ItemNo", inputtext.itemNo);
                cmd.Parameters.AddWithValue("@ItemDescription", inputtext.ItemDescription);
                cmd.Parameters.AddWithValue("@IsAccessories", inputtext.isAccessories);
                cmd.Parameters.AddWithValue("@SupplierName", inputtext.supplierName);
                cmd.Parameters.AddWithValue("@PartNo", inputtext.partNo);
                cmd.Parameters.AddWithValue("@ItemQty", inputtext.quantity);
                cmd.Parameters.AddWithValue("@Param", inputtext.param);
                cmd.CommandType = CommandType.StoredProcedure;

                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = sdr["SaveFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("AddBOMdetails: " + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }
        internal static List<KitMaster1> GetKitList(string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            List<KitMaster1> list = new List<KitMaster1>();
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_BOM_Details", con);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;

                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        KitMaster1 kitM = new KitMaster1();
                        kitM.KitNo = sdr["KitNo"].ToString();
                        kitM.KitName = sdr["KitName"].ToString();
                        list.Add(kitM);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetKitList:" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static List<BOMDetails> GetItemList(string inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            List<BOMDetails> list = new List<BOMDetails>();
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_BOM_Details", con);
                cmd.Parameters.AddWithValue("@KitName", inputtext);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        BOMDetails ItemM = new BOMDetails();
                        ItemM.itemNo = sdr["ItemNo"].ToString();
                        ItemM.itemName = sdr["ItemName"].ToString();
                        ItemM.ItemDescription = sdr["ItemDescription"].ToString();
                        if ((ItemM.IsSelected = sdr["Checked"].ToString()) == "1")
                        {
                            ItemM.SelectedCheck = true;
                        }
                        else
                        {
                            ItemM.SelectedCheck = false;
                        }
                        ItemM.isAccessories = (bool)sdr["IsAccessories"];
                        ItemM.partNo = sdr["PartNo"].ToString();
                        ItemM.supplierName = sdr["SupplierName"].ToString();
                        ItemM.quantity = sdr["ItemQty"].ToString();
                        list.Add(ItemM);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetItemList:" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        #endregion

        #region"-----POAssociate Details-------"
        internal static List<string> GetPONO(string param)
        {
            List<string> result = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_Customer_PO_BOM_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result.Add(sdr["PoNumber"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("GetPONO:" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }


        internal static List<string> GetCustomerName(string param)
        {
            List<string> result = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_Customer_PO_BOM_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result.Add(sdr["Customer"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("GetCustomerName: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }
        internal static DataTable GetPOKitDetails(string param, string Customer, string PoNo)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_Customer_PO_BOM_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@Customer", Customer);
                cmd.Parameters.AddWithValue("@PoNumber", PoNo);

                sdr = cmd.ExecuteReader();
                dt.Load(sdr);
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("GetPOKitDetails: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return dt;
        }
        internal static List<POKitAccess> GetKitLMasterDetails(string param, string customer, string pono)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            List<POKitAccess> list = new List<POKitAccess>();

            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_Customer_PO_BOM_Details", con);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@Customer", customer);
                cmd.Parameters.AddWithValue("@PoNumber", pono);
                cmd.CommandType = CommandType.StoredProcedure;

                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        POKitAccess kitM = new POKitAccess();
                        kitM.Kitno = sdr["KitNo"].ToString();
                        kitM.Kitname = sdr["KitName"].ToString();
                        kitM.KQty = sdr["KitQty"].ToString();
                        if ((kitM.IsSelected = sdr["Checked"].ToString()) == "1")
                        {
                            kitM.Selectedkit = true;
                        }
                        else
                        {
                            kitM.Selectedkit = false;
                        }
                        list.Add(kitM);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetKitLMasterDetails: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static string SaveKitDetails(POKitAccess inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string success = null;
            try
            {
                cmd = new SqlCommand("S_Get_Customer_PO_BOM_Details", con);
                cmd.Parameters.AddWithValue("@Param", inputtext.param);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@KitNo", inputtext.Kitno);
                cmd.Parameters.AddWithValue("@Customer", inputtext.cust);
                cmd.Parameters.AddWithValue("@PoNumber", inputtext.pono);
                cmd.Parameters.AddWithValue("@KitName", inputtext.Kitname);
                cmd.Parameters.AddWithValue("@KitQty", inputtext.KQty);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = sdr["SaveFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SaveKitDetails: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return success;
        }
        internal static string SaveCustomerItemDetails(POItemAccess inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string success = null;
            try
            {
                cmd = new SqlCommand("S_Get_Customer_PO_BOM_Details", con);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Customer", inputtext.Cust);
                cmd.Parameters.AddWithValue("@PoNumber", inputtext.Pono);
                cmd.Parameters.AddWithValue("@KitNo", inputtext.KitNo);
                cmd.Parameters.AddWithValue("@KitName", inputtext.Kitname);
                cmd.Parameters.AddWithValue("@KitQty", inputtext.KitQty);
                cmd.Parameters.AddWithValue("@ItemName", inputtext.Itemname);
                cmd.Parameters.AddWithValue("@ItemNo", inputtext.ItemNo);
                cmd.Parameters.AddWithValue("@ItemDescription", inputtext.ItemDesc);
                cmd.Parameters.AddWithValue("@IsAccessories", inputtext.IsAccessories);
                cmd.Parameters.AddWithValue("@SupplierName", inputtext.SName);
                cmd.Parameters.AddWithValue("@PartNo", inputtext.PartNo);
                cmd.Parameters.AddWithValue("@ItemQty", inputtext.ItemQty);
                cmd.Parameters.AddWithValue("@Qty", inputtext.Qty);
                cmd.Parameters.AddWithValue("@Shortage", inputtext.shortage);

                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = sdr["SaveFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SaveCustomerItemDetails: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return success;
        }
        internal static string RequestAprrovalStatus(POItemAccess inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string success = null;
            try
            {
                cmd = new SqlCommand("S_Get_Customer_PO_BOM_Details", con);
                cmd.Parameters.AddWithValue("@Param", inputtext.param);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Customer", inputtext.Cust);
                cmd.Parameters.AddWithValue("@PoNumber", inputtext.Pono);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@ApproveRequestedBy", inputtext.ApproveRequestBy);
                cmd.Parameters.AddWithValue("@ApproveRequestedTS", Convert.ToDateTime(inputtext.ApproveRequestTS));
                cmd.Parameters.AddWithValue("@ApprovedBy", inputtext.ApprovedBy);
                cmd.Parameters.AddWithValue("@ApprovedTS", Convert.ToDateTime(inputtext.ApprovedTS));
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = sdr["SaveFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("RequestAprrovalStatus: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return success;
        }
        internal static DataTable GetEmployeeId(string UserName)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            DataTable dt = new DataTable();
            try
            {
                string query = "select Email,Employeeid from Employee_Information";
                SqlCommand cmd = new SqlCommand(query, con);
                sdr = cmd.ExecuteReader();
                dt.Load(sdr);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetKitLMasterDetails: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return dt;
        }
        internal static List<string> GetPONumber(DropDownValues inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            //DataTable dt = new DataTable();
            string query = null;
            // proposalEntryDetails proposalValue = null;
            try
            {
                query = "select PONumber from PurchaseOrderDetails where Customer=@Customer and region=@Region";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Region", inputtext.Region);
                cmd.Parameters.AddWithValue("@Customer", inputtext.Customer);
                cmd.CommandType = CommandType.Text;

                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sdr = cmd.ExecuteReader();
                // sda.Fill(dt);
                if(sdr.HasRows)
                {
                    while(sdr.Read())
                    {
                        list.Add(sdr["PONumber"].ToString());
                    }
                   
                }
                
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting ProposalEntry details" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return list;
        }
        #endregion

        #endregion

        #region "---Proposal Entry && Purchase order-----"
        internal static List<string> GetCustName(string param, string name)
        {
            List<string> result = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;
            try
            {
                string query = "select distinct Customer from Customer where Customer LIKE ''+@Customer+'%'";
                cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@Customer", name);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result.Add(sdr["Customer"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }
        internal static List<ProposalEntryDetails> getProposalEntry(DropDownValues inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<ProposalEntryDetails> list = new List<ProposalEntryDetails>();
            ProposalEntryDetails proposalValue = null;
            try
            {
                cmd = new SqlCommand("S_Get_ProposalReport", con);
                //cmd.Parameters.AddWithValue("@input", inputtext);
                cmd.Parameters.AddWithValue("@Region", inputtext.Region);
                cmd.Parameters.AddWithValue("@Customer", inputtext.Customer);
                cmd.Parameters.AddWithValue("@Owner", inputtext.Owner);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@StartDate", inputtext.FromDate);
                cmd.Parameters.AddWithValue("@EndDate", inputtext.ToDate);
                cmd.Parameters.AddWithValue("@KeyField", inputtext.KeyField);


                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            proposalValue = new ProposalEntryDetails();
                            proposalValue.Region = sdr["Region"].ToString();
                            proposalValue.Customer = sdr["Customer"].ToString();
                            proposalValue.ProposalNumber = sdr["ProposalNumber"].ToString();
                            proposalValue.ProposalOwner = sdr["ProposalOwner"].ToString();
                            proposalValue.ProposalVersion = sdr["ProposalVersion"].ToString();
                            proposalValue.Status = sdr["Status"].ToString();
                            if (sdr["SubmittedDate"] == DBNull.Value)
                            {
                                proposalValue.SubmittedDate = "";
                            }
                            else
                            {
                                //proposalValue.SubmittedDate = sdr["SubmittedDate"].ToString();
                                proposalValue.SubmittedDate = Convert.ToDateTime(sdr["SubmittedDate"]).ToString("yyyy-MM-dd");
                            }
                            if (sdr["ProposalDate"] == DBNull.Value)
                            {
                                proposalValue.ProposalDate = "";
                            }
                            else
                            {
                                proposalValue.ProposalDate = Convert.ToDateTime(sdr["ProposalDate"]).ToString("yyyy-MM-dd");
                            }
                            proposalValue.KeyField = sdr["KeyField"].ToString();
                            if (sdr["StatusAsOn"] == DBNull.Value)
                            {
                                proposalValue.StatusAsOn = "";
                            }
                            else
                            {
                                proposalValue.StatusAsOn = Convert.ToDateTime(sdr["StatusAsOn"]).ToString("yyyy-MM-dd");
                            }
                            proposalValue.ProposalValue = sdr["ProposalValue"].ToString();
                            proposalValue.FileUpload = sdr["Attachment"].ToString();
                            proposalValue.FIlePath = sdr["Attachment_FilePath"].ToString();
                            //proposalValue.attachmentfile = sdr["attachmentfile"].ToString();
                            if (sdr["attachmentfile"].ToString() == "" || sdr["attachmentfile"] == DBNull.Value)
                            {
                                proposalValue.attachedFile = null;
                            }
                            else
                            {
                                if ((sdr["Attachment"].ToString().EndsWith(".pdf")))
                                {
                                    proposalValue.attachedFile = (byte[])sdr["attachmentfile"];
                                    byte[] bytes = (byte[])sdr["attachmentfile"];
                                    proposalValue.AttachmentFileBase64 = Convert.ToBase64String(bytes);

                                    //var abc = (byte[])sdr["attachmentfile"];
                                    //var aaa = System.Convert.ToBase64String(abc);
                                    //proposalValue.attachmentfile = aaa;
                                }
                                else
                                {
                                    byte[] bytes = (byte[])sdr["attachmentfile"];
                                    proposalValue.attachedFile = bytes;
                                    string strBase64 = Convert.ToBase64String(bytes);
                                    proposalValue.attachmentfile = "data:Image/png;base64" + strBase64;
                                    proposalValue.AttachmentFileBase64 = Convert.ToBase64String(bytes);
                                }
                            }

                            list.Add(proposalValue);

                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog(ex.Message);
                        }

                    }

                }
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting ProposalEntry details" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static DataTable getExportValues(DropDownValues inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            // SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<ProposalEntryDetails> list = new List<ProposalEntryDetails>();
            DataTable dt = new DataTable();
            // proposalEntryDetails proposalValue = null;
            try
            {


                cmd = new SqlCommand("S_Get_ProposalReport", con);
                //cmd.Parameters.AddWithValue("@input", inputtext);
                cmd.Parameters.AddWithValue("@Region", inputtext.Region);
                cmd.Parameters.AddWithValue("@Customer", inputtext.Customer);
                cmd.Parameters.AddWithValue("@Owner", inputtext.Owner);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@StartDate", inputtext.FromDate);
                cmd.Parameters.AddWithValue("@EndDate", inputtext.ToDate);

                cmd.CommandType = CommandType.StoredProcedure;

                // cmd.ExecuteReader();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting ProposalEntry details" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return dt;
        }
        internal static DataTable[] getDropDownValue(string param, out DataTable dtDropD1, out DataTable dtDropD2, out DataTable dtDropD3, out DataTable dtDropD4, out DataTable dtDropD5)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            dtDropD1 = new DataTable();
            dtDropD2 = new DataTable();
            dtDropD3 = new DataTable();
            dtDropD4 = new DataTable();
            dtDropD5 = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_ProposalReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);

                SqlDataReader sda = null;
                sda = cmd.ExecuteReader();

                dtDropD1.Load(sda);
                dtDropD2.Load(sda);
                dtDropD3.Load(sda);
                dtDropD4.Load(sda);
                dtDropD5.Load(sda);

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return new DataTable[] { dtDropD1, dtDropD2, dtDropD3, dtDropD4, dtDropD5 };

        }
        internal static DataTable[] getDropDownValue1(string param, out DataTable dtDropD1, out DataTable dtDropD2, out DataTable dtDropD3, out DataTable dtDropD4, out DataTable dtDropD5)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            dtDropD1 = new DataTable();
            dtDropD2 = new DataTable();
            dtDropD3 = new DataTable();
            dtDropD4 = new DataTable();
            dtDropD5 = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_ProposalReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);

                SqlDataReader sda = null;
                sda = cmd.ExecuteReader();

                dtDropD1.Load(sda);
                dtDropD2.Load(sda);
                dtDropD3.Load(sda);
                dtDropD4.Load(sda);
                dtDropD5.Load(sda);

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return new DataTable[] { dtDropD1, dtDropD2, dtDropD3, dtDropD4, dtDropD5 };

        }


        internal static List<string> getRegionValues(string name)
        {
            List<string> result = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            try
            {
                string query = "select * from Customer where Customer=@Customer";
                cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@Customer", name);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result.Add(sdr["Region"].ToString());

                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }
        internal static List<string> getPORegion()
        {
            List<string> result = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            try
            {
                string query = "select Region from Region";
                cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result.Add(sdr["Region"].ToString());

                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }
        internal static void InsertUpdateGridView(ProposalEntryDetails inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            List<ProposalEntryDetails> list = new List<ProposalEntryDetails>();
            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_ProposalReport", con);
                cmd.Parameters.AddWithValue("@Region", inputtext.Region);
                cmd.Parameters.AddWithValue("@Customer", inputtext.Customer);
                cmd.Parameters.AddWithValue("@ProposalOwner", inputtext.ProposalOwner);
                cmd.Parameters.AddWithValue("@ProsalNumber", inputtext.ProposalNumber);
                cmd.Parameters.AddWithValue("@ProposalVersion", inputtext.ProposalVersion);
                if (inputtext.ProposalDate == "" || inputtext.ProposalDate == null)
                {

                    cmd.Parameters.AddWithValue("@ProposalDate", DBNull.Value);
                }
                else
                {
                    DateTime.TryParse(inputtext.ProposalDate, out now);
                    cmd.Parameters.AddWithValue("@ProposalDate", now.ToString("yyyy-MM-dd HH:mm:ss"));

                }
                cmd.Parameters.AddWithValue("@ProposalValue", inputtext.ProposalValue);
                if (inputtext.SubmittedDate == "" || inputtext.SubmittedDate == null)
                {
                    cmd.Parameters.AddWithValue("@SubmittedDate", DBNull.Value);
                }
                else
                {
                    DateTime.TryParse(inputtext.SubmittedDate, out now);
                    cmd.Parameters.AddWithValue("@SubmittedDate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                if (inputtext.StatusAsOn == "" || inputtext.StatusAsOn == null)
                {
                    cmd.Parameters.AddWithValue("@StatusAsOn", DBNull.Value);
                }
                else
                {
                    DateTime.TryParse(inputtext.StatusAsOn, out now);
                    cmd.Parameters.AddWithValue("@StatusAsOn", now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                cmd.Parameters.AddWithValue("@KeyField", inputtext.KeyField);
                cmd.Parameters.AddWithValue("@Attachment", inputtext.FileUpload);
                cmd.Parameters.AddWithValue("@AttachmentPath", inputtext.FIlePath);
                cmd.Parameters.AddWithValue("@attachmentfile", inputtext.attachedFile);
                cmd.Parameters.AddWithValue("@Param", param);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting ProposalEntry details" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }
        internal static List<purchaseOrder> getPurchaseDetails(DropDownValues inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<purchaseOrder> list = new List<purchaseOrder>();
            purchaseOrder poDetails = null;
            try
            {
                cmd = new SqlCommand("S_Get_PurchaseOrderReport", con);
                cmd.Parameters.AddWithValue("@Region", inputtext.Region);
                cmd.Parameters.AddWithValue("@Customer", inputtext.Customer);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@StartDate", inputtext.FromDate);
                cmd.Parameters.AddWithValue("@EndDate", inputtext.ToDate);

                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            poDetails = new purchaseOrder();
                            poDetails.Region = sdr["Region"].ToString();
                            poDetails.Customer = sdr["Customer"].ToString();
                            poDetails.POnumber = sdr["PONumber"].ToString();
                            if (sdr["PODate"] == DBNull.Value)
                            {
                                poDetails.POdate = "";
                            }
                            else
                            {
                                poDetails.POdate = Convert.ToDateTime(sdr["PODate"]).ToString("yyyy-MM-dd");
                            }
                            poDetails.POvalue = sdr["POValue"].ToString();
                            poDetails.QuoteRef = sdr["QuoteReference"].ToString();
                            poDetails.Status = sdr["Status"].ToString();
                            if (sdr["StatusAsOn"] == DBNull.Value)
                            {
                                poDetails.StatusAsOn = "";
                            }
                            else
                            {
                                poDetails.StatusAsOn = Convert.ToDateTime(sdr["StatusAsOn"]).ToString("yyyy-MM-dd");
                            }
                            poDetails.tallyPOnumber = sdr["TallyPoNumber"].ToString();
                            if (sdr["TallyPoDate"] == DBNull.Value)
                            {
                                poDetails.tallyPOdate = "";
                            }
                            else
                            {
                                poDetails.tallyPOdate = Convert.ToDateTime(sdr["TallyPoDate"]).ToString("yyyy-MM-dd");
                            }
                            poDetails.Attachment = sdr["Attachment"].ToString();
                            if (sdr["attachmentfile"].ToString() == "" || sdr["attachmentfile"] == DBNull.Value)
                            {
                                poDetails.attachedFile = null;
                            }
                            else
                            {
                                if (sdr["Attachment"].ToString().EndsWith(".pdf"))
                                {
                                    poDetails.attachedFile = (byte[])sdr["attachmentfile"];
                                    byte[] bytes = (byte[])sdr["attachmentfile"];
                                    poDetails.AttachmentFileBase64 = Convert.ToBase64String(bytes);
                                }
                                else
                                {
                                    byte[] bytes = (byte[])sdr["attachmentfile"];
                                    poDetails.attachedFile = bytes;
                                    string strBase64 = Convert.ToBase64String(bytes);
                                    poDetails.attachmentfile = "data:Image/png;base64," + strBase64;
                                    poDetails.AttachmentFileBase64 = Convert.ToBase64String(bytes);
                                }
                            }

                            list.Add(poDetails);

                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog(ex.Message);
                        }

                    }

                }
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting Purchase details" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static void saveUpdatePurchase(purchaseOrder inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            List<purchaseOrder> list = new List<purchaseOrder>();
            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_PurchaseOrderReport", con);
                cmd.Parameters.AddWithValue("@Region", inputtext.Region);
                cmd.Parameters.AddWithValue("@Customer", inputtext.Customer);
                cmd.Parameters.AddWithValue("@PONumber", inputtext.POnumber);
                if (inputtext.POdate == "" || inputtext.POdate == null)
                {
                    cmd.Parameters.AddWithValue("@PODate", DBNull.Value);
                }
                else
                {
                    DateTime.TryParse(inputtext.POdate, out now);
                    cmd.Parameters.AddWithValue("@PODate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                cmd.Parameters.AddWithValue("@POValue", inputtext.POvalue);
                cmd.Parameters.AddWithValue("@QuoteReference", inputtext.QuoteRef);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                if (inputtext.StatusAsOn == "" || inputtext.StatusAsOn == null)
                {
                    cmd.Parameters.AddWithValue("@StatusAsOn", DBNull.Value);
                }
                else
                {
                    DateTime.TryParse(inputtext.StatusAsOn, out now);
                    cmd.Parameters.AddWithValue("@StatusAsOn", now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                cmd.Parameters.AddWithValue("@TallyPoNumber", inputtext.tallyPOnumber);
                if (inputtext.tallyPOdate == "" || inputtext.tallyPOdate == null)
                {
                    cmd.Parameters.AddWithValue("@TallyPoDate", DBNull.Value);
                }
                else
                {
                    DateTime.TryParse(inputtext.tallyPOdate, out now);
                    cmd.Parameters.AddWithValue("@TallyPoDate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                cmd.Parameters.AddWithValue("@Attachment", inputtext.FileUpload);
                cmd.Parameters.AddWithValue("@attachmentfile", inputtext.attachedFile);
                cmd.Parameters.AddWithValue("@Param", param);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting Purchase details" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }
        internal static bool DeletePO(purchaseOrder inputtext, string param)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            bool success = false;
            try
            {
                cmd = new SqlCommand("S_Get_PurchaseOrderReport", conn);
                cmd.Parameters.AddWithValue("@PONumber", inputtext.POnumber);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        success = Convert.ToBoolean(rdr["DeleteFlag"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return success;
        }
        internal static DataTable[] getFilterValues(string param, out DataTable dtDropD1, out DataTable dtDropD2, out DataTable dtDropD3)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            dtDropD1 = new DataTable();
            dtDropD2 = new DataTable();
            dtDropD3 = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand("S_Get_PurchaseOrderReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);

                SqlDataReader sda = null;
                sda = cmd.ExecuteReader();

                dtDropD1.Load(sda);
                dtDropD2.Load(sda);
                dtDropD3.Load(sda);
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return new DataTable[] { dtDropD1, dtDropD2, dtDropD3 };
        }
        internal static DataTable getExportedOrders(DropDownValues inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            List<purchaseOrder> list = new List<purchaseOrder>();
            DataTable dt = new DataTable();
            try
            {


                cmd = new SqlCommand("S_Get_PurchaseOrderReport", con);
                cmd.Parameters.AddWithValue("@Region", inputtext.Region);
                cmd.Parameters.AddWithValue("@Customer", inputtext.Customer);
                cmd.Parameters.AddWithValue("@Status", inputtext.Status);
                cmd.Parameters.AddWithValue("@StartDate", inputtext.FromDate);
                cmd.Parameters.AddWithValue("@EndDate", inputtext.ToDate);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting ProposalEntry details" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
            return dt;
        }
        internal static string getSelectedTallyDate(string selectedValue)
        {
            string result = "";
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            try
            {
                cmd = new SqlCommand("select PoDate from PurchaseOrderMaster where PoNumber=@PoNumber", con);
                cmd.Parameters.AddWithValue("@PoNumber", selectedValue);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result = Convert.ToDateTime(sdr["PODate"]).ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting ProposalEntry details" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }
        internal static List<string> getQuoteRefvalue(string param, string name)
        {
            List<string> result = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();

            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("S_Get_PurchaseOrderReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@Customer", name);
                sdr = cmd.ExecuteReader();
                dt1.Load(sdr);
                dt2.Load(sdr);
                dt3.Load(sdr);
                while (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        dt4.Load(sdr);
                        for (int i = 0; i <= dt4.Rows.Count; i++)
                        {
                            result.Add(Convert.ToString(dt4.Rows[i]["Column1"]));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }
        #endregion

        #region "---------Task Detaiks-------"
        internal static int InsertTaskDetailsGridview(Taskdetails inputtext, string param)
        {

            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            List<Taskdetails> list = new List<Taskdetails>();
            int Result = 0;
            try
            {
                DateTime now = DateTime.Now;
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@IDD", inputtext.Id);
                //cmd.Parameters.AddWithValue("@WeekNo", inputtext.Weekno);
                //cmd.Parameters.AddWithValue("@YearNo", inputtext.Year);
                //cmd.Parameters.AddWithValue("@ProjectID", inputtext.Projectid);
                //cmd.Parameters.AddWithValue("@TaskType", inputtext.Tasktype);
                cmd.Parameters.AddWithValue("@MainTask", inputtext.Maintask);
                inputtext.Estimatedeffort = inputtext.Estimatedeffort.Replace(':', '.');
                if (inputtext.Estimatedeffort == "" || inputtext.Estimatedeffort == null)
                {
                    cmd.Parameters.AddWithValue("@EstimatedEffort ", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Estimatedeffort", Convert.ToDouble(inputtext.Estimatedeffort));
                }
                cmd.Parameters.AddWithValue("@Status", inputtext.MaintaskStatus);
                if (inputtext.DeliveryDate == "" || inputtext.DeliveryDate == null)
                {

                    cmd.Parameters.AddWithValue("@DeliveryDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DeliveryDate", Convert.ToDateTime(inputtext.DeliveryDate));
                    //    //DateTime.TryParse(inputtext.DeliveryDate, out now);
                    //cmd.Parameters.AddWithValue("@DeliveryDate", now.ToString("dd-MM-yyyy"));
                }
                //Add MaintaskStatus here
                //cmd.Parameters.AddWithValue("@AssignedTo", inputtext.Assignedto);
                //cmd.Parameters.AddWithValue("@ProblemType", inputtext.Request);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                Result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("InsertTaskDetailsGridview:" + ex.Message);
            }
            return Result;
        }

        internal static string InsertTaskDetailsGridview1(Taskdetails inputtext, string param)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            string success = "";
            try
            {
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", conn);
                cmd.Parameters.AddWithValue("@IDD", inputtext.Id);
                cmd.Parameters.AddWithValue("@AssignedTo", inputtext.Assignedto);
                cmd.Parameters.AddWithValue("@ProblemType", inputtext.Request);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.Projectid);
                cmd.Parameters.AddWithValue("@TaskType", inputtext.Tasktype);
                cmd.Parameters.AddWithValue("@MainTask", inputtext.Maintask);
                cmd.Parameters.AddWithValue("@Estimatedeffort", inputtext.Estimatedeffort);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        success = "Deleted";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return success;

        }

        internal static List<Taskdetails> getTaskEntry(Taskdetails inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            List<Taskdetails> list = new List<Taskdetails>();
            Taskdetails taskdtls = null;
            try
            {
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.Projectid);
                //cmd.Parameters.AddWithValue("@WeekNo", inputtext.Weekno);
                //cmd.Parameters.AddWithValue("@YearNo", inputtext.Year);
                //cmd.Parameters.AddWithValue("@IDD", inputtext.Id);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            taskdtls = new Taskdetails();
                            taskdtls.Id = sdr["IDD"].ToString();
                            taskdtls.Projectid = sdr["ProjectID"].ToString();
                            //taskdtls.Weekno = sdr["WeekNo"].ToString();
                            //taskdtls.Year = sdr["YearNo"].ToString();
                            //taskdtls.Tasktype = sdr["TaskType"].ToString();
                            //taskdtls.TaskTypeName= HelperClass.TaskTypeDisplayName(sdr["TaskType"].ToString());
                            taskdtls.Maintask = sdr["MainTask"].ToString();
                            //taskdtls.Request = sdr["ProblemType"].ToString();
                            //taskdtls.RequestName= HelperClass.IssueTypeDisplayName(sdr["ProblemType"].ToString());
                            //taskdtls.Maintask = sdr["MainTask"].ToString();
                            taskdtls.Estimatedeffort = sdr["EstimatedEffort"].ToString().Replace(".", ":");
                            if (sdr["DeliveryDate"] == DBNull.Value)
                            {
                                taskdtls.DeliveryDate = "";
                            }
                            else
                            {
                                taskdtls.DeliveryDate = Convert.ToDateTime(sdr["DeliveryDate"]).ToString("dd-MM-yyyy");
                            }
                            taskdtls.MaintaskStatus = sdr["Status"].ToString();
                            //taskdtls.Assignedto = sdr["AssignedTo"].ToString();
                            //taskdtls.ProblemID = sdr["ProblemID"].ToString();
                            list.Add(taskdtls);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(ex.Message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("Error" + e.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }

        internal static List<string> GetAssignedValues()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;

            List<string> list = new List<string>();
            try
            {
                cmd = new SqlCommand("select distinct(Employeeid) from Employee_Information", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    list.Add(sdr["Employeeid"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetAssignedValues" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static string GetMaintaskIdd(Subtaskdetails subtask)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd;

            string maintaskid = "";

            try
            {
                cmd = new SqlCommand("Select IDD from Project_MainTask_MasterDetails where ProjectID=@ProjectID and MainTask= @MainTask", con);
                cmd.Parameters.AddWithValue("@ProjectID", subtask.Projectid);
                cmd.Parameters.AddWithValue("@MainTask", subtask.Maintask);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    maintaskid = sdr["IDD"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetMaintaskIdd: " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }

            return maintaskid;
        }
        internal static List<Subtaskdetails> GetMaintaskValues(Subtaskdetails name)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<Subtaskdetails> list = new List<Subtaskdetails>();
            Subtaskdetails subtask = null;
            //List<string> list = new List<string>();
            try
            {
                cmd = new SqlCommand("select MainTask,IDD from Project_MainTask_MasterDetails where ProjectID=@ProjectID", con);
                cmd.Parameters.AddWithValue("@ProjectID", name.Projectid);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    subtask = new Subtaskdetails();
                    subtask.MainTaskIDD = sdr["MainTask"].ToString();
                    subtask.Maintask = sdr["IDD"].ToString();
                    list.Add(subtask);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetMaintaskValues" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static DataTable GetMaintaskValues1(Subtaskdetails name,out DataTable dt1)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            dt1 = new DataTable();
            try
            {
                cmd = new SqlCommand("select IDD,MainTask from Project_MainTask_MasterDetails where ProjectID=@ProjectID", con);
                cmd.Parameters.AddWithValue("@ProjectID", name.Projectid);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        dt1.Load(sdr);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetMaintaskValues" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                //if (sdr != null) sdr.Close();
            }
            return dt1; 
        }
        internal static EmpDetails GetEmployeeemaildetails(string name)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            EmpDetails empDetails = null;
            try
            {
                cmd = new SqlCommand("select * from Employee_Information where Employeeid=@AssignedTo", con);
                cmd.Parameters.AddWithValue("@AssignedTo", name);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    empDetails = new EmpDetails();
                    empDetails.EmpID = sdr["Employeeid"].ToString();
                    empDetails.EmpName = sdr["Name"].ToString();
                    empDetails.Email = sdr["Email"].ToString();
                    empDetails.Password = sdr["upassword"].ToString();
                    empDetails.Password.Equals("*");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetEmployeeemaildetails" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return empDetails;
        }
        #endregion

        #region"-----------subtaskdetails-------------"
        internal static List<Subtaskdetails> GetSubtaskentry(Subtaskdetails inputtext)
        {
            List<Subtaskdetails> list = new List<Subtaskdetails>();
            Subtaskdetails subtask = null;
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            try
            {
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@Projectid", inputtext.Projectid);
                cmd.Parameters.AddWithValue("@WeekNo", inputtext.Weekno);
                cmd.Parameters.AddWithValue("@YearNo", inputtext.Year);
                //cmd.Parameters.AddWithValue("@MainTaskIDD", inputtext.MainTaskIDD);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        subtask = new Subtaskdetails();
                        subtask.Id = sdr["IDD"].ToString();
                        subtask.Weekno = sdr["WeekNo"].ToString();
                        //subtask.Employees= sdr[""].ToString();
                        subtask.Projectid = sdr["ProjectID"].ToString();
                        subtask.Tasktype = sdr["TaskType"].ToString();
                        subtask.Maintask = sdr["MainTask"].ToString();
                        subtask.Subtask = sdr["SubTask"].ToString();
                        //subtask.ManualEntryRemark = sdr["ManualEntryRemarks"].ToString();
                        subtask.Estimatedeffortsub = sdr["EstimatedEffort"].ToString();
                        // subtask.RemarksEngineer = sdr[""].ToString();
                        //subtask.ViewMOM = sdr[""].ToString();
                        //subtask.SubtaskStatus = sdr["Status"].ToString();                        
                        //subtask.MaintaskStatus = sdr[""].ToString();
                        //subtask.ProjectStatus = sdr[""].ToString();                        
                        if (list.Count == 0)
                        {
                            //subtask.HeaderVisibility = true;
                        }
                        list.Add(subtask);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("GetSubtaskentry" + e.Message);
            }
            return list;
        }
        internal static int SaveSubTaskDetails(Subtaskdetails inputtext, string param)
        {
            int Result = 0;
            List<Subtaskdetails> list = new List<Subtaskdetails>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            try
            {
                DateTime now = new DateTime();
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@IDD", inputtext.Id);
                cmd.Parameters.AddWithValue("@Weekno", inputtext.Weekno);
                cmd.Parameters.AddWithValue("@YearNo", inputtext.Year);
                cmd.Parameters.AddWithValue("@Projectid", inputtext.Projectid);
                cmd.Parameters.AddWithValue("@Maintask", inputtext.Maintask);
                cmd.Parameters.AddWithValue("@SubTask", inputtext.Subtask);
                cmd.Parameters.AddWithValue("@Tasktype", inputtext.Tasktype);
                cmd.Parameters.AddWithValue("@AssignedTo", inputtext.Assignedto);
                cmd.Parameters.AddWithValue("@ProblemType", inputtext.Request);
                cmd.Parameters.AddWithValue("@Dependency", inputtext.Dependencies);
                cmd.Parameters.AddWithValue("@MainTaskIDD", inputtext.MainTaskIDD);
                //cmd.Parameters.AddWithValue("@ProblemID", inputtext.ProblemID);
                inputtext.Estimatedeffortsub = inputtext.Estimatedeffortsub.Replace(':', '.');
                if (inputtext.Estimatedeffortsub == "" || inputtext.Estimatedeffortsub == null)
                {
                    cmd.Parameters.AddWithValue("@EstimatedEffort ", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Estimatedeffort", Convert.ToDouble(inputtext.Estimatedeffortsub));
                }
                if (inputtext.DeliveryDate == "" || inputtext.DeliveryDate == null)
                {
                    cmd.Parameters.AddWithValue("@DeliveryDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DeliveryDate", Convert.ToDateTime(inputtext.DeliveryDate));
                }


                cmd.Parameters.AddWithValue("@Status", inputtext.SubtaskStatus);
                cmd.Parameters.AddWithValue("@ManualEntryRemarks", inputtext.ManualEntryRemark);

                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                Result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("SaveSubTaskDetails" + e.Message);
            }
            return Result;
        }
        internal static int InsertTaskDetailsGridview(Subtaskdetails inputtext, string param)
        {
            int Result = 0;
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            List<Subtaskdetails> subtask = new List<Subtaskdetails>();
            try
            {
                DateTime now = new DateTime();
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@IDD", inputtext.Id);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.Projectid);
                //cmd.Parameters.AddWithValue("@MainTaskIDD", inputtext.MainTaskIDD);
                cmd.Parameters.AddWithValue("@WeekNo", inputtext.Weekno);
                cmd.Parameters.AddWithValue("@YearNo", inputtext.Year);
                cmd.Parameters.AddWithValue("@TaskType", inputtext.Tasktype);
                cmd.Parameters.AddWithValue("@MainTask", inputtext.Maintask);
                cmd.Parameters.AddWithValue("@SubTask", inputtext.Subtask);
                //cmd.Parameters.AddWithValue("@Status", inputtext.SubtaskStatus);
                //cmd.Parameters.AddWithValue("@ManualEntryRemarks", inputtext.ManualEntryRemark);

                //cmd.Parameters.AddWithValue("@EstimatedEffort", inputtext.Estimatedeffortsub);
                inputtext.Estimatedeffortsub = inputtext.Estimatedeffortsub.Replace(':', '.');
                if (inputtext.Estimatedeffortsub == "" || inputtext.Estimatedeffortsub == null)
                {

                    cmd.Parameters.AddWithValue("@EstimatedEffort ", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Estimatedeffort", Convert.ToDouble(inputtext.Estimatedeffortsub));
                }
                ////if (inputtext.DeliveryDate == "" || inputtext.DeliveryDate == null)
                ////{

                ////    cmd.Parameters.AddWithValue("@DeliveryDate", DBNull.Value);
                ////}
                ////else
                ////{
                ////    DateTime.TryParse(inputtext.DeliveryDate, out now);
                ////    cmd.Parameters.AddWithValue("@DeliveryDate", now.ToString("yyyy-MM-dd HH:mm:ss"));
                ////}
                //inputtext.MainTaskEstimatedeffort = inputtext.MainTaskEstimatedeffort.Replace(':', '.');
                //if (inputtext.MainTaskEstimatedeffort == "" || inputtext.MainTaskEstimatedeffort == null)
                //{

                //    cmd.Parameters.AddWithValue("@MainTaskEstimatedEffort", DBNull.Value);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@MainTaskEstimatedEffort", Convert.ToDouble(inputtext.MainTaskEstimatedeffort));
                //}
                cmd.Parameters.AddWithValue("@ProblemType", inputtext.Request);
                cmd.Parameters.AddWithValue("@AssignedTo", inputtext.Assignedto);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                Result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("InsertTaskDetailsGridview" + e.Message);
            }
            return Result;
        }
        internal static string DeleteInsertSubTaskDetailsGridview(Subtaskdetails inputtext, string param)
        {
            string success = "";
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            List<Subtaskdetails> subtask = new List<Subtaskdetails>();
            try
            {
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@IDD", inputtext.Id);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = "Deleted";
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("DeleteInsertSubTaskDetailsGridview" + e.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }
        internal static List<string> GetddlWeekNumbers()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            List<string> str = new List<string>();
            try
            {
                cmd = new SqlCommand("select distinct WeekNumber from Calender order by WeekNumber", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    str.Add(sdr["WeekNumber"].ToString());
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("GetddlWeekNumbers" + e.Message);
            }
            return str;
        }
        internal static List<string> GetMainTaskStatus()
        {
            List<string> list1 = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand smd = null;
            try
            {
                smd = new SqlCommand("select Status from Status where Category='Maintask'", con);
                smd.CommandType = CommandType.Text;
                sdr = smd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list1.Add(sdr["Status"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e.Message);
            }
            return list1;
        }
        internal static List<string> GetTaskType()
        {
            List<string> list1 = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand smd = null;
            try
            {
                smd = new SqlCommand("select Status from Status where Category='TaskType'", con);
                smd.CommandType = CommandType.Text;
                sdr = smd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list1.Add(sdr["Status"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e.Message);
            }
            return list1;
        }
        internal static List<string> GetDependencies()
        {
            List<string> list1 = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand smd = null;
            try
            {
                smd = new SqlCommand("select Status from Status where Category='Dependency'", con);
                smd.CommandType = CommandType.Text;
                sdr = smd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list1.Add(sdr["Status"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e.Message);
            }
            return list1;
        }
        internal static List<string> GetRequest()
        {
            List<string> list1 = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand smd = null;
            try
            {
                smd = new SqlCommand("select Status from Status where Category='Request'", con);
                smd.CommandType = CommandType.Text;
                sdr = smd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list1.Add(sdr["Status"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e.Message);
            }
            return list1;
        }
        internal static DataTable GetEmployeeReportTo(String str, out DataTable dt1, out DataTable dt2)
        {
            dt1 = new DataTable();
            dt2 = new DataTable();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;

            try
            {
                cmd = new SqlCommand(@"IF ((select [Role] from Employee_Information where Employeeid=@Employeeid)='Team Manager')
BEGIN
	Select distinct Employeeid,[Name],[Role] from Employee_Information where ReportingTo=@Employeeid
	UNION
	Select distinct Employeeid,[Name],[Role] from Employee_Information 
	where ReportingTo in (Select distinct Employeeid from Employee_Information where ReportingTo=@Employeeid and [Role]='Team Leader')
	Order by Employeeid

	SELECT temp.idd,ProjectID,ProjectName,Split.a.value('.', 'VARCHAR(100)') AS 'Employeeid' 
	FROM (SELECT IDD,ProjectID,ProjectName,CAST('<M>' + REPLACE(A1.UserIDs, ',', '</M><M>') + '</M>' AS XML) AS Employeeid  
	FROM ProjectDetails A1
	) AS temp CROSS APPLY Employeeid.nodes('/M') AS Split(a)
	inner join (Select distinct Employeeid,[Name],[Role] from Employee_Information where ReportingTo=@Employeeid
				UNION
				Select distinct Employeeid,[Name],[Role] from Employee_Information 
				where ReportingTo in (Select distinct Employeeid from Employee_Information where ReportingTo=@Employeeid and [Role]='Team Leader')) E 
	on E.Employeeid=Split.a.value('.', 'VARCHAR(100)')
	Order by ProjectID,Employeeid
	return
END
Else 
IF ((select [Role] from Employee_Information where Employeeid=@Employeeid)='Team Leader')
BEGIN
	Select distinct Employeeid,[Name] from Employee_Information 
	where ReportingTo=@Employeeid

	SELECT temp.idd,ProjectID,ProjectName,Split.a.value('.', 'VARCHAR(100)') AS 'Employeeid' 
	FROM (SELECT IDD,ProjectID,ProjectName,CAST('<M>' + REPLACE(A1.UserIDs, ',', '</M><M>') + '</M>' AS XML) AS Employeeid  
	FROM ProjectDetails A1
	) AS temp CROSS APPLY Employeeid.nodes('/M') AS Split(a)
	inner join (Select distinct Employeeid,[Name] from Employee_Information
	where ReportingTo=@Employeeid) E 
	on E.Employeeid=Split.a.value('.', 'VARCHAR(100)'); 
	return
END", con);
                cmd.Parameters.AddWithValue("@Employeeid", str);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                dt1.Load(sdr);
                dt2.Load(sdr);

            }
            catch (Exception ex)
            {

            }
            return dt2;
        }
        internal static List<string> GetfootProjectID(string str)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            List<string> listprojectid = new List<string>();
            try
            {
                cmd = new SqlCommand("select ProjectID from ProjectDetails where UserIDs LIKE '%'+@UserIDs+'%'", con);
                cmd.Parameters.AddWithValue("@UserIDs", str);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        listprojectid.Add(sdr["ProjectID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listprojectid;
        }
        internal static string GetWeekNumbers(string date)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string weekno = "";
            try
            {
                cmd = new SqlCommand("select WeekNumber from Calender where Weekdate=@Weekdate", con);
                if (date == "" || date == null)
                {
                    cmd.Parameters.AddWithValue("@Weekdate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Weekdate", Convert.ToDateTime(date));
                }

                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    weekno = sdr["WeekNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetWeekNumbers" + ex.Message);
            }
            return weekno;
        }
        internal static string GetCurrentWeek(string date)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string weekno = "";
            try
            {
                cmd = new SqlCommand("select WeekNumber from Calender where Weekdate=@Weekdate", con);
                if (date == "" || date == null)
                {
                    cmd.Parameters.AddWithValue("@Weekdate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Weekdate", Convert.ToDateTime(date));
                }

                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    weekno = sdr["WeekNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetWeekNumbers" + ex.Message);
            }
            return weekno;
        }
        internal static String GetCurrentYear(string date)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string year = "";
            try
            {
                cmd = new SqlCommand("select YearNo from Calender where Weekdate=@Weekdate", con);
                if (date == "" || date == null)
                {
                    cmd.Parameters.AddWithValue("@Weekdate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Weekdate", Convert.ToDateTime(date));
                }

                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    year = sdr["YearNo"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetWeekNumbers" + ex.Message);
            }
            return year;
        }
        internal static List<string> GetfilterAssignedValues()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;

            List<string> list = new List<string>();
            try
            {
                cmd = new SqlCommand("select distinct(Employeeid) from Employee_Information", con);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    list.Add(sdr["Employeeid"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetAssignedValues" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static DataTable GetSubtaskentry(Subtaskdetails inputtext, out DataTable dt1, out DataTable dt2)
        {
            List<Subtaskdetails> list = new List<Subtaskdetails>();
            Subtaskdetails subtask = null;
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            dt1 = new DataTable();
            dt2 = new DataTable();
            try
            {
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@ProjectIDList", inputtext.Projectid);
                cmd.Parameters.AddWithValue("@WeekNo1", inputtext.Weekno);
                cmd.Parameters.AddWithValue("@AssignedToList", inputtext.Employees);
                cmd.Parameters.AddWithValue("@YearNo1", inputtext.Year);
                cmd.Parameters.AddWithValue("@WeekNo2", inputtext.Weekno1);
                //cmd.Parameters.AddWithValue("@strCustomerNameList", inputtext.Customer);
                //cmd.Parameters.AddWithValue("@Team", inputtext.Team);
                //cmd.Parameters.AddWithValue("@MainTaskIDD", inputtext.MainTaskIDD);
                cmd.Parameters.AddWithValue("@Param", inputtext.Param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                dt1.Load(sdr);
                dt2.Load(sdr);
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("GetSubtaskentry" + e.Message);
            }
            return dt1;
        }
        internal static string GetAssignedValues(Subtaskdetails subtask)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;

            string list = "";
            try
            {
                cmd = new SqlCommand("select distinct(UserIDs) from ProjectDetails where ProjectID=@ProjectID", con);
                cmd.Parameters.AddWithValue("ProjectID", subtask.Projectid);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    list = sdr["UserIDs"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetAssignedValues" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        #endregion

        #region"--------Transaction details------------"
        internal static string InsertTaskTransDetails(tasktransactiondetails inputtext, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            string success = "";
            try
            {
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.Projectid);
                cmd.Parameters.AddWithValue("@WeekNo", inputtext.Weekno);
                cmd.Parameters.AddWithValue("@YearNo", inputtext.Year);
                cmd.Parameters.AddWithValue("@TaskType", inputtext.TaskType);
                cmd.Parameters.AddWithValue("@MainTask", inputtext.Maintask);
                cmd.Parameters.AddWithValue("@SubTask", inputtext.Subtask);
                cmd.Parameters.AddWithValue("@EstimatedEffort", inputtext.SubTaskEstimatedeffort.Replace(':', '.'));
                cmd.Parameters.AddWithValue("@MainTaskEstimatedEffort", inputtext.MainTaskEstimatedeffort.Replace(':', '.'));
                cmd.Parameters.AddWithValue("@ProblemType", inputtext.Request);
                cmd.Parameters.AddWithValue("@AssignedTo", inputtext.AssignedTo);
                inputtext.Spenthour = inputtext.Spenthour.Replace(':', '.');
                if (inputtext.Spenthour != "")
                {
                    cmd.Parameters.AddWithValue("@SpentHours", Convert.ToDouble(inputtext.Spenthour));
                }

                cmd.Parameters.AddWithValue("@Remarks", inputtext.Remarks);
                cmd.Parameters.AddWithValue("@TDate", HelperClass.GetDateTime(inputtext.Date).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@MainTaskIDD", inputtext.MainTaskIDD);
                cmd.Parameters.AddWithValue("@SubTaskIDD ", inputtext.SubTaskIDD);
                cmd.Parameters.AddWithValue("@ProblemID", inputtext.ProblemID);
                cmd.Parameters.AddWithValue("@FileName", inputtext.FileName);
                cmd.Parameters.AddWithValue("@FileData", inputtext.File);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = sdr["SaveFlag"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("InsertTaskTransDetails" + e.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }
        internal static List<tasktransactiondetails> GetMemberTaskDetails(tasktransactiondetails inputtext, string dept, string param, out DataTable dtRemarksDetails)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            List<tasktransactiondetails> list = new List<tasktransactiondetails>();
            tasktransactiondetails taskvalue = null;
            dtRemarksDetails = new DataTable();
            try
            {
                cmd = new SqlCommand("S_Get_Project_Tasks_Details", con);
                cmd.Parameters.AddWithValue("@AssignedTo", inputtext.AssignedTo);
                cmd.Parameters.AddWithValue("@ProjectID", inputtext.Projectid);
                cmd.Parameters.AddWithValue("@WeekNo", inputtext.Weekno);
                cmd.Parameters.AddWithValue("@YearNo", inputtext.Year);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                dtRemarksDetails.Load(sdr);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        taskvalue = new tasktransactiondetails();

                        //taskvalue.Id = sdr["IDD"].ToString();

                        taskvalue.Projectid = sdr["ProjectID"].ToString();
                        taskvalue.Year = sdr["YearNo"].ToString();
                        taskvalue.Weekno = sdr["WeekNo"].ToString();
                        taskvalue.Maintask = sdr["MainTask"].ToString();
                        taskvalue.Subtask = sdr["SubTask"].ToString();
                        taskvalue.MainTaskIDD = sdr["MainTaskIDD"].ToString();
                        taskvalue.SubTaskIDD = sdr["SubTaskIDD"].ToString();
                        taskvalue.AssignedTo = sdr["AssignedTo"].ToString();

                        var taskdetails = dtRemarksDetails.AsEnumerable().Where(x => x.Field<dynamic>("MainTaskIDD").ToString() == taskvalue.MainTaskIDD && x.Field<dynamic>("SubTaskIDD").ToString() == taskvalue.SubTaskIDD).FirstOrDefault();

                        taskvalue.EstimatedEffort = taskdetails["EstimatedEffort"].ToString().Replace('.', ':');
                        taskvalue.Task = taskdetails["Task"].ToString();
                        taskvalue.TaskType = taskdetails["TaskType"].ToString();
                        taskvalue.SubtaskStatus = taskdetails["Status"].ToString();
                        taskvalue.MainTaskEstimatedeffort = taskdetails["MainTaskEstimatedEffort"].ToString().Replace('.', ':');
                        taskvalue.SubTaskEstimatedeffort = taskdetails["EstimatedEffort"].ToString().Replace('.', ':');
                        taskvalue.Request = taskdetails["ProblemType"].ToString();
                        if (dept == "Development Team")
                        {
                            taskvalue.Visible = true;
                            //taskvalue.HeaderVisibility = true;
                        }
                        taskvalue.ProblemID = taskdetails["ProblemID"].ToString();
                        int dayColNo = 8;

                        taskvalue.Day1Date = sdr.GetName(dayColNo);
                        if (list.Count == 0)
                        {
                            taskvalue.Day1 = Convert.ToDateTime(taskvalue.Day1Date).ToString("dd-MM");
                        }
                        taskvalue.Day1Value = sdr[taskvalue.Day1Date].ToString().Replace('.', ':');

                        if (taskvalue.Day1Value == "0")
                        {
                            taskvalue.Day1EditBtnVisibility = false;
                            taskvalue.Day1TextBoxEable = true;
                            taskvalue.Day1Value = "";
                        }

                        var details = dtRemarksDetails.AsEnumerable().Where(x => x.Field<string>("TDate") == taskvalue.Day1Date && x.Field<dynamic>("MainTaskIDD").ToString() == taskvalue.MainTaskIDD && x.Field<dynamic>("SubTaskIDD").ToString() == taskvalue.SubTaskIDD).FirstOrDefault();
                        if (details != null)
                        {
                            taskvalue.Day1Remarks = details["Remarks"].ToString();
                            taskvalue.Day1FileName = details["FileName"].ToString();
                            if (details["FileData"].ToString() == "")
                            {
                                taskvalue.Day1FileInBase64 = "";
                            }
                            else
                            {
                                byte[] bytes = (byte[])details["FileData"];
                                taskvalue.Day1bytefile = bytes;
                                taskvalue.Day1FileInBase64 = Convert.ToBase64String(bytes);
                            }
                        }

                        dayColNo++;

                        taskvalue.Day2Date = sdr.GetName(dayColNo);
                        if (list.Count == 0)
                        {
                            taskvalue.Day2 = Convert.ToDateTime(taskvalue.Day2Date).ToString("dd-MM");
                        }
                        taskvalue.Day2Value = sdr[taskvalue.Day2Date].ToString().Replace('.', ':');

                        if (taskvalue.Day2Value == "0")
                        {
                            taskvalue.Day2EditBtnVisibility = false;
                            taskvalue.Day2TextBoxEable = true;
                            taskvalue.Day2Value = "";
                        }
                        details = dtRemarksDetails.AsEnumerable().Where(x => x.Field<string>("TDate") == taskvalue.Day2Date && x.Field<dynamic>("MainTaskIDD").ToString() == taskvalue.MainTaskIDD && x.Field<dynamic>("SubTaskIDD").ToString() == taskvalue.SubTaskIDD).FirstOrDefault();
                        if (details != null)
                        {

                            taskvalue.Day2Remarks = details["Remarks"].ToString();
                            taskvalue.Day2FileName = details["FileName"].ToString();
                            if (details["FileData"].ToString() == "")
                            {
                                taskvalue.Day2FileInBase64 = "";
                            }
                            else
                            {
                                byte[] bytes = (byte[])details["FileData"];
                                taskvalue.Day2bytefile = bytes;
                                taskvalue.Day2FileInBase64 = Convert.ToBase64String(bytes);
                            }
                        }
                        dayColNo++;

                        taskvalue.Day3Date = sdr.GetName(dayColNo);
                        if (list.Count == 0)
                        {
                            taskvalue.Day3 = Convert.ToDateTime(taskvalue.Day3Date).ToString("dd-MM");
                        }
                        taskvalue.Day3Value = sdr[taskvalue.Day3Date].ToString().Replace('.', ':');

                        if (taskvalue.Day3Value == "0")
                        {
                            taskvalue.Day3EditBtnVisibility = false;
                            taskvalue.Day3TextBoxEable = true;
                            taskvalue.Day3Value = "";
                        }
                        details = dtRemarksDetails.AsEnumerable().Where(x => x.Field<string>("TDate") == taskvalue.Day3Date && x.Field<dynamic>("MainTaskIDD").ToString() == taskvalue.MainTaskIDD && x.Field<dynamic>("SubTaskIDD").ToString() == taskvalue.SubTaskIDD).FirstOrDefault();
                        if (details != null)
                        {
                            taskvalue.Day3Remarks = details["Remarks"].ToString();
                            taskvalue.Day3FileName = details["FileName"].ToString();
                            if (details["FileData"].ToString() == "")
                            {
                                taskvalue.Day3FileInBase64 = "";
                            }
                            else
                            {
                                byte[] bytes = (byte[])details["FileData"];
                                taskvalue.Day3bytefile = bytes;
                                taskvalue.Day3FileInBase64 = Convert.ToBase64String(bytes);
                            }
                        }
                        dayColNo++;

                        taskvalue.Day4Date = sdr.GetName(dayColNo);
                        if (list.Count == 0)
                        {
                            taskvalue.Day4 = Convert.ToDateTime(taskvalue.Day4Date).ToString("dd-MM");
                        }
                        taskvalue.Day4Value = sdr[taskvalue.Day4Date].ToString().Replace('.', ':');

                        if (taskvalue.Day4Value == "0")
                        {
                            taskvalue.Day4EditBtnVisibility = false;
                            taskvalue.Day4TextBoxEable = true;
                            taskvalue.Day4Value = "";
                        }
                        details = dtRemarksDetails.AsEnumerable().Where(x => x.Field<string>("TDate") == taskvalue.Day4Date && x.Field<dynamic>("MainTaskIDD").ToString() == taskvalue.MainTaskIDD && x.Field<dynamic>("SubTaskIDD").ToString() == taskvalue.SubTaskIDD).FirstOrDefault();
                        if (details != null)
                        {
                            taskvalue.Day4Remarks = details["Remarks"].ToString();
                            taskvalue.Day4FileName = details["FileName"].ToString();
                            if (details["FileData"].ToString() == "")
                            {
                                taskvalue.Day4FileInBase64 = "";
                            }
                            else
                            {
                                byte[] bytes = (byte[])details["FileData"];
                                taskvalue.Day1bytefile = bytes;
                                taskvalue.Day4FileInBase64 = Convert.ToBase64String(bytes);
                            }
                        }
                        dayColNo++;

                        taskvalue.Day5Date = sdr.GetName(dayColNo);
                        if (list.Count == 0)
                        {
                            taskvalue.Day5 = Convert.ToDateTime(taskvalue.Day5Date).ToString("dd-MM");
                        }
                        taskvalue.Day5Value = sdr[taskvalue.Day5Date].ToString().Replace('.', ':');

                        if (taskvalue.Day5Value == "0")
                        {
                            taskvalue.Day5EditBtnVisibility = false;
                            taskvalue.Day5TextBoxEable = true;
                            taskvalue.Day5Value = "";
                        }
                        details = dtRemarksDetails.AsEnumerable().Where(x => x.Field<string>("TDate") == taskvalue.Day5Date && x.Field<dynamic>("MainTaskIDD").ToString() == taskvalue.MainTaskIDD && x.Field<dynamic>("SubTaskIDD").ToString() == taskvalue.SubTaskIDD).FirstOrDefault();
                        if (details != null)
                        {
                            taskvalue.Day5Remarks = details["Remarks"].ToString();
                            taskvalue.Day5FileName = details["FileName"].ToString();
                            if (details["FileData"].ToString() == "")
                            {
                                taskvalue.Day5FileInBase64 = "";
                            }
                            else
                            {
                                byte[] bytes = (byte[])details["FileData"];
                                taskvalue.Day5bytefile = bytes;
                                taskvalue.Day5FileInBase64 = Convert.ToBase64String(bytes);
                            }
                        }
                        dayColNo++;

                        taskvalue.Day6Date = sdr.GetName(dayColNo);
                        if (list.Count == 0)
                        {
                            taskvalue.Day6 = Convert.ToDateTime(taskvalue.Day6Date).ToString("dd-MM");
                        }
                        taskvalue.Day6Value = sdr[taskvalue.Day6Date].ToString().Replace('.', ':');

                        if (taskvalue.Day6Value == "0")
                        {
                            taskvalue.Day6EditBtnVisibility = false;
                            taskvalue.Day6TextBoxEable = true;
                            taskvalue.Day6Value = "";
                        }
                        details = dtRemarksDetails.AsEnumerable().Where(x => x.Field<string>("TDate") == taskvalue.Day6Date && x.Field<dynamic>("MainTaskIDD").ToString() == taskvalue.MainTaskIDD && x.Field<dynamic>("SubTaskIDD").ToString() == taskvalue.SubTaskIDD).FirstOrDefault();
                        if (details != null)
                        {
                            taskvalue.Day6Remarks = details["Remarks"].ToString();
                            taskvalue.Day6FileName = details["FileName"].ToString();
                            if (details["FileData"].ToString() == "")
                            {
                                taskvalue.Day6FileInBase64 = "";
                            }
                            else
                            {
                                byte[] bytes = (byte[])details["FileData"];
                                taskvalue.Day6bytefile = bytes;
                                taskvalue.Day6FileInBase64 = Convert.ToBase64String(bytes);
                            }
                        }
                        dayColNo++;

                        taskvalue.Day7Date = sdr.GetName(dayColNo);
                        if (list.Count == 0)
                        {
                            taskvalue.Day7 = Convert.ToDateTime(taskvalue.Day7Date).ToString("dd-MM");
                        }
                        taskvalue.Day7Value = sdr[taskvalue.Day7Date].ToString().Replace('.', ':');

                        if (taskvalue.Day7Value == "0")
                        {
                            taskvalue.Day7EditBtnVisibility = false;
                            taskvalue.Day7TextBoxEable = true;
                            taskvalue.Day7Value = "";
                        }
                        details = dtRemarksDetails.AsEnumerable().Where(x => x.Field<string>("TDate") == taskvalue.Day7Date && x.Field<dynamic>("MainTaskIDD").ToString() == taskvalue.MainTaskIDD && x.Field<dynamic>("SubTaskIDD").ToString() == taskvalue.SubTaskIDD).FirstOrDefault();
                        if (details != null)
                        {
                            taskvalue.Day7Remarks = details["Remarks"].ToString();
                            taskvalue.Day7FileName = details["FileName"].ToString();
                            if (details["FileData"].ToString() == "")
                            {
                                taskvalue.Day7FileInBase64 = "";
                            }
                            else
                            {
                                byte[] bytes = (byte[])details["FileData"];
                                taskvalue.Day7bytefile = bytes;
                                taskvalue.Day7FileInBase64 = Convert.ToBase64String(bytes);
                            }
                        }
                        // taskvalue.Status = sdr["Status"].ToString();
                        if (list.Count == 0)
                        {
                            taskvalue.HeaderVisibility = true;
                        }
                        list.Add(taskvalue);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("GetMemberTaskDetails" + e.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }

        internal static List<string> SubtaskStatus()
        {
            List<string> list1 = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand smd = null;
            try
            {
                smd = new SqlCommand("select Status from Status where Category='SubTaskStatus'", con);
                smd.CommandType = CommandType.Text;
                sdr = smd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list1.Add(sdr["Status"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e.Message);
            }
            return list1;
        }

        internal static string UpdateSubtaskDetails(tasktransactiondetails tasktrans)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string Result = "";
            try
            {
                cmd = new SqlCommand("Update Project_SubTask_MasterDetails Set Status=@Status where IDD=@IDD", con);
                cmd.Parameters.AddWithValue("@IDD", tasktrans.SubTaskIDD);
                cmd.Parameters.AddWithValue("@Status", tasktrans.SubtaskStatus);
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = null;
                cmd.ExecuteNonQuery();
                Result = "Updated";
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("UpdateSubtaskDetails:" + ex.Message);
            }
            return Result;
        }
        #endregion

        #region


        internal static List<string> GetManageranmes(string teamleader)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            List<string> managerName = new List<string>();
            try
            {
                cmd = new SqlCommand("Select Employeeid from Employee_Information where Role=@Role", con);
                cmd.Parameters.AddWithValue("@Role", teamleader);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        managerName.Add(sdr["Employeeid"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetManageranmes" + ex.Message);
            }
            return managerName;
        }
        internal static List<WeeklyTaskReport> GetWeeklyReport(string employeeid, string weekno, string year)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<WeeklyTaskReport> weeklyTaskReports = new List<WeeklyTaskReport>();
            WeeklyTaskReport report = null;
            try
            {
                cmd = new SqlCommand("S_Get_WeeklyReportDetails", con);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeid);
                cmd.Parameters.AddWithValue("@WeekNo", weekno);
                cmd.Parameters.AddWithValue("@YearNo", year);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        report = new WeeklyTaskReport();
                        report.Weekno = sdr["WeekNo"].ToString();
                        report.TeamSize = sdr["TeamSize"].ToString();
                        report.AvailableHours = sdr["AvailableHours"].ToString();
                        report.PlannedHours = sdr["PlannedHours"].ToString();
                        report.PToA = sdr["PtoA"].ToString();
                        report.PlannedTask = sdr["PlannedTasks"].ToString();
                        report.TaskTakenPerPlan = sdr["TasksTakenUpAsPerPlan"].ToString();
                        report.AdherenceToPlan = sdr["AdherenceToPlan"].ToString();
                        report.UToP = sdr["UtoP"].ToString();
                        report.UtilizedHours = sdr["UtilizedHours"].ToString();
                        report.TaskNotPlannedButTakenUp = sdr["TasksNotPlannedButTakenUp"].ToString();
                        //report.TaskStatus = sdr["TaskStatus"].ToString();
                        //report.SkippedTask= sdr["SkippedTasks"].ToString();
                        report.WeekNumberText = "Current Week Number";
                        if (weeklyTaskReports.Count == 0)
                        {
                            report.HeaderVisibility = true;
                            report.WeekNumberText = "Last Week Number";
                            report.SkippedTaskLabel = true;
                            report.SkippedTaskTextBox = false;
                        }
                        report.Planner = employeeid;
                        weeklyTaskReports.Add(report);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("GetWeeklyReport" + e.Message);
            }
            return weeklyTaskReports;
        }
        internal static DataTable GetWeeklyReportq1(string employeeid, string weekno, string year, out DataTable dt1, out DataTable dt2, out DataTable dt3, out DataTable dt4)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            dt1 = new DataTable();
            dt2 = new DataTable();
            dt3 = new DataTable();
            dt4 = new DataTable();
            try
            {
                cmd = new SqlCommand("S_Get_WeeklyReportDetails", con);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeid);
                cmd.Parameters.AddWithValue("@WeekNo", weekno);
                cmd.Parameters.AddWithValue("@YearNo", year);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                dt1.Load(sdr);
                dt2.Load(sdr);
                dt3.Load(sdr);
                dt4.Load(sdr);
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("GetWeeklyReport" + e.Message);
            }
            return dt1;
        }
        internal static int InsertWeeklyTaskReport(WeeklyTaskReport inputtext, string param)
        {
            int Result = 0;
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            List<WeeklyTaskReport> weeklyTaskReports = new List<WeeklyTaskReport>();
            try
            {
                cmd = new SqlCommand("S_Get_WeeklyReportDetails", con);
                cmd.Parameters.AddWithValue("@YearNo", inputtext.Year);
                cmd.Parameters.AddWithValue("@WeekNo", inputtext.Weekno);
                cmd.Parameters.AddWithValue("@EmployeeID", inputtext.EmployeeID);
                cmd.Parameters.AddWithValue("@SkippedTasks", inputtext.SkippedTask);
                cmd.Parameters.AddWithValue("@TaskStatus", inputtext.TaskStatus);
                //cmd.Parameters.AddWithValue("@ProductionSupport", inputtext.ProductionSupport);
                cmd.Parameters.AddWithValue("@Dependencies", inputtext.Dependencies);
                cmd.Parameters.AddWithValue("@MajarTasks", inputtext.MajorTask);
                cmd.Parameters.AddWithValue("@Param", param);
                //cmd.Parameters.AddWithValue("@UpdatedTS", inputtext.UpdatedTask);
                cmd.CommandType = CommandType.StoredProcedure;
                Result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("InsertWeeklyTaskReport" + ex.Message);
            }
            return Result;
        }
        #endregion

    }
}
