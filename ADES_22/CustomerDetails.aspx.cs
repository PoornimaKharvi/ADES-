using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADES_22.Model;
using ADES_22.DBAccess;
using System.Text;

namespace ADES_22
{
    public partial class CustomerDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((String)Session["login"] != "Yes" || (String)Session["login"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }

            if (!Page.IsPostBack)
            {
                BindRegion();
                BindCList();
                BindFilterRegion();
                BindCustomerInfo();
            }
        }

        public void BindCustomerInfo()
        {
            try
            {
                GridCust.DataSource = DBAccess.DBAccess.GetCustomerInfo();
                GridCust.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindCustomerInfo: " + ex.Message);
            }
        }

        public void BindRegion()
        {
            try
            {
                DdlRegion.DataSource = DBAccess.DBAccess.BindCRegion();
                DdlRegion.DataBind();
                DdlRegion.Items.Insert(0, "Select");
                DdlRegion.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindRegion: " + ex.Message);
            }
        }

        public void BindCList()
        {
            try
            {
                List<string> MList = DBAccess.DBAccess.GetCustomerName("CustomerList");
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < MList.Count; i++)
                    stringBuilder.Append(string.Format("<option value='{0}' />", MList[i]));

                CList.InnerHtml = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindCList: " + ex.Message);
            }
        }

        public void BindFilterRegion()
        {
            try
            {
                FilterDDLRegion.DataSource = DBAccess.DBAccess.BindCRegion(); 
                FilterDDLRegion.DataBind();
                FilterDDLRegion.Items.Insert(0, "Select");
                FilterDDLRegion.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BindFilterRegion: " + ex.Message);
            }
        }

        public void Clear()
        {
            try
            {
                txtCName.Text = String.Empty;
                txtCDescription.Text = String.Empty;
                DdlRegion.SelectedIndex = 0;
                txtCName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Clear: " + ex.Message);
            }
        }

        protected void Btnadd_Click(object sender, EventArgs e)
        {
            hfNeworEdit.Value = "New";
            modaltitle.Text = "Add Customer";

            try
            {
                Clear();

                HelperClass.OpenModal(this, "AddEditModal", true);

                Btnedit.Value = "Add";

                txtCName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Btnadd_Click: " + ex.Message);
            }
        }

        protected void Btnedit_Click(object sender, EventArgs e)
        {
            hfNeworEdit.Value = "Edit";
            modaltitle.Text = "Edit Customer";

            try
            {
                Clear();

                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                hfCID.Value = (GridCust.Rows[rowIndex].FindControl("lblCID") as HiddenField).Value;
                txtCName.Text = (GridCust.Rows[rowIndex].FindControl("lblCName") as Label).Text;
                txtCDescription.Text = (GridCust.Rows[rowIndex].FindControl("lblCDescription") as Label).Text;

                string Region = (GridCust.Rows[rowIndex].FindControl("lblCRegion") as Label).Text;
                if (DdlRegion.Items.FindByValue(Region) != null)
                    DdlRegion.SelectedValue = Region;

                HelperClass.OpenModal(this, "AddEditModal", true);

                Btnedit.Value = "Update";

                txtCName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Btnedit_Click: " + ex.Message);
            }
        }

        protected void Btndlt_Click(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                ViewState["DeleteRowIndex"] = DeleteRowIndex;

                ConfirmText.Text = "Are you sure you want to delete this record?";

                HelperClass.OpenModal(this, "myConfirmationModal", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btndlt_Click: " + ex.Message);
            }
        }

        protected void DeleteConfirmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = (int)ViewState["DeleteRowIndex"];

                Customer customerDetails = new Customer();
                customerDetails.ID = Convert.ToInt16((GridCust.Rows[DeleteRowIndex].FindControl("lblCID") as HiddenField).Value);

                DBAccess.DBAccess.DeleteCustomer(customerDetails);

                ViewState["DeleteRowIndex"] = -1;

                HelperClass.ClearModal(this);
                HelperClass.OpenSuccessToaster(this, "Project details deleted successfully!");

                BindCustomerInfo();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes_ServerClick: " + ex.Message);
            }
        }

        protected void Btnedit_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Customer customerDetails = new Customer();
                customerDetails.CName = txtCName.Text;
                customerDetails.CDescription = txtCDescription.Text;
                customerDetails.CRegion = DdlRegion.SelectedItem.Text;

                if (hfNeworEdit.Value == "New")
                {
                    string success = DBAccess.DBAccess.SaveCustomerDetails(customerDetails);
                    BindCustomerInfo();
                    BtnFilterSearch_Click(null, null);

                    if (success == "Inserted")
                        HelperClass.OpenSuccessToaster(this, "Customer details saved successfully!");
                    else
                        HelperClass.OpenSuccessToaster(this, "Customer details updated successfully!");

                    HelperClass.ClearModal(this);
                }
                else
                {
                    customerDetails.ID = Convert.ToInt16(hfCID.Value);
                    DBAccess.DBAccess.UpdateCustomerDetails(customerDetails);
                    BindCustomerInfo();
                    BtnFilterSearch_Click(null, null);

                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Customer details updated successfully!");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Btnedit_ServerClick: " + ex.Message);
            }
        }

        protected void BtnFilterSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Customer cd = new Customer();
                cd.CName = FilterCName.Text;
                cd.CRegion = FilterDDLRegion.SelectedItem.Text;
                
                int cnt = 0;

                if (FilterCName.Text != String.Empty && FilterDDLRegion.SelectedIndex != 0) //Both has data
                    cnt = 1;
                else if (FilterCName.Text != String.Empty) //Only CName has data
                    cnt = 2;
                else if (FilterDDLRegion.SelectedItem.Text!="Select") //Only Region has data
                    cnt = 3;
                else //Both are empty
                    cnt = 4;

                GridCust.DataSource = DBAccess.DBAccess.FilterCustomerInfo(cd,cnt);
                GridCust.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BtnFilterSearch_Click: " + ex.Message);
            }
        }
    }
}