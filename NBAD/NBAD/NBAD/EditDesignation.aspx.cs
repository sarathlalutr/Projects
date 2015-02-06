using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class EditDesignation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["Id"];

                ViewState["DesignationId"] = id;
                var conobj = new DBConnection();
                var allData = new DataTable();
                allData = conobj.GetDetailsWithId(id, "usp_tblDesignationSelect", "@DesignationId");
                fillDetails(allData);
            }
        }

        private void fillDetails(DataTable allData)
        {
            txtDesignationName.Text = allData.Rows[0]["DesignationName"].ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var conobj = new DBConnection();
            conobj.updateDesignation(txtDesignationName.Text.Trim(),
                ViewState["DesignationId"].ToString());
            conobj.insertLog("Update", "Designation", Session["username"].ToString(), System.DateTime.Now);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + "Successfully Updated" + "');</script>", false);

            Server.Transfer("designationEntry.aspx", true);
        }
        }
    
}