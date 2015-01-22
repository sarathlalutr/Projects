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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillBranch();
            }
        }

        private void fillBranch()
        {
            var branchTable = new DataTable();
            DBConnection objDbConnection = new DBConnection();
            branchTable = objDbConnection.getBranchAll();
            drpBranch.DataSource = null;
            if (branchTable.Rows.Count > 0)
            {
                drpBranch.Items.Clear();
                drpBranch.Items.Add("-select-");
                drpBranch.AppendDataBoundItems = true;
                drpBranch.DataTextField = "Branch";
                //drpBuilding.DataValueField = "BuildingDetailsId";
                drpBranch.DataSource = branchTable;
                drpBranch.DataBind();
                //drpdEmpCategory.Enabled = true;
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                var repObj = new ReportClass();
                DataTable dt = repObj.BranchReport(drpBranch.SelectedItem.Text.Trim());

                if (dt == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('No Records to export', 'error', 'top');", true);
                }
                else
                {
                    //PdfCreater.ExportToPdf(dt, "Staff Check-In Report", Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy"));
                    //string subHeading = "(" + drpDesignation.SelectedItem.Text + " - " + System.DateTime.Now.ToString()+ ")";
                    string subHeading = "Branch  :  " + drpBranch.SelectedItem.Text + "           " + "Date  :  " +
                                        System.DateTime.Now.ToString("dd/MM/yyyy");
                    ExcelCreater.CreateSheet("Branchwise Report", dt,  subHeading);
                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var repObj = new ReportClass();
                DataTable dt = repObj.BranchReport(drpBranch.SelectedItem.Text.Trim());

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