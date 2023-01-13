using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using ADES_22.DBAccess;
using ADES_22.Model;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Microsoft.AspNet.Web.Optimization.WebForms;


namespace ADES_22
{
    public partial class Tasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindProjectId();
                SelectedTask();
            }             
        }                      

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                BindDDLProjectID();
                txtaddrows.Text = "5";
                if(ddlviewprojectid.SelectedValue!=null)
                {
                    ddlProjectID.SelectedValue = ddlviewprojectid.SelectedValue;
                }
                string param = "Oldrow";
                BindGridview(param);                
                HelperClass.OpenModal(this, "newtask", true);                
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("btnAdd_Click:" + ex.Message);
            }
        }                
        protected void txtweekview_TextChanged(object sender,EventArgs e)
        {
            try
            {
                Taskdetails list = new Taskdetails();                
                list.Param = "View_MainTaskDetails";
                List<Taskdetails> listTask = new List<Taskdetails>();
                Session["Task"] = listTask = DBAccess.DBAccess.getTaskEntry(list);
                GVView.Visible = true;
                GVView.DataSource = listTask;
                GVView.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SelectedTask:" + ex.Message);
            }
        }
        protected void BindGridview(string param)
        {            
            int rows = Convert.ToInt32(txtaddrows.Text);
            List<Taskdetails> BeforeAddRowClickdList = new List<Taskdetails>();            
            if (param == "NewRow")
            {
               if (Session["ExistingGridvalues"] != null)
                {
                    BeforeAddRowClickdList = (List<Taskdetails>)Session["ExistingGridvalues"];
                }
                rows = BeforeAddRowClickdList.Count + Convert.ToInt32(txtaddrows.Text);
            }
            List<Taskdetails> list1 = new List<Taskdetails>();
            for (int i = 0; i < rows; i++)
            {
                list1.Add(new Taskdetails());
            }
            Session["currentdata"] = list1;
            GVAddtask.DataSource = list1;
            GVAddtask.DataBind();
            List<String> TaskStaus = DBAccess.DBAccess.GetMainTaskStatus();
            for (int i = 0; i < GVAddtask.Rows.Count; i++)
            {
                List<string> result = new List<string>();
                if (param == "NewRow")
                {
                    if (i < BeforeAddRowClickdList.Count)
                    {
                        TextBox txt1 = (GVAddtask.Rows[i]).FindControl("txtaddMaintask") as TextBox;
                        txt1.Text = BeforeAddRowClickdList[i].Maintask;
                        TextBox txt2 = (GVAddtask.Rows[i]).FindControl("txtAddEstimatedEffort") as TextBox;
                        txt2.Text = BeforeAddRowClickdList[i].Estimatedeffort;
                        TextBox txt3= (GVAddtask.Rows[i]).FindControl("txtDeliverydate") as TextBox;
                        txt3.Text = BeforeAddRowClickdList[i].DeliveryDate;
                    }
                }                
            }
        }
        public void BindProjectId()
        {
            try
            {
                List<string> list= DBAccess.DBAccess.GetProjectIDList();
                ddlviewprojectid.DataSource = list;
                ddlviewprojectid.DataBind();
                ddlviewprojectid.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("BindProjectId:" + e.Message);
            }
        }
        public void BindDDLProjectID()
        {
            ddlProjectID.DataSource= DBAccess.DBAccess.GetProjectIDList();
            ddlProjectID.DataBind();
            ddlProjectID.Items.Insert(0, new ListItem("Select", ""));
        }
        public void BindEditMaintaskStatus()
        {
            ddleditstatus.DataSource = DBAccess.DBAccess.GetMainTaskStatus();
            ddleditstatus.DataBind();
            ddleditstatus.Items.Insert(0, new ListItem("Select", ""));
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                SelectedTask();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnView_Click:" + ex.Message);
            }
        }              
        protected void btnSave_Click(object sender,EventArgs e)
        {
            try
            {
                int Result = 0;
                for (int i = 0; i < GVAddtask.Rows.Count; i++)
                {
                    Taskdetails taskdts = new Taskdetails();
                    taskdts.Projectid = ddlProjectID.SelectedValue;
                    taskdts.Maintask = ((GVAddtask.Rows[i]).FindControl("txtaddMaintask") as TextBox).Text;
                    if (taskdts.Projectid == "" || taskdts.Maintask == "")
                    {
                        continue;
                    }
                    taskdts.Estimatedeffort = ((GVAddtask.Rows[i]).FindControl("txtAddEstimatedEffort") as TextBox).Text;
                    taskdts.DeliveryDate= ((GVAddtask.Rows[i]).FindControl("txtDeliverydate") as TextBox).Text;
                    taskdts.MaintaskStatus = "Open";
                    Result = DBAccess.DBAccess.InsertTaskDetailsGridview(taskdts, "Save_MainTaskDetails");    
                    if(Result==0)
                    {
                        break;
                    }
                }
                if (Result > 0)
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Task Details Saved successfully!");
                    SelectedTask();
                }
                else
                {
                    HelperClass.OpenModal(this, "newtask", false);
                    HelperClass.OpenErrorModal(this, "Error, While Saving Records.");
                }                               
            }                            
            catch(Exception ex)
            {
                Logger.WriteErrorLog("btnsave_click:" + ex.Message);
            }
        }
        protected  void btnhdSave_Click(object sender,EventArgs e)
        {
            try
            {
                Taskdetails task = new Taskdetails();
                int rowIndex = Convert.ToInt32(hdnrowindex.Value);
                task.Id = ((GVView.Rows[rowIndex]).FindControl("hdnid") as  HiddenField).Value;
                task.Projectid = ((GVView.Rows[rowIndex]).FindControl("hdnprojectid") as HiddenField).Value;
                task.Maintask = ((GVView.Rows[rowIndex]).FindControl("lbMainTask") as  Label).Text;
                task.Estimatedeffort = ((GVView.Rows[rowIndex]).FindControl("txtEstimatedEffort") as TextBox).Text;
                task.MaintaskStatus = ((GVView.Rows[rowIndex]).FindControl("lbStatus") as Label).Text;
                task.DeliveryDate= ((GVView.Rows[rowIndex]).FindControl("lbDeliverydate") as Label).Text;              
                
                int Result=DBAccess.DBAccess.InsertTaskDetailsGridview(task,"Update_MainTaskDetails");
                if (Result > 0)
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Estimated Effort Updated Successfully!");
                    SelectedTask();
                }
                else
                {                    
                    HelperClass.OpenErrorModal(this, "Error, While Updating Records.");
                }                               
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
        }
        protected void lbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = (((sender as LinkButton).NamingContainer) as GridViewRow).RowIndex;
                hdneditid.Value = (GVView.Rows[rowIndex].FindControl("hdnid") as HiddenField).Value;
                lbeditprojectid.Text = (GVView.Rows[rowIndex].FindControl("hdnprojectid") as HiddenField).Value;
                txteditmaintask.Text = (GVView.Rows[rowIndex].FindControl("lbMainTask") as Label).Text;
                txteditestimatedeffort.Text = (GVView.Rows[rowIndex].FindControl("txtEstimatedEffort") as TextBox).Text;
                BindEditMaintaskStatus();
                string value = (GVView.Rows[rowIndex].FindControl("lbStatus") as  Label).Text;
                if(ddleditstatus.Items.FindByValue(value)!=null)
                {
                    ddleditstatus.SelectedValue = value;
                }
                txteditdeliverydate.Text= (GVView.Rows[rowIndex].FindControl("lbDeliverydate") as Label).Text;
                HelperClass.OpenModal(this, "Edittask", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbEdit_Click:" + ex.Message);
            }
        }
        public void SelectedTask()
          {
            try
            {
                    Taskdetails list = new Taskdetails();
                    list.Projectid = ddlviewprojectid.SelectedValue;
                    list.Param = "View_MainTaskDetails";                    
                    List<Taskdetails> listTask = new List<Taskdetails>();
                    Session["Task"] = listTask = DBAccess.DBAccess.getTaskEntry(list);
                    GVView.Visible = true;
                    GVView.DataSource = listTask;
                    GVView.DataBind();                
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SelectedTask:" + ex.Message);
            }
        }
        protected void btnUpdate_Click(object sender,EventArgs e)
        {
            try
            {
                Taskdetails task = new Taskdetails();
                //task.Projectid = lbeditprojectid.Text;
                task.Id = hdneditid.Value;
                task.Maintask = txteditmaintask.Text;
                task.Estimatedeffort = txteditestimatedeffort.Text;
                task.DeliveryDate = txteditdeliverydate.Text;
                task.MaintaskStatus = ddleditstatus.SelectedValue;
                int Result=DBAccess.DBAccess.InsertTaskDetailsGridview(task, "Update_MainTaskDetails");
                if(Result>0)
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Task Details updated successfully!");
                    SelectedTask();
                }
                else
                {
                    HelperClass.OpenModal(this, "Edittask", false);
                    HelperClass.OpenErrorModal(this, "Error, While Updating Records.");
                }                
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("Update_click:"  +ex.Message);
            }           
        }        
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Session["DeleteRowIndex"] = e.RowIndex;
                HelperClass.openConfirmModal(this, "Are you sure, you want to delete this record?");
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("GridView1_RowDeleting" + ex.Message);
            }
        }
        protected void saveConfirmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = (int)Session["DeleteRowIndex"];
                Taskdetails   task = new  Taskdetails();
                task.Id= (GVView.Rows[DeleteRowIndex].FindControl("hdnid") as  HiddenField).Value;                           
                string success=DBAccess.DBAccess.InsertTaskDetailsGridview1(task,"Delete_MainTaskDetails");
                GVView.EditIndex = -1;
                if (success == "Deleted")
                {                    
                    HelperClass.OpenSuccessToaster(this, "Task Details Deleted successfully");
                    SelectedTask();
                }
                else
                {
                    HelperClass.OpenErrorModal(this, "error, while deleting records.");
                    return;
                }                
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes_ServerClick" + ex.Message);
            }
        }
        protected void lbAddnewrow_Click(object sender, EventArgs e)
        {
            try
            {
                string param = "NewRow";
                List<Taskdetails> BeforeAddRowClickdList = new List<Taskdetails>();
                for (int i = 0; i < GVAddtask.Rows.Count; i++)
                {
                    Taskdetails task = new Taskdetails();             
                    task.Maintask = (GVAddtask.Rows[i].FindControl("txtaddMaintask") as TextBox).Text;
                    task.Estimatedeffort = (GVAddtask.Rows[i].FindControl("txtAddEstimatedEffort") as TextBox).Text;
                    task.DeliveryDate= (GVAddtask.Rows[i].FindControl("txtDeliverydate") as TextBox).Text;
                    BeforeAddRowClickdList.Add(task);
                }
                Session["ExistingGridvalues"] = BeforeAddRowClickdList;
                BindGridview(param);    
                HelperClass.OpenModal(this, "newtask", false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BtnNewRowAdd_Click:" + ex.Message);
            }
        }
    }
}

