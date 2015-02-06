using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class EditDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["Id"];

                ViewState["NBADId"] = id;
                var conobj = new DBConnection();
                var allData = new DataTable();
                allData = conobj.GetDetailsWithId(id, "usp_tblAccess_ManualEntrySelect", "@NBADId");
                fillDetails(allData);
            }
            
        }

        private void fillDetails(DataTable allData)
        {
            txtEmployeeId.Text = allData.Rows[0]["EmployeeId"].ToString();
            txtEmployeeName.Text = allData.Rows[0]["EmployeeName"].ToString();
            drpGender.SelectedValue = allData.Rows[0]["Gender"].ToString();

            fillDesignation();
            drpDesignation.SelectedItem.Text = allData.Rows[0]["Designation"].ToString();

            fillDescription();
            drpDescription.SelectedItem.Text = allData.Rows[0]["Description"].ToString();

            fillBranch();
            drpBranch.SelectedItem.Text = allData.Rows[0]["Branch"].ToString();

            fillDepartment();
            drpDepartment.SelectedItem.Text = allData.Rows[0]["Department"].ToString();
            
            fillLocation();
            drpSwipeInLocation.SelectedItem.Text = allData.Rows[0]["Location"].ToString();

            drpReaderType.SelectedValue = allData.Rows[0]["ReaderType"].ToString();
            txtSwipeInTime.Text = allData.Rows[0]["AccessTime"].ToString();
            txtAprovedBy.Text = allData.Rows[0]["ApprovedBy"].ToString();
            txtApprovedDate.Text = allData.Rows[0]["ApprovedDate"].ToString();

        }

        private void fillLocation()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblLocationSelect");
            drpSwipeInLocation.DataSource = null;
            drpSwipeOutLocation.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                drpSwipeInLocation.Items.Clear();
                drpSwipeInLocation.Items.Add("-select-");
                drpSwipeInLocation.AppendDataBoundItems = true;
                drpSwipeInLocation.DataTextField = "LocationName";
                drpSwipeInLocation.DataValueField = "LocationId";
                drpSwipeInLocation.DataSource = dt;
                drpSwipeInLocation.DataBind();
            }
        }

        private void fillDepartment()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblDepartmentSelect");
            drpDepartment.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                drpDepartment.Items.Clear();
                drpDepartment.Items.Add("-select-");
                drpDepartment.AppendDataBoundItems = true;
                drpDepartment.DataTextField = "DepartmentName";
                drpDepartment.DataValueField = "DepartmentId";
                drpDepartment.DataSource = dt;
                drpDepartment.DataBind();

            }
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
                drpBranch.DataTextField = "BranchName";
                drpBranch.DataValueField = "BranchID";
                drpBranch.DataSource = dt;
                drpBranch.DataBind();

            }
        }

        private void fillDescription()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblDescriptionSelect");
            drpDescription.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                drpDescription.Items.Clear();
                drpDescription.Items.Add("-select-");
                drpDescription.AppendDataBoundItems = true;
                drpDescription.DataTextField = "Description";
                drpDescription.DataValueField = "DescriptionId";
                drpDescription.DataSource = dt;
                drpDescription.DataBind();

            }
        }

        private void fillDesignation()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblDesignationSelect");
            drpDesignation.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                drpDesignation.Items.Clear();
                drpDesignation.Items.Add("-select-");
                drpDesignation.AppendDataBoundItems = true;
                drpDesignation.DataTextField = "DesignationName";
                drpDesignation.DataValueField = "DesignationId";
                drpDesignation.DataSource = dt;
                drpDesignation.DataBind();
                drpDepartment.SelectedValue = "-select-";

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var conobj = new DBConnection();
             DateTime swipeInTime_Val = conobj.covertDateTime(txtSwipeInTime.Text.Trim());
                string swipeInTime = swipeInTime_Val.ToString();
             DateTime aprovedtime_Val = conobj.covertDateTime((txtApprovedDate.Text.Trim()));
                string aprovedTime = aprovedtime_Val.ToString();
            conobj.updateDetails(txtEmployeeId.Text.Trim(), txtEmployeeName.Text.Trim(),
                    drpGender.SelectedValue, drpDesignation.SelectedItem.Text,
                    drpDescription.SelectedItem.Text, drpBranch.SelectedValue, drpDepartment.SelectedItem.Text,
                    swipeInTime, drpSwipeInLocation.SelectedItem.Text,drpReaderType.SelectedItem.Text,txtAprovedBy.Text.Trim(),aprovedTime,
                ViewState["NBADId"].ToString());
            conobj.insertLog("Update", "Details", Session["username"].ToString(), System.DateTime.Now);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + "Successfully Updated" + "');</script>", false);

            Server.Transfer("detailsEntry.aspx", true);
        }
    }
}