using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var conobj = new DBConnection();
                fillDescription();
            }
        }

        private void fillDescription()
        {
            var conObj = new DBConnection();
            DataTable dt = conObj.GetAllDetails("usp_tblDescriptionSelect");
            gridDescription.DataSource = dt;
            gridDescription.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var conobj = new DBConnection();
                int rs = conobj.InsertDescription(txtDescription.Text.Trim(), Session["username"].ToString(),System.DateTime.Now);
                conobj.insertLog("Insert", "Description Entry", Session["username"].ToString(), System.DateTime.Now);
                fillDescription();
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
            txtDescription.Text = "";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();

        }

        private void Search()
        {
            var conobj = new DBConnection();

            gridDescription.DataSource = conobj.GetSearchDetails("usp_DescriptionDetailsSearch", "@SearchTerm ", txtSearchBranch.Text.Trim());
            gridDescription.DataBind();
        }
         protected void gridDescription_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gridDescription.PageIndex = e.NewPageIndex;
            if (txtSearchBranch.Text == "")
            {
                fillDescription();
            }
            else
                Search();

        }

        protected void gridDescription_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                //http://www.c-sharpcorner.com/uploadfile/kannagoud/edit-update-delete-record-in-repeater-control/
                var conobj = new DBConnection();
                string res = conobj.deletewithId(e.CommandArgument.ToString(), "usp_tblDescriptionDelete", "@DescriptionId");
                if (res == "547")
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "showAlert('Unable to delete..It is already used!!', 'error', 'top');", true);
                conobj.insertLog("Delete", "Description Entry", Session["username"].ToString(), System.DateTime.Now);
                fillDescription();
            }
        }
        protected void gridDescription_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

        }
    }
}