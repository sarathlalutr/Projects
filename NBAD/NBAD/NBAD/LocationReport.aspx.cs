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
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillLocation();
            }
        }

        private void fillLocation()
        {
            var locationTable = new DataTable();
            DBConnection objDbConnection = new DBConnection();
            locationTable = objDbConnection.getLocationAll();
            drpLocation.DataSource = null;
            if (locationTable.Rows.Count > 0)
            {
                drpLocation.Items.Clear();
                drpLocation.Items.Add("-select-");
                drpLocation.AppendDataBoundItems = true;
                drpLocation.DataTextField = "Location";
                //drpBuilding.DataValueField = "BuildingDetailsId";
                drpLocation.DataSource = locationTable;
                drpLocation.DataBind();
                //drpdEmpCategory.Enabled = true;
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                var repObj = new ReportClass();
                DataTable dt = repObj.LocationReport(drpLocation.SelectedItem.Text.Trim());

                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('No Records to export', 'error', 'top');", true);
                }
                else
                {
                    //PdfCreater.ExportToPdf(dt, "Staff Check-In Report", Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy"));
                    //string subHeading = "(" + drpDesignation.SelectedItem.Text + " - " + System.DateTime.Now.ToString()+ ")";
                    string subHeading = "Location :   " + drpLocation.SelectedItem.Text.Trim() + "           " + "Date :  " +
                                        System.DateTime.Now.ToString("dd/MM/yyyy");
                    ExcelCreater.CreateSheet("Locationwise Report", dt, subHeading);
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }

        protected void drpLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var repObj = new ReportClass();
            DataTable dt = repObj.LocationReport(drpLocation.SelectedItem.Text.Trim());

            grvExcelData.DataSource = dt;

            // bind the gridview
            grvExcelData.DataBind();
        }
    }
}