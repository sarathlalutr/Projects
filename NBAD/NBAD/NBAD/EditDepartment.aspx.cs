using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class EditDepartment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["Id"];

                ViewState["DepartmentId"] = id;
                var conobj = new DBConnection();
                var allData = new DataTable();
                allData = conobj.GetDetailsWithId(id, "usp_tblDepartmentSelect", "@DepartmentId");
                fillDetails(allData);
            }
        }

        private void fillDetails(DataTable allData)
        {
            txtDepartmentName.Text = allData.Rows[0]["DepartmentName"].ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var conobj = new DBConnection();
            conobj.updateDepartment(txtDepartmentName.Text.Trim(),
                ViewState["DepartmentId"].ToString());
            conobj.insertLog("Update", "Department", Session["username"].ToString(), System.DateTime.Now);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + "Successfully Updated" + "');</script>", false);

            Server.Transfer("departmentEntry.aspx", true);
        }
    }
}