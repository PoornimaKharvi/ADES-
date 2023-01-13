using ADES_22.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADES_22
{
    public partial class ItemMasterDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSearchItemName.Text = "";
                GetItemMaster();
            }
        }
        public void GetItemMaster()
        {
            try
            {
                List<ItemMaster> list = new List<ItemMaster>();
                Session["ItemMaster"] = list = DBAccess.DBAccess.GetItemMaster("ViewItemMaster");
                gvItemMaster.DataSource = list;
                gvItemMaster.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetItemMaster:" + ex.Message);
            }
        }
        protected void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                txtItemNo.Text = "";
                txtItemName.Text = "";
                cbxAccessories.Checked = false;
                txtDescription.Text = "";
                txtPartNo.Text = "";
                txtSupplierName.Text = "";

                txtItemName.Enabled = true;
                hdnAddItemMaster.Value = "New";
                btnSave.Text = "Add";
                ItemMasterTitle.Text = "Add Item Details";

                HelperClass.ClearModal(this);
                HelperClass.OpenModal(this, "AddItemMaster", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnAddDetails_Click" + ex.Message);
            }
        }

        protected void lkbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = (((sender as LinkButton).NamingContainer) as GridViewRow).RowIndex;
                txtItemNo.Text = (gvItemMaster.Rows[rowIndex].FindControl("lblINo") as Label).Text;
                txtItemName.Text = (gvItemMaster.Rows[rowIndex].FindControl("lblIName") as Label).Text;
                txtDescription.Text = (gvItemMaster.Rows[rowIndex].FindControl("lblIDescription") as Label).Text;
                cbxAccessories.Checked = (gvItemMaster.Rows[rowIndex].FindControl("cbAccessories") as CheckBox).Checked;
                txtSupplierName.Text = (gvItemMaster.Rows[rowIndex].FindControl("lblIsupplierName") as Label).Text;
                txtPartNo.Text = (gvItemMaster.Rows[rowIndex].FindControl("lblIPartNo") as Label).Text;

                txtItemName.Enabled = false;
                hdnAddItemMaster.Value = "Edit";
                btnSave.Text = "Update";
                ItemMasterTitle.Text = "Edit Item Details";

                HelperClass.OpenModal(this, "AddItemMaster", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lkbtnEdit_Click:" + ex.Message);
            }
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = (int)ViewState["DeleteRowIndex"];
                ItemMaster ItemMasterEntry = new ItemMaster();
                ItemMasterEntry.ItemNo = (gvItemMaster.Rows[DeleteRowIndex].FindControl("lblINo") as Label).Text;
                ItemMasterEntry.ItemName = (gvItemMaster.Rows[DeleteRowIndex].FindControl("lblIName") as Label).Text;
                ItemMasterEntry.ItemDescription = (gvItemMaster.Rows[DeleteRowIndex].FindControl("lblIDescription") as Label).Text;
                ItemMasterEntry.supplierName = (gvItemMaster.Rows[DeleteRowIndex].FindControl("lblISupplierName") as Label).Text;
                ItemMasterEntry.IsAccessories = (gvItemMaster.Rows[DeleteRowIndex].FindControl("cbAccessories") as CheckBox).Checked;
                ItemMasterEntry.partNo = (gvItemMaster.Rows[DeleteRowIndex].FindControl("lblIPartNo") as Label).Text;

                string success = DBAccess.DBAccess.DeleteItemMaster(ItemMasterEntry, "DeleteItemMaster");
                ViewState["DeleteRowIndex"] = -1;
                if (success == "Deleted")
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Item Details Deleted successfully");
                }
                else
                {
                    HelperClass.OpenModal(this, "myconfirmationmodal", false);
                    HelperClass.OpenErrorModal(this, "error, while deleting records.");
                    return;
                }
                GetItemMaster();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnYes_Click:" + ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ItemMaster IMaster = new ItemMaster();
                IMaster.ItemName = txtItemName.Text;
                IMaster.ItemNo = txtItemNo.Text;
                IMaster.ItemDescription = txtDescription.Text;
                IMaster.IsAccessories = cbxAccessories.Checked;
                IMaster.supplierName = txtSupplierName.Text;
                IMaster.partNo = txtPartNo.Text;

                string success = DBAccess.DBAccess.InsertUpdateItemMaster(IMaster, "SaveItemMaster");
                if (success == "Inserted")
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Item Details successfully Inserted!");

                }
                else if (success == "Updated")
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Item Details Updated Successfully!");
                    //HelperClass.OpenModal(this, "AddItemMaster", false);
                }
                else
                {
                    HelperClass.OpenModal(this, "AddItemMaster", false);
                    HelperClass.OpenErrorModal(this, "Error, while inserting records.");
                    return;
                }
                GetItemMaster();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSave_Click:" + ex.Message);
            }
        }
        protected void gvItemMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ViewState["DeleteRowIndex"] = e.RowIndex;
                HelperClass.OpenModal(this, "myConfirmationModal", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("gvItemMaster_RowDeleting:" + ex.Message);
            }
        }
    }
}