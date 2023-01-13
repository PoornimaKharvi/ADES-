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
    public partial class Kit_Master_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtKitName.Text = "";
                BindKitDetails();
            }
        }
        public void BindKitDetails()
        {
            try
            {
                List<KitMaster1> list = new List<KitMaster1>();
                Session["KitMaster"] = list = DBAccess.DBAccess.GetKitDetails("ViewKitMaster");
                gvKitMaster.DataSource = list;
                gvKitMaster.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getKitMaster:" + ex.Message);
            }
        }
        protected void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                txtKitNo.Text = "";
                txtKitName.Text = "";

                txtKitName.Enabled = true;

                hdnModalKitMaster.Value = "New";
                btnSave.Text = "Add";
                KitMasterTitle.Text = "Add Kit Master";

                HelperClass.OpenModal(this, "AddKitMaster", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnAddDetails_Click:" + ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string success = DBAccess.DBAccess.InsertUpdateKitMaster(txtKitNo.Text, txtKitName.Text, "SaveKitMaster");
                if (success == "Inserted")
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Kit Details successfully Inserted!");

                }
                else if (success == "Updated")
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Kit Details Updated Succesfully!");
                    //HelperClass.OpenModal(this, "AddKitMaster", false);

                }
                else
                {
                    HelperClass.OpenModal(this, "AddKitMaster", false);
                    HelperClass.OpenErrorModal(this, "Error, while inserting records.");
                    return;
                }
                BindKitDetails();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSave_Click:" + ex.Message);
            }
        }
        protected void lkbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = (((sender as LinkButton).NamingContainer) as GridViewRow).RowIndex;
                txtKitNo.Text = (gvKitMaster.Rows[rowIndex].FindControl("lblKNo") as Label).Text;
                txtKitName.Text = (gvKitMaster.Rows[rowIndex].FindControl("lblKName") as Label).Text;

                txtKitName.Enabled = false;

                hdnModalKitMaster.Value = "Edit";
                btnSave.Text = "Update";
                KitMasterTitle.Text = "Edit Kit Master";

                HelperClass.ClearModal(this);
                HelperClass.OpenModal(this, "AddKitMaster", true);

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
                KitMaster1 kitmasterEntry = new KitMaster1();
                kitmasterEntry.KitNo = (gvKitMaster.Rows[DeleteRowIndex].FindControl("lblKNo") as Label).Text;
                kitmasterEntry.KitName = (gvKitMaster.Rows[DeleteRowIndex].FindControl("lblKName") as Label).Text;
                string success=DBAccess.DBAccess.DeleteKitMaster(kitmasterEntry, "DeleteKitMaster");
                ViewState["DeleteRowIndex"] = -1;
                if (success == "Deleted")
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Kit Details Deleted successfully");
                }
                else
                {
                    HelperClass.OpenModal(this, "myconfirmationmodal", false);
                    HelperClass.OpenErrorModal(this, "error, while deleting records.");
                    return;
                }
                BindKitDetails();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnYes_Click:" + ex.Message);
            }
        }

        protected void gvKitMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ViewState["DeleteRowIndex"] = e.RowIndex;
              
                HelperClass.OpenModal(this, "myConfirmationModal", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("gvKitMaster_RowDeleting:" + ex.Message);
            }

        }


    }
}