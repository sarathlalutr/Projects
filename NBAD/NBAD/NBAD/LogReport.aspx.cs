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
    public partial class WebForm18 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                //DataTable dt = repObj.allSwipeReport(fromdate, todate,drpEmployeeId.SelectedItem.Text);
                DataTable dt = repObj.LogReport(fromdate, todate);

                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('No Records to export', 'error', 'top');", true);
                }
                else
                {
                    string subHeading = "(" + txtFromDate.Text + " - " + txtToDate.Text + ")";
                    PdfCreater.ExportToPdf(dt, "Log Report", subHeading);
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
                //DataTable dt = repObj.allSwipeReport(fromdate, todate,drpEmployeeId.SelectedItem.Text);
                DataTable dt = repObj.LogReport(fromdate, todate);

                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('No Records to export', 'error', 'top');", true);
                }
                else
                {
                    //PdfCreater.ExportToPdf(dt, "Staff Check-In Report", Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy"));
                    string subHeading = "(" + txtFromDate.Text + " - " + txtToDate.Text + ")";
                    ExcelCreater.CreateSheet("Log Report", dt, subHeading);
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
                //DataTable dt = repObj.allSwipeReport(fromdate, todate,drpEmployeeId.SelectedItem.Text);
                DataTable dt = repObj.LogReport(fromdate, todate);

                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('No Records to export', 'error', 'top');", true);
                }
                else
                {
                    //PdfCreater.ExportToPdf(dt, "Staff Check-In Report", Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy"));
                    string subHeading = "(" + txtFromDate.Text + " - " + txtToDate.Text + ")";
                    WordCreater.ExportToWord3("Log Report", dt, subHeading);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                    "showAlert('Cannot process the report. Please try again', 'error', 'top');", true);
            }
        }
    }
}