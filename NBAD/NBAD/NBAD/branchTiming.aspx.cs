using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class WebForm17 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDropdownList();
                fillBranchTiming();
            }
        }

        private void fillBranchTiming()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblBranchTimingSelectAll");
            gridBranchTiming.DataSource = dt;
            gridBranchTiming.DataBind();
        }

        private void fillDropdownList()
        {
            fillBranch();
        }

        private void fillBranch()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblBranchSelect");
            drpBranch.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                drpBranch.Items.Clear();
                drpBranch.Items.Add("-select-");
                drpBranch.AppendDataBoundItems = true;
                drpBranch.DataTextField = "BranchID";
                drpBranch.DataValueField = "BranchEntryId";
                drpBranch.DataSource = dt;
                drpBranch.DataBind();

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var conobj = new DBConnection();
                int rs = conobj.InsertBranchTiming(drpBranch.SelectedValue,txtSundayTimeIn.Text.Trim(),txtSundayTimeOut.Text.Trim(),
                    txtMondayTimeIn.Text.Trim(),txtMondayTimeOut.Text.Trim(),txtTuesdayTimeIn.Text.Trim(),txtTuesdayTimeOut.Text.Trim(),
                    txtWednesdayTimeIn.Text.Trim(),txtWednesdayTimeOut.Text.Trim(),txtThursdayTimeIn.Text.Trim(),txtThursdayTimeOut.Text.Trim(),
                    txtFridayTimeIn.Text.Trim(), txtFridayTimeOut.Text.Trim(), txtSaturdayTimeIn.Text.Trim(), txtSaturdayTimeOut.Text.Trim(), Session["username"].ToString(), System.DateTime.Now);
                conobj.insertLog("Insert", "Branch Timing Entry", Session["username"].ToString(), System.DateTime.Now);
                fillBranchTiming();
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

        private void clearfields()
        {
            drpBranch.SelectedValue = "-select-";
            txtSundayTimeIn.Text = "";
            txtSundayTimeOut.Text = "";
            txtMondayTimeIn.Text = "";
            txtMondayTimeOut.Text = "";
            txtTuesdayTimeIn.Text = "";
            txtTuesdayTimeOut.Text = "";
            txtWednesdayTimeIn.Text = "";
            txtWednesdayTimeOut.Text = "";
            txtThursdayTimeIn.Text = "";
            txtThursdayTimeOut.Text = "";
            txtFridayTimeIn.Text = "";
            txtFridayTimeOut.Text = "";
            txtSaturdayTimeIn.Text = "";
            txtSaturdayTimeOut.Text = "";

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();

        }

        private void Search()
        {
            var conobj = new DBConnection();

            gridBranchTiming.DataSource = conobj.GetSearchDetails("usp_BranchTimingDetailsSearch", "@SearchTerm ", txtSearchBranch.Text.Trim());
            gridBranchTiming.DataBind();
        }
        protected void gridBranchTiming_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gridBranchTiming.PageIndex = e.NewPageIndex;
            if (txtSearchBranch.Text == "")
            {
                fillBranch();
            }
            else
                Search();

        }
        protected void gridBranchTiming_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                //http://www.c-sharpcorner.com/uploadfile/kannagoud/edit-update-delete-record-in-repeater-control/
                var conobj = new DBConnection();
                string res = conobj.deletewithId(e.CommandArgument.ToString(), "usp_tblBranchTimingDelete", "@BranchTimeEntryID");
                if (res == "547")
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Unable to delete..It is already used!!', 'error', 'top');", true);
                conobj.insertLog("Delete", "Branch Timing Entry", Session["username"].ToString(), System.DateTime.Now);
                fillBranchTiming();
            }
        }

        protected void gridBranchTiming_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

        }

    }
}