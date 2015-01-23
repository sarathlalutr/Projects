using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var conobj = new DBConnection();
                fillLocation();
            }
        }

        private void fillLocation()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblLocationSelect");
            gridLocation.DataSource = dt;
            gridLocation.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var conobj = new DBConnection();
                int rs = conobj.InsertLocation(txtLocation.Text.Trim());
                fillLocation();
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
            txtLocation.Text = "";
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();

        }
        private void Search()
        {
            var conobj = new DBConnection();

            gridLocation.DataSource = conobj.GetSearchDetails("usp_LocationDetailsSearch", "@SearchTerm ", txtSearchBranch.Text.Trim());
            gridLocation.DataBind();
        }
        protected void gridLocation_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gridLocation.PageIndex = e.NewPageIndex;
            if (txtSearchBranch.Text == "")
            {
                fillLocation();
            }
            else
                Search();

        }

        protected void gridLocation_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                //http://www.c-sharpcorner.com/uploadfile/kannagoud/edit-update-delete-record-in-repeater-control/
                var conobj = new DBConnection();
                string res = conobj.deletewithId(e.CommandArgument.ToString(), "usp_tblLocationDelete", "@LocationId");
                if (res == "547")
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Unable to delete..It is already used!!', 'error', 'top');", true);
                fillLocation();
            }
        }
        protected void gridLocation_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

        }
    }
}