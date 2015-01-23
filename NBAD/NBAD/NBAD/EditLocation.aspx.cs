using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class EditLocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string id = Request.QueryString["Id"];

                ViewState["DesignationId"] = id;
                var conobj = new DBConnection();
                var allData = new DataTable();
                allData = conobj.GetDetailsWithId(id, "usp_tblLocationSelect", "@LocationId");
                fillDetails(allData);
            }
        }

        private void fillDetails(DataTable allData)
        {
            txtLocation.Text = allData.Rows[0]["LocationName"].ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var conobj = new DBConnection();
            conobj.updateLocation(txtLocation.Text.Trim(),
                ViewState["DesignationId"].ToString());
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + "Successfully Updated" + "');</script>", false);

            Server.Transfer("locationEntry.aspx", true);
        }
    }
}