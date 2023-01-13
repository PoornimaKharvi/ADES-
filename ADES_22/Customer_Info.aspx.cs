using ADES_22.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADES_22
{
    public partial class Customer_Info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRegion();
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
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BindCustomerInfo: " + ex.Message);
            }
        }

        public void BindRegion()
        {
            try
            {
                DdlRegion.DataSource = HelperClass.BindCRegion();
                DdlRegion.DataBind();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BindRegion: " + ex.Message);
            }
        }

        public void Clear()
        {
            try
            {
                //txtCID.Text = String.Empty;
                txtCName.Text = String.Empty;
                txtCDescription.Text=String.Empty;
                DdlRegion.SelectedIndex = 0;
                txtCName.Focus();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("Clear: " + ex.Message);
            }
        }

        protected void Btnadd_Click(object sender, EventArgs e) 
        {
            hfNeworEdit.Value = "New";
            try
            {
                Clear();

                HelperClass.OpenModal(this, "AddEditModal", true);

                Btnedit.Value = "Add";

                txtCName.Focus();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("Btnadd_Click: " + ex.Message);
            }
        }

        protected void Btnedit_ServerClick(object sender, EventArgs e) 
        {
            

            try
            {
                CustomerDetails customerDetails = new CustomerDetails();
                customerDetails.CName = txtCName.Text;
                customerDetails.CDescription = txtCDescription.Text;
                customerDetails.CRegion = DdlRegion.SelectedItem.Text;

                if (hfNeworEdit.Value == "New")
                {
                    string success=DBAccess.DBAccess.SaveCustomerDetails(customerDetails);
                    BindCustomerInfo();

                    HelperClass.ClearModal(this);

                    if (success == "Inserted")
                        HelperClass.OpenSuccessToaster(this, "Customer details saved successfully!");
                    else
                        HelperClass.OpenSuccessToaster(this, "Customer details updated successfully!");
                }
                else
                {
                    customerDetails.ID = Convert.ToInt16(hfCID.Value);
                    DBAccess.DBAccess.UpdateCustomerDetails(customerDetails);
                    BindCustomerInfo();

                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Customer details updated successfully!");
                }
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("Btnedit_ServerClick: " + ex.Message);
            }
        }

        protected void Btnedit_Click(object sender, EventArgs e)
        {
            hfNeworEdit.Value = "Edit";
            try
            {
                Clear();

                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                hfCID.Value= (GridCust.Rows[rowIndex].FindControl("lblCID") as Label).Text;
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

                CustomerDetails customerDetails = new CustomerDetails();
                customerDetails.ID = Convert.ToInt16((GridCust.Rows[DeleteRowIndex].FindControl("lblCID") as Label).Text);

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
    }
}