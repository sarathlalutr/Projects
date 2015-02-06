using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var conobj = new DBConnection();
                fillBranch();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var conobj = new DBConnection();
                int rs = conobj.InsertBranch(txtBranchID.Text.Trim(), txtBranchName.Text.Trim(), txtScheduleInTime.Text.Trim(), txtScheduleOutTime.Text.Trim(), Session["username"].ToString(),System.DateTime.Now);
                conobj.insertLog("Insert", "Branch Entry", Session["username"].ToString(), System.DateTime.Now);
                fillBranch();
                clearfields();
                if (rs > 0)
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Record saved successfully!', 'success', 'top');", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Record already exist', 'error', 'top');", true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        private void fillBranch()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblBranchSelect");
            gridBranch.DataSource = dt;
            gridBranch.DataBind();
        }

        private void clearfields()
        {
            txtBranchID.Text = "";
            txtBranchName.Text = "";
        }
        protected void gridBranch_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gridBranch.PageIndex = e.NewPageIndex;
            if (txtSearchBranch.Text == "")
            {
                fillBranch();
            }
            else
                Search();

        }

        protected void gridBranch_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                //http://www.c-sharpcorner.com/uploadfile/kannagoud/edit-update-delete-record-in-repeater-control/
                var conobj = new DBConnection();
                string res = conobj.deletewithId(e.CommandArgument.ToString(), "usp_tblBranchDelete", "@BranchEntryId");
                if (res == "547")
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Unable to delete..It is already used!!', 'error', 'top');", true);
                conobj.insertLog("Delete", "Branch Entry", Session["username"].ToString(), System.DateTime.Now);
                fillBranch();
            }
        }

        protected void gridBranch_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();

        }

        private void Search()
        {
            var conobj = new DBConnection();

            gridBranch.DataSource = conobj.GetSearchDetails("usp_BranchDetailsSearch", "@SearchTerm ", txtSearchBranch.Text.Trim());
            gridBranch.DataBind();
        }
    }
}