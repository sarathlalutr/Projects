using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAD
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected static DataTable dt = new DataTable();
        protected static DataTable dtBuildings = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            if (fileuploadExcel.HasFile)
            {
                try
                {
                    string connString = "";
                    string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string path = Server.MapPath("~/App_Data/") + fileuploadExcel.FileName;
                    fileuploadExcel.SaveAs(path);

                    string fileBasePath = Server.MapPath("~/App_Data/");
                    string fileName = Path.GetFileName(this.fileuploadExcel.FileName);
                    string fullFilePath = fileBasePath + fileName;
                    //  get all lines of csv file
                    string[] str = File.ReadAllLines(fullFilePath);

                    // create new datatable
                    DataTable dt = new DataTable();

                    // get the column header means first line
                    string[] temp = str[0].Split(',');

                    // creates columns of gridview as per the header name
                    foreach (string t in temp)
                    {
                        dt.Columns.Add(t, typeof(string));
                    }

                    // now retrive the record from second line and add it to datatable
                    for (int i = 1; i < str.Length; i++)
                    {
                        string[] t = str[i].Split(',');
                        dt.Rows.Add(t);

                    }

                    // assign gridview datasource property by datatable
                    grvExcelData.DataSource = dt;

                    // bind the gridview
                    grvExcelData.DataBind();

                    if (File.Exists(path))
                        File.Delete(path);

                    insertTable(dt);

                }
                catch (Exception EX)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                    "showAlert('Please select CSV file', 'error', 'top');", true);
                }
               
            }
            else
            {
                grvExcelData.DataSource = null;
                grvExcelData.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                    "showAlert('Please select CSV file', 'error', 'top');", true);
            }
            
        }

        private void insertTable(DataTable dt)
        {
            DBConnection objDbConnection = new DBConnection();
            dtBuildings = objDbConnection.insertTODB(dt);
            //if (dtBuildings.Rows.Count > 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
            //               "showAlert('Import process finished. Please verify the data imported', 'success', 'top');", true);
            //}
        }
    }
}