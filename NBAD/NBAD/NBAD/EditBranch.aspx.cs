using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class EditBranch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["Id"];

                ViewState["BranchEntryId"] = id;
                var conobj = new DBConnection();
                var allData = new DataTable();
                allData = conobj.GetDetailsWithId(id, "usp_tblBranchSelect", "@BranchEntryId");
                fillDetails(allData);
            }
        }

        private void fillDetails(DataTable allData)
        {
            txtBranchID.Text = allData.Rows[0]["BranchID"].ToString();
            txtBranchName.Text = allData.Rows[0]["BranchName"].ToString();
            txtScheduleInTime.Text = allData.Rows[0]["ScheduleInTime"].ToString();
            txtScheduleOutTime.Text = allData.Rows[0]["ScheduleOutTime"].ToString();
           
        }

        protected void btnEditSubmit_Click(object sender, EventArgs e)
        {
            var conobj = new DBConnection();
            //conobj.updateBranch(txtBranchID.Text.Trim(), txtBranchName.Text.Trim(),
            //    ViewState["BranchEntryId"].ToString());
            conobj.updateBranch(txtBranchID.Text.Trim(), txtBranchName.Text.Trim(),txtScheduleInTime.Text.Trim(),txtScheduleOutTime.Text.Trim(),
                ViewState["BranchEntryId"].ToString());
            conobj.insertLog("Update", "Branch", Session["username"].ToString(), System.DateTime.Now);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + "Successfully Updated" + "');</script>", false);

            Server.Transfer("branchEntry.aspx", true);
        }
    }
}