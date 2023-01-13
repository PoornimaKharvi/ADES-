using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADES_22
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideShowMenus();
        }

        public void HideShowMenus()
        {
            try
            {
                string Dept = (String)Session["Dept"];
                string Role = (String)Session["Role"];

                if((Dept == "Admin") && (Role == "Admin"))
                {
                    hasmastermenu.Visible = true;
                    li_mj.Visible = true;
                    hassubmenu_prj.Visible = true;
                    li_plist.Visible = true;
                    hassubmenu.Visible = true;
                    li_dash.Visible = true;
                    li_backlog_External.Visible = true;
                    li_backlog_Internal.Visible = true;
                }
                else if ((Dept == "Development Team") && (Role == "Team Leader" || Role=="Team Manager"))
                {
                    hasmastermenu.Visible = false;
                    li_mj.Visible = true;
                    hassubmenu_prj.Visible = true;
                    li_plist.Visible = true;
                    hassubmenu.Visible = true;
                    li_dash.Visible = true;
                    li_backlog_External.Visible = true;
                    li_backlog_Internal.Visible = true;
                }
                else if ((Dept == "Development Team") && (Role == "Team Member"))
                {
                    hasmastermenu.Visible = false;
                    li_mj.Visible = true;
                    hassubmenu_prj.Visible = false;
                    li_plist.Visible = false;
                    hassubmenu.Visible = true;
                    li_dash.Visible = true;
                    li_backlog_External.Visible = false;
                    li_backlog_Internal.Visible = true;
                }
                else if ((Dept == "Application Team") && (Role == "Team Leader" || Role=="Team Manager"))
                {
                    hasmastermenu.Visible = false;
                    li_mj.Visible = true;
                    hassubmenu_prj.Visible = true;
                    li_plist.Visible = true;
                    hassubmenu.Visible = true;
                    li_dash.Visible = true;
                    li_backlog_External.Visible = true;
                    li_backlog_Internal.Visible = false;
                }
                else if ((Dept == "Application Team") && (Role == "Team Member"))
                {
                    hasmastermenu.Visible = false;
                    li_mj.Visible = true;
                    hassubmenu_prj.Visible = false;
                    li_plist.Visible = false;
                    hassubmenu.Visible = true;
                    li_dash.Visible = true;
                    li_backlog_External.Visible = false;
                    li_backlog_Internal.Visible = true;
                }
                else if ((Dept == "QA Team") && (Role == "Team Leader" || Role == "Team Manager"))
                {
                    hasmastermenu.Visible = false;
                    li_mj.Visible = true;
                    hassubmenu_prj.Visible = true;
                    li_plist.Visible = true;
                    hassubmenu.Visible = true;
                    li_dash.Visible = true;
                    li_backlog_External.Visible = true;
                    li_backlog_Internal.Visible = true;
                }
                else if ((Dept == "QA Team") && (Role == "Team Member"))
                {
                    hasmastermenu.Visible = false;
                    li_mj.Visible = true;
                    hassubmenu_prj.Visible = false;
                    li_plist.Visible = false;
                    hassubmenu.Visible = true;
                    li_dash.Visible = true;
                    li_backlog_External.Visible = false;
                    li_backlog_Internal.Visible = true;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("HideShowMenus: " + ex.Message);
            }
        }

        protected void link_logout_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["Dept"] = "";
            Session["Role"] = "";
            Session["ProjectName"] = "";
            Session["login"] = "";

            Response.Redirect("~/Login.aspx", false);
        }
    }
}