using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var conobj = new DBConnection();
                fillDesignation();
            }
        }

        private void fillDesignation()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblDesignationSelect");
            gridDesignation.DataSource = dt;
            gridDesignation.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var conobj = new DBConnection();
                int rs = conobj.InsertDesignation(txtDesignationName.Text.Trim());
                fillDesignation();
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
            txtDesignationName.Text = "";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();

        }
        private void Search()
        {
            var conobj = new DBConnection();

            gridDesignation.DataSource = conobj.GetSearchDetails("usp_DesignationDetailsSearch", "@SearchTerm ", txtSearchBranch.Text.Trim());
            gridDesignation.DataBind();
        }
        protected void gridDesignation_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gridDesignation.PageIndex = e.NewPageIndex;
            if (txtSearchBranch.Text == "")
            {
                fillDesignation();
            }
            else
                Search();

        }

        protected void gridDesignation_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                //http://www.c-sharpcorner.com/uploadfile/kannagoud/edit-update-delete-record-in-repeater-control/
                var conobj = new DBConnection();
                string res = conobj.deletewithId(e.CommandArgument.ToString(), "usp_tblDesignationDelete", "@DesignationId");
                if (res == "547")
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Unable to delete..It is already used!!', 'error', 'top');", true);
                fillDesignation();
            }
        }
        protected void gridDesignation_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

        }
    }
}