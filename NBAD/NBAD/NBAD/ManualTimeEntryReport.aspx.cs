using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libraries;
using PdfExport;

namespace NBAD
{
    public partial class WebForm14 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillBranch();
                fillEmployee();
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                drpBranch.Items.Add("All");
                drpBranch.AppendDataBoundItems = true;
                drpBranch.DataTextField = "BranchName";
                drpBranch.DataValueField = "BranchID";
                drpBranch.DataSource = dt;
                drpBranch.DataBind();

            }
        }


        private void fillEmployee()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("EmployeeIdSelectManualEntry");
            drpEmployeeId.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                drpEmployeeId.Items.Clear();
                drpEmployeeId.Items.Add("All");
                drpEmployeeId.AppendDataBoundItems = true;
                drpEmployeeId.DataTextField = "EmployeeId";
                //drpEmployeeId.DataValueField = "DepartmentId";
                drpEmployeeId.DataSource = dt;
                drpEmployeeId.DataBind();

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var conObj = new DBConnection();
                var repObj = new ReportClass();
                DateTime fromdate = conObj.ConvertDate(txtFromDate.Text);
                DateTime todate = conObj.ConvertDate(txtToDate.Text);
                //DataTable dt = repObj.manualEntryReport(fromdate, todate,drpEmployeeId.SelectedItem.Text);
                DataTable dt = repObj.manualEntryReport(fromdate, todate,drpBranch.SelectedValue, drpEmployeeId.SelectedItem.Text);
                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('No Records to export', 'error', 'top');", true);
                }
                else
                {
                    string subHeading = "(" + txtFromDate.Text + " - " + txtToDate.Text + ")";
                    PdfCreater.ExportToPdf(dt, "Manual Entry Report", subHeading);
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            try
            {
                var conObj = new DBConnection();
                var repObj = new ReportClass();
                DateTime fromdate = conObj.ConvertDate(txtFromDate.Text);
                DateTime todate = conObj.ConvertDate(txtToDate.Text);
                //DataTable dt = repObj.manualEntryReport(fromdate, todate,drpEmployeeId.SelectedItem.Text);
                DataTable dt = repObj.manualEntryReport(fromdate, todate, drpBranch.SelectedValue, drpEmployeeId.SelectedItem.Text);

                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('No Records to export', 'error', 'top');", true);
                }
                else
                {
                    //PdfCreater.ExportToPdf(dt, "Staff Check-In Report", Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy"));
                    string subHeading = "(" + txtFromDate.Text + " - " + txtToDate.Text + ")";
                    ExcelCreater.CreateSheet("All Swipe Report", dt, subHeading);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                    "showAlert('Cannot process the report. Please try again', 'error', 'top');", true);
            }
        }

        protected void ExportToWord(object sender, EventArgs e)
        {
            try
            {
                var conObj = new DBConnection();
                var repObj = new ReportClass();
                DateTime fromdate = conObj.ConvertDate(txtFromDate.Text);
                DateTime todate = conObj.ConvertDate(txtToDate.Text);
                //DataTable dt = repObj.manualEntryReport(fromdate, todate, drpEmployeeId.SelectedItem.Text);
                DataTable dt = repObj.manualEntryReport(fromdate, todate, drpBranch.SelectedValue, drpEmployeeId.SelectedItem.Text);

                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('No Records to export', 'error', 'top');", true);
                }
                else
                {
                    //PdfCreater.ExportToPdf(dt, "Staff Check-In Report", Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy"));
                    string subHeading = "(" + txtFromDate.Text + " - " + txtToDate.Text + ")";
                    WordCreater.ExportToWord3("All Swipe Report", dt, subHeading);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                    "showAlert('Cannot process the report. Please try again', 'error', 'top');", true);
            }
        }

        protected void drpEmployeeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var DBConObj = new ReportClass();
            DataTable dt = DBConObj.GetDetailsWithIdManualEntry(drpEmployeeId.SelectedItem.Text);
            txtEmployeeName.Text = dt.Rows[0]["EmployeeName"].ToString();
            txtGender.Text = dt.Rows[0]["Gender"].ToString();
            txtDesignation.Text = dt.Rows[0]["Designation"].ToString();
            txtBranch.Text = dt.Rows[0]["Branch"].ToString();
            txtDepartment.Text = dt.Rows[0]["Department"].ToString();
        }
        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpBranch.SelectedValue != "All")
                fillEmployeeWithBranchID();
            else
                fillEmployee();

        }

        private void fillEmployeeWithBranchID()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetDetailsWithId(drpBranch.SelectedValue, "employeeSelectwithBranchID_ManualEntry", "@BranchID");
            drpEmployeeId.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                drpEmployeeId.Items.Clear();
                drpEmployeeId.Items.Add("All");
                drpEmployeeId.AppendDataBoundItems = true;
                drpEmployeeId.DataTextField = "EmployeeId";
                //drpEmployeeId.DataValueField = "DepartmentId";
                drpEmployeeId.DataSource = dt;
                drpEmployeeId.DataBind();

            }
            else
            {
                drpEmployeeId.Items.Clear();
            }
        }
    }
}