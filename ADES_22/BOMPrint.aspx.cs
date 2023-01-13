using ADES_22.Model;
using iTextSharp.text;
using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADES_22
{
    public partial class BOMPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindBOMDetails();
        }
        public void BindBOMDetails()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CUSTOMER"].ToString()))
                {
                    List<POItemAccess> List = new List<POItemAccess>();
                    ViewState["customerName"]= Request.QueryString["CUSTOMER"].ToString();
                    ViewState["Pono"]= Request.QueryString["PONO"].ToString();
                    string customer = Request.QueryString["CUSTOMER"].ToString();
                    string PONO = Request.QueryString["PONO"].ToString();
                    DataTable dt = DBAccess.DBAccess.GetPOKitDetails("View", ViewState["customerName"] as string , ViewState["Pono"] as string);
                    if (dt.AsEnumerable().Where(x => x.Field<bool>("IsAccessories") == false).Count() > 0)
                    {
                        DataTable NotAccessories = dt.AsEnumerable().Where(x => x.Field<bool>("IsAccessories") == false).CopyToDataTable();
                        int original = 1;
                        int result;
                        for (int i = 0; i < NotAccessories.Rows.Count; i++)
                        {
                            POItemAccess exp = new POItemAccess();

                            exp.Kitname = NotAccessories.Rows[i]["KitName"].ToString();
                            int countKitName = NotAccessories.AsEnumerable().Where(x => x.Field<string>("KitName") == exp.Kitname).Select(x => x.Field<string>("KitName")).Count();
                            if (i > 0)
                            {
                                exp.PresentRowSpan = countKitName;
                                exp.prevKitName = NotAccessories.Rows[i - 1]["KitName"].ToString();
                                if (exp.prevKitName == exp.Kitname)
                                {
                                    exp.KitVisiblility = false;
                                }
                                else
                                {
                                    result = ++original;
                                    exp.id = result;
                                    exp.KitVisiblility = true;
                                }
                            }
                            else
                            {
                                exp.id = i + 1;
                                exp.PresentRowSpan = countKitName;
                            }
                            exp.Itemname = NotAccessories.Rows[i]["ItemName"].ToString();
                            exp.PartNo = NotAccessories.Rows[i]["PartNo"].ToString();
                            exp.ItemDesc = NotAccessories.Rows[i]["ItemDescription"].ToString();
                            exp.Qty = NotAccessories.Rows[i]["Qty"].ToString();
                            exp.shortage = NotAccessories.Rows[i]["Shortage"].ToString();
                            if (i == 0)
                            {
                                exp.Cust = NotAccessories.Rows[i]["Customer"].ToString();
                                exp.Pono = NotAccessories.Rows[i]["PoNumber"].ToString();
                                exp.HeaderVisibility1 = true;
                                exp.HeaderVisibility2 = true;
                            }
                            List.Add(exp);
                        }
                    }
                    if (dt.AsEnumerable().Where(x => x.Field<bool>("IsAccessories") == true).Count() > 0)
                    {
                        DataTable IsAccessories = dt.AsEnumerable().Where(x => x.Field<bool>("IsAccessories") == true).CopyToDataTable();
                        int original1 = 1;
                        int result1;
                        for (int i = 0; i < IsAccessories.Rows.Count; i++)
                        {
                            POItemAccess exp1 = new POItemAccess();
                            exp1.Kitname = IsAccessories.Rows[i]["KitName"].ToString();
                            int countKitName = IsAccessories.AsEnumerable().Where(x => x.Field<string>("KitName") == exp1.Kitname).Select(x => x.Field<string>("KitName")).Count();

                            if (i > 0)
                            {
                                exp1.PresentRowSpan = countKitName;
                                exp1.prevKitName = IsAccessories.Rows[i - 1]["KitName"].ToString();
                                if (exp1.prevKitName == exp1.Kitname)
                                {
                                    exp1.KitVisiblility = false;
                                }
                                else
                                {
                                    result1 = ++original1;
                                    exp1.id = result1;
                                    exp1.KitVisiblility = true;
                                }
                            }
                            else
                            {
                                exp1.id = i + 1;
                                exp1.PresentRowSpan = countKitName;
                                exp1.AccessoriesHeader = true;
                            }
                            exp1.Itemname = IsAccessories.Rows[i]["ItemName"].ToString();
                            exp1.PartNo = IsAccessories.Rows[i]["PartNo"].ToString();
                            exp1.ItemDesc = IsAccessories.Rows[i]["ItemDescription"].ToString();
                            exp1.Qty = IsAccessories.Rows[i]["Qty"].ToString();
                            exp1.shortage = IsAccessories.Rows[i]["Shortage"].ToString();
                            List.Add(exp1);
                        }
                    }
                    lvBOMPrint.DataSource = List;
                    lvBOMPrint.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindBOMDetails: " + ex.Message);
            }
        }

       
        protected void btnback_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string Customer = ViewState["customerName"] as string;
                string PoNO = ViewState["Pono"] as string;
                var url = String.Format("POAssociate.aspx?CUSTOMER={0}&PONO={1}", Customer, PoNO);
                Response.Redirect(url, false);

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnback_ServerClick: " + ex.Message);
            }
        }
    }
}