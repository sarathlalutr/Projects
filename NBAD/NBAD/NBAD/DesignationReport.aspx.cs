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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDesignation();
            }
        }

        private void fillDesignation()
        {
            var designationTable = new DataTable();
            DBConnection objDbConnection=new DBConnection();
            designationTable = objDbConnection.getDesignationAll();
            drpDesignation.DataSource = null;
            if (designationTable.Rows.Count > 0)
            {
                drpDesignation.Items.Clear();
                drpDesignation.Items.Add("-select-");
                drpDesignation.AppendDataBoundItems = true;
                drpDesignation.DataTextField = "Designation";
                //drpBuilding.DataValueField = "BuildingDetailsId";
                drpDesignation.DataSource = designationTable;
                drpDesignation.DataBind();
                //drpdEmpCategory.Enabled = true;
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                var repObj = new ReportClass();
                DataTable dt = repObj.DesigantionReport(drpDesignation.SelectedItem.Text.Trim());

                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('No Records to export', 'error', 'top');", true);
                }
                else
                {
                    //PdfCreater.ExportToPdf(dt, "Staff Check-In Report", Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy"));
                    //string subHeading = "(" + drpDesignation.SelectedItem.Text + " - " + System.DateTime.Now.ToString()+ ")";
                    string subHeading = "Designation  :  " + drpDesignation.SelectedItem.Text + "           " + "Date  :  " +
                                        System.DateTime.Now.ToString("dd/MM/yyyy");
                    ExcelCreater.CreateSheet("Designationwise Report", dt, "Designation:"+subHeading);
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }

        protected void drpDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var repObj = new ReportClass();
                DataTable dt = repObj.DesigantionReport(drpDesignation.SelectedItem.Text.Trim());

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