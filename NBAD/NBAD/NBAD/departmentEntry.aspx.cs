using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var conobj = new DBConnection();
                fillDepartment();
            }
        }

        private void fillDepartment()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblDepartmentSelect");
            gridDepartment.DataSource = dt;
            gridDepartment.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var conobj = new DBConnection();
                int rs = conobj.InsertDepartment(txtDepartmentName.Text.Trim(), Session["username"].ToString(), System.DateTime.Now);
                conobj.insertLog("Insert", "Department Entry", Session["username"].ToString(), System.DateTime.Now);
                fillDepartment();
                clearfields();
                if (rs > 0)
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Record saved successfully!', 'success', 'top');", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Record already exist', 'error', 'top');", true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        private void clearfields()
        {
            txtDepartmentName.Text = "";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();

        }

        private void Search()
        {
            var conobj = new DBConnection();

            gridDepartment.DataSource = conobj.GetSearchDetails("usp_DepartmentDetailsSearch", "@SearchTerm ", txtSearchBranch.Text.Trim());
            gridDepartment.DataBind();
        }
        protected void gridDepartment_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gridDepartment.PageIndex = e.NewPageIndex;
            if (txtSearchBranch.Text == "")
            {
                fillDepartment();
            }
            else
                Search();

        }

        protected void gridDepartment_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                //http://www.c-sharpcorner.com/uploadfile/kannagoud/edit-update-delete-record-in-repeater-control/
                var conobj = new DBConnection();
                string res = conobj.deletewithId(e.CommandArgument.ToString(), "usp_tblDepartmentDelete", "@DepartmentId");
                if (res == "547")
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Unable to delete..It is already used!!', 'error', 'top');", true);
                conobj.insertLog("Delete", "Department Entry", Session["username"].ToString(), System.DateTime.Now);
                fillDepartment();
            }
        }
        protected void gridDepartment_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

        }
    }
}