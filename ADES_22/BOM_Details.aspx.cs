using ADES_22.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADES_22
{
    public partial class BOM_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindKitDetails();
                lkbtnSelect_Click(null, null);
            }
        }
        public void BindKitDetails()
        {
            try
            {
                List<KitMaster1> list = new List<KitMaster1>();
                Session["KitMaster"] = list = DBAccess.DBAccess.GetKitList("KitList");
                
                gvKitDetails.DataSource = list;
                gvKitDetails.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindKitDetails:" + ex.Message);
            }
        }
        public void BindItemDetails()
        {
            try
            {
                List<BOMDetails> list = new List<BOMDetails>();
                Session["BOMDetails"] = list = DBAccess.DBAccess.GetItemList(ViewState["SelectKitName"].ToString(), "View");
                lvItemDetails.DataSource = list;
                lvItemDetails.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindItemDetails:" + ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lvItemDetails.Items.Count; i++)
                {
                    BOMDetails BOMListEntry = new BOMDetails();

                    if ((lvItemDetails.Items[i].FindControl("ckbSelect") as CheckBox).Checked)
                    {
                        BOMListEntry.param = "Save";
                    }
                    else
                    {
                        BOMListEntry.param = "Delete";
                    }
                    BOMListEntry.kitname = ViewState["SelectKitName"] as string;
                    BOMListEntry.kitno = ViewState["SelectKitNo"] as string;
                    BOMListEntry.itemNo = (lvItemDetails.Items[i].FindControl("hdnItemNo") as HiddenField).Value;
                    BOMListEntry.itemName = (lvItemDetails.Items[i].FindControl("lblItemName") as Label).Text;
                    BOMListEntry.ItemDescription = (lvItemDetails.Items[i].FindControl("lblDescription") as Label).Text;
                    BOMListEntry.supplierName = (lvItemDetails.Items[i].FindControl("lblSupplierName") as Label).Text;
                    BOMListEntry.partNo = (lvItemDetails.Items[i].FindControl("lblPartNo") as Label).Text;
                    BOMListEntry.quantity = (lvItemDetails.Items[i].FindControl("txtQty") as TextBox).Text;
                    BOMListEntry.isAccessories = (bool)(lvItemDetails.Items[i].FindControl("cbAccessories") as CheckBox).Checked;
                   
                    string success = DBAccess.DBAccess.AddBOMdetails(BOMListEntry);
                }
                HelperClass.OpenSuccessToaster(this, "BOM Details successfully Inserted!");
                BindItemDetails();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSave_Click:" + ex.Message);
            }
        }
        protected void lkbtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex;
                if (sender == null)
                {
                    if (gvKitDetails.Rows.Count > 0)
                    {
                        rowIndex = 0;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    rowIndex=(((sender as LinkButton).NamingContainer) as GridViewRow).RowIndex;
                }
                foreach (GridViewRow row in gvKitDetails.Rows)
                {
                    if (row.RowIndex == rowIndex)
                    {
                        row.BackColor = System.Drawing.Color.Orange;
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    }
                }
                ViewState["SelectKitName"] = (gvKitDetails.Rows[rowIndex].FindControl("lblKName") as Label).Text;
                ViewState["SelectKitNo"] = (gvKitDetails.Rows[rowIndex].FindControl("hdnkitNo") as HiddenField).Value;
                BindItemDetails();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lkbtnSelect_Click:" + ex.Message);
            }
        }
    }
}