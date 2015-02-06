using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class EditBranchTiming : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["Id"];
                ViewState["BranchTimeEntryID"] = id;
                var conobj = new DBConnection();
                var allData = new DataTable();
                allData = conobj.GetDetailsWithId(id, "usp_tblBranchTimingSelect", "@BranchTimeEntryID");
                fillDetails(allData);
            }
        }

        private void fillDetails(DataTable allData)
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
            drpBranch.SelectedValue = allData.Rows[0]["BranchEntryId"].ToString();
            txtSundayTimeIn.Text = allData.Rows[0]["SundayIn"].ToString();
            txtSundayTimeOut.Text = allData.Rows[0]["SundayOut"].ToString();
            txtMondayTimeIn.Text = allData.Rows[0]["MondayIn"].ToString();
            txtMondayTimeOut.Text = allData.Rows[0]["MondayOut"].ToString();
            txtTuesdayTimeIn.Text = allData.Rows[0]["TuesdayIn"].ToString();
            txtTuesdayTimeOut.Text = allData.Rows[0]["TuesdayOut"].ToString();
            txtWednesdayTimeIn.Text = allData.Rows[0]["WednesdayIn"].ToString();
            txtWednesdayTimeOut.Text = allData.Rows[0]["WednesdayOut"].ToString();
            txtThursdayTimeIn.Text = allData.Rows[0]["ThursdayIn"].ToString();
            txtThursdayTimeOut.Text = allData.Rows[0]["ThursdayOut"].ToString();
            txtFridayTimeIn.Text = allData.Rows[0]["FridayIn"].ToString();
            txtFridayTimeOut.Text = allData.Rows[0]["FridayIn"].ToString();
            txtSaturdayTimeIn.Text = allData.Rows[0]["SaturdayIn"].ToString();
            txtSaturdayTimeOut.Text = allData.Rows[0]["SaturdayOut"].ToString();


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var conobj = new DBConnection();
            //conobj.updateBranch(txtBranchID.Text.Trim(), txtBranchName.Text.Trim(),
            //    ViewState["BranchEntryId"].ToString());
            conobj.updateBranchTiming(drpBranch.SelectedValue,txtSundayTimeIn.Text.Trim(),txtSundayTimeOut.Text.Trim(),
                    txtMondayTimeIn.Text.Trim(),txtMondayTimeOut.Text.Trim(),txtTuesdayTimeIn.Text.Trim(),txtTuesdayTimeOut.Text.Trim(),
                    txtWednesdayTimeIn.Text.Trim(),txtWednesdayTimeOut.Text.Trim(),txtThursdayTimeIn.Text.Trim(),txtThursdayTimeOut.Text.Trim(),
                    txtFridayTimeIn.Text.Trim(),txtFridayTimeOut.Text.Trim(),txtSaturdayTimeIn.Text.Trim(),txtSaturdayTimeOut.Text.Trim()
                ,ViewState["BranchTimeEntryID"].ToString());
            conobj.insertLog("Update", "Branch Timing", Session["username"].ToString(), System.DateTime.Now);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + "Successfully Updated" + "');</script>", false);

            Server.Transfer("branchTiming.aspx", true);
        }
    }
}