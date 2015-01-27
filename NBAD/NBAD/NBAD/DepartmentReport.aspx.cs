using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libraries;

namespace NBAD
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDepartment();
            }
        }

        private void fillDepartment()
        {
            var DEPARTMENTTable = new DataTable();
            DBConnection objDbConnection = new DBConnection();
            DEPARTMENTTable = objDbConnection.getDepartmentAll();
            drpDepartment.DataSource = null;
            if (DEPARTMENTTable.Rows.Count > 0)
            {
                drpDepartment.Items.Clear();
                drpDepartment.Items.Add("-select-");
                drpDepartment.AppendDataBoundItems = true;
                drpDepartment.DataTextField = "DepartmentName";
                drpDepartment.DataValueField = "DepartmentId";
                drpDepartment.DataSource = DEPARTMENTTable;
                drpDepartment.DataBind();
                //drpdEmpCategory.Enabled = true;
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                var repObj = new ReportClass();
                //DataTable dt = repObj.DepartmentReport(drpDepartment.SelectedItem.Text.Trim());
                DataTable dt = repObj.DepartmentReport(drpDepartment.SelectedValue);

                //grvExcelData.DataSource = dt;

                //// bind the gridview
                //grvExcelData.DataBind();

                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('No Records to export', 'error', 'top');", true);
                }
                else
                {
                    //PdfCreater.ExportToPdf(dt, "Staff Check-In Report", Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy"));
                    //string subHeading = "(" + drpDesignation.SelectedItem.Text + " - " + System.DateTime.Now.ToString()+ ")";
                    string subHeading = "Department  :  " + drpDepartment.SelectedItem.Text + "           " + "Date  : " +
                                        System.DateTime.Now.ToString("dd/MM/yyyy");
                    ExcelCreater.CreateSheet("Departmentwise Report", dt, subHeading);
                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var repObj = new ReportClass();
                //DataTable dt = repObj.DepartmentReport(drpDepartment.SelectedItem.Text.Trim());
                DataTable dt = repObj.DepartmentReport(drpDepartment.SelectedValue);
                grvExcelData.DataSource = dt;

                // bind the gridview
                grvExcelData.DataBind();
            }
            catch (Exception ex)
            {
                
                
            }
            
        }
    }
}