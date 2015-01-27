using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDropdownList();
                txtSwipeInTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                txtSwipeOutTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
        }

        private void fillDropdownList()
        {
            fillDesignation();
            fillDescription();
            fillBranch();
            fillDepartment();
            fillLocation();
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

                drpSwipeOutLocation.Items.Clear();
                drpSwipeOutLocation.Items.Add("-select-");
                drpSwipeOutLocation.AppendDataBoundItems = true;
                drpSwipeOutLocation.DataTextField = "LocationName";
                drpSwipeOutLocation.DataValueField = "LocationId";
                drpSwipeOutLocation.DataSource = dt;
                drpSwipeOutLocation.DataBind();

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
                drpBranch.DataValueField = "BranchEntryId";
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
            try
            {
                var conObj = new DBConnection();
                DateTime swipeInTime_Val = conObj.covertDateTime(txtSwipeInTime.Text.Trim());
                string swipeInTime = swipeInTime_Val.ToString();
                DateTime swipeOutTime_Val = conObj.covertDateTime((txtSwipeOutTime.Text.Trim()));
                string swipeOutTime = swipeOutTime_Val.ToString();

                int rs = conObj.insertDetails(txtEmployeeId.Text.Trim(), txtEmployeeName.Text.Trim(),
                    drpGender.SelectedValue, drpDesignation.SelectedValue,
                    drpDescription.SelectedValue, drpBranch.SelectedValue, drpDepartment.SelectedValue,
                    swipeInTime, drpSwipeInLocation.SelectedValue,
                    swipeOutTime, drpSwipeOutLocation.SelectedValue);
                clearFields();
                if (rs > 0)
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Record saved successfully!', 'success', 'top');", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Record already exist', 'error', 'top');", true);

            }
            catch (Exception ex)
            {
                
                
            }
           

        }

        private void clearFields()
        {
            txtEmployeeId.Text = "";
            txtEmployeeName.Text = "";
            drpGender.SelectedValue = "Male";
            drpDesignation.SelectedValue = "-select-";
            drpDescription.SelectedValue = "-select-";
            drpBranch.SelectedValue = "-select-";
            drpSwipeInLocation.SelectedValue = "-select-";
            drpSwipeOutLocation.SelectedValue = "-select-";
        }
    }
}