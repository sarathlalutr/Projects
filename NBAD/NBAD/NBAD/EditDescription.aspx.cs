using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class EditDescription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string id = Request.QueryString["Id"];

                ViewState["DescriptionId"] = id;
                var conobj = new DBConnection();
                var allData = new DataTable();
                allData = conobj.GetDetailsWithId(id, "usp_tblDescriptionSelect", "@DescriptionId");
                fillDetails(allData);
            }
        }

        private void fillDetails(DataTable allData)
        {
            txtDescription.Text = allData.Rows[0]["Description"].ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var conobj = new DBConnection();
                conobj.updateDescription(txtDescription.Text.Trim(),
                    ViewState["DescriptionId"].ToString());
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + "Successfully Updated" + "');</script>", false);

                Server.Transfer("descriptionEntry.aspx", true);
            }
            catch (Exception ex)
            {
                
            }
            
        }
    }
}