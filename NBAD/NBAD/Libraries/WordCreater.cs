using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Novacode;

namespace PdfExport
{
    public static class WordCreater
    {
        public static void ExportToWord(string heading, DataTable dt, string dateFrom, string dateTo)
        {
            using (var ms = new MemoryStream())
            {
                using (DocX document = DocX.Create(string.Format("Report-{0}.doc", DateTime.Now.Ticks)))
                {
                    //// Add a Table to this document.
                    //Table t = document.AddTable(2, 3);
                    //// Specify some properties for this Table.
                    //t.Alignment = Alignment.center;
                    //t.Design = TableDesign.MediumGrid1Accent2;
                    //// Add content to this Table.
                    //t.Rows[0].Cells[0].Paragraphs.First().Append("A");
                    //t.Rows[0].Cells[1].Paragraphs.First().Append("B");
                    //t.Rows[0].Cells[2].Paragraphs.First().Append("C");
                    //t.Rows[1].Cells[0].Paragraphs.First().Append("D");
                    //t.Rows[1].Cells[1].Paragraphs.First().Append("E");
                    //t.Rows[1].Cells[2].Paragraphs.First().Append("F");
                    //// Insert the Table into the document.
                    //document.InsertTable(t);
                    document.InsertParagraph(heading);
                    document.SaveAs(ms);

                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    //response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //response.ContentType = "application/ms-word";
                    response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    //response.ContentType = "application/octet-stream";
                    //String fileName = string.Concat(heading,
                    //    DateTime.Now.ToString()
                    //        .Replace(":", string.Empty)
                    //        .Replace("/", string.Empty)
                    //        .Replace(" ", string.Empty), ".pdf");

                    String fileName = string.Concat(heading, ".doc").Replace(' ', '_');

                    response.AddHeader("content-disposition", "attachment; filename= " + fileName);
                    //System.Web.HttpContext.Current.Response.Write(pdfDoc);
                    byte[] bytes = ms.ToArray();
                    response.OutputStream.Write(bytes, 0, bytes.Length);
                    response.OutputStream.Flush();
                    response.Flush();
                    response.End();
                } // Release this document from memory.
            }
        }

        public static void ExportToWord1(string heading, DataTable dt, string dateFrom, string dateTo)
        {
            using (DocX Report = DocX.Create(string.Format("Report-{0}.doc", DateTime.Now.Ticks)))
            {
                Paragraph p = Report.InsertParagraph("Hello world");

                String fileName = string.Concat(heading, ".doc").Replace(' ', '_');

                var ms = new MemoryStream();
                //Report.SaveAs(ms);

                HttpResponse response = HttpContext.Current.Response;

                response.Clear();
                response.AddHeader("content-disposition", "attachment; filename=\"" + fileName + ".docx\"");
                response.ContentType = "application/msword";
                Report.SaveAs(response.OutputStream);

                response.End();
            }
        }

        public static void ExportToWord2(string heading, DataTable dt, string dateFrom, string dateTo)
        {
            HttpResponse response = HttpContext.Current.Response;

            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.Charset = "";
            response.ContentType = "application/vnd.ms-word";
            response.AddHeader("content-disposition", "attachment;filename=Products.doc");

            var sWriter = new StringWriter();
            var hWriter = new HtmlTextWriter(sWriter);

            var GridView1 = new GridView();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.RenderControl(hWriter);

            response.Write(sWriter.ToString());
            response.End();
        }

        public static void ExportToWord3(string heading, DataTable dt, string subHeading)
        {
            var strBody = new StringBuilder("");

            strBody.Append(
                "<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'>" +
                "<head><title>Time</title>");

            //The setting specifies document's view after it is downloaded as Print
            //instead of the default Web Layout
            strBody.Append("<!--[if gte mso 9]>" + "<xml>" + "<w:WordDocument>" + "<w:View>Print</w:View>" +
                           "<w:Zoom>90</w:Zoom>" + "<w:DoNotOptimizeForBrowser/>" + "</w:WordDocument>" + "</xml>" +
                           "<![endif]-->");

            strBody.Append("<style>" + "<!-- /* Style Definitions */" + "BODY{ font-family:arial,verdana; }" +
                           "@page Section1" + "   {size:21cm 29.7cmt; " + "   margin:1cm 1cm 1cm 1cm; " +
                           "   mso-header-margin:.5in; " +
                           "mso-page-orientation: portrait;    mso-footer-margin:.5in; mso-paper-source:0;}" +
                           " div.Section1" + "   {page:Section1;}" + "-->" + "</style></head>");

            strBody.Append("<body lang=EN-US style='tab-interval:.5in'>" + "<div class=Section1>");

            strBody.Append("<h4 align='center'>" + heading + "<br>" + subHeading + " </h4>");

            strBody.Append(
                "<table border='0' cellpadding='0' cellspacing='0' align='center' width='100%' style='font: 8px arial,verdana;'>");

            strBody.Append("<thead style='font: bold 8px arial,verdana; background-color: #b4b4b4'>");

            #region Lost Key

            if (heading == "Lost Key Report")
            {
                strBody.Append("<td style='width:5%'>");
                strBody.Append("Sl#");
                strBody.Append("</td>");
                strBody.Append("<td style='width:20%'>");
                strBody.Append("Lost by");
                strBody.Append("</td>");
                strBody.Append("<td style='width:15%'>");
                strBody.Append("Building Name");
                strBody.Append("</td>");
                strBody.Append("<td style='width:8%'>");
                strBody.Append("Block Name");
                strBody.Append("</td>");
                strBody.Append("<td style='width:7%'>");
                strBody.Append("Room No");
                strBody.Append("</td>");
                strBody.Append("<td style='width:8%'>");
                strBody.Append("Key Type");
                strBody.Append("</td>");
                strBody.Append("<td style='width:15%'>");
                strBody.Append("Door");
                strBody.Append("</td>");
                strBody.Append("<td style='width:15%'>");
                strBody.Append("Date of lost");
                strBody.Append("</td>");
                strBody.Append("<td style='width:7%'>");
                strBody.Append("Lost Keys");
                strBody.Append("</td>");
                //strBody.Append("<td style='width:80px'>");
                //strBody.Append("Date of re-issue");
                //strBody.Append("</td>");
                //strBody.Append("<td style='width:45px'>");
                //strBody.Append("Re-issued keys");
                //strBody.Append("</td>");
                //strBody.Append("<td style='width:70px'>");
                //strBody.Append("Changed by");
                //strBody.Append("</td>");
            }
            #endregion

            #region Cylinder Change

            else if (heading == "Cylinder Change Report")
            {
                strBody.Append("<td style='width:5%'>");
                strBody.Append("Sl#");
                strBody.Append("</td>");
                strBody.Append("<td style='width:10%'>");
                strBody.Append("Requested by");
                strBody.Append("</td>");
                strBody.Append("<td style='width:15%'>");
                strBody.Append("Building Name");
                strBody.Append("</td>");
                strBody.Append("<td style='width:8%'>");
                strBody.Append("Block Name");
                strBody.Append("</td>");
                strBody.Append("<td style='width:7%'>");
                strBody.Append("Room No");
                strBody.Append("</td>");
                strBody.Append("<td style='width:8%'>");
                strBody.Append("Key Type");
                strBody.Append("</td>");
                strBody.Append("<td style='width:15%'>");
                strBody.Append("Door");
                strBody.Append("</td>");
                strBody.Append("<td style='width:15%'>");
                strBody.Append("Date of Change");
                strBody.Append("</td>");
                strBody.Append("<td style='width:7%'>");
                strBody.Append("New Keys");
                strBody.Append("</td>");
                strBody.Append("<td style='width:10%'>");
                strBody.Append("Changed By");
                strBody.Append("</td>");
            }
            #endregion

            #region kEY iNVENTORY

            else if (heading == "Key Inventory Report")
            {
                strBody.Append("<td style='width:3%'>");
                strBody.Append("Sl#");
                strBody.Append("</td>");
                strBody.Append("<td style='width:10%'>");
                strBody.Append("Building ");
                strBody.Append("</td>");
                strBody.Append("<td style='width:6%'>");
                strBody.Append("Block");
                strBody.Append("</td>");
                strBody.Append("<td style='width:7%'>");
                strBody.Append("Apartment");
                strBody.Append("</td>");
                strBody.Append("<td style='width:6%'>");
                strBody.Append("Room #");
                strBody.Append("</td>");
                strBody.Append("<td style='width:5%'>");
                strBody.Append("Occupant");
                strBody.Append("</td>");
                strBody.Append("<td style='width:10%'>");
                strBody.Append("Key Type");
                strBody.Append("</td>");
                strBody.Append("<td style='width:5%'>");
                strBody.Append("Shared?");
                strBody.Append("</td>");
                strBody.Append("<td style='width:9%'>");
                strBody.Append("Door");
                strBody.Append("</td>");
                strBody.Append("<td align=\"center\" style='width:6%'>");
                strBody.Append("Total Keys");
                strBody.Append("</td>");
                strBody.Append("<td align=\"center\" style='width:5%'>");
                strBody.Append("Spare");
                strBody.Append("</td>");
                strBody.Append("<td align=\"center\" style='width:5%'>");
                strBody.Append("Lost");
                strBody.Append("</td>");
                strBody.Append("<td align=\"center\" style='width:10%'>");
                strBody.Append("Cylinder Changed");
                strBody.Append("</td>");
                strBody.Append("<td align=\"center\" style='width:13%'>");
                strBody.Append("No. Of Cylinder Change");
                strBody.Append("</td>");
            }
            #endregion

            else
            {
                strBody.Append("<td style='width:30px'>");
                strBody.Append("Sl#");
                strBody.Append("</td>");
                strBody.Append("<td style='width:35px'>");
                strBody.Append("Log#");
                strBody.Append("</td>");
                strBody.Append("<td style='width:75px'>");
                strBody.Append("First Name");
                strBody.Append("</td>");
                strBody.Append("<td style='width:75px'>");
                strBody.Append("Last Name");
                strBody.Append("</td>");
                strBody.Append("<td style='width:60px'>");
                strBody.Append("Contact#");
                strBody.Append("</td>");
                strBody.Append("<td style='width:60px'>");
                strBody.Append("Parking Tag#");
                strBody.Append("</td>");
                strBody.Append("<td style='width:60px'>");
                strBody.Append("Parking Slot#");
                strBody.Append("</td>");
                strBody.Append("<td style='width:60px'>");
                strBody.Append("Vehicle#");
                strBody.Append("</td>");
                strBody.Append("<td style='width:70px'>");
                strBody.Append("Check-In Date");
                strBody.Append("</td>");
                strBody.Append("<td style='width:70px'>");
                strBody.Append("Building Name");
                strBody.Append("</td>");
                strBody.Append("<td style='width:50px'>");
                strBody.Append("Block Name");
                strBody.Append("</td>");
                strBody.Append("<td style='width:50px'>");
                strBody.Append("Room No");
                strBody.Append("</td>");
                strBody.Append("<td style='width:50px'>");
                strBody.Append("Incharge");
                strBody.Append("</td>");
            }
            strBody.Append("</thead>");

            foreach (DataRow dr in dt.Rows) // Adding Data into rows
            {
                strBody.Append("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    strBody.Append("<td>");
                    strBody.Append(dr[dc.ColumnName]);
                    strBody.Append("</td>");
                }
                strBody.Append("</tr>");
            }

            strBody.Append("</table>");


            strBody.Append("</div></body></html>");

            //str1.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            //str1.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div>");
            //str1.Append("<DIV  style='font-size:12px;'>");
            //str1.Append(str.ToString());
            //str1.Append("</div></body></html>");
            string strFile = string.Concat(heading, ".doc").Replace(" ", "_").Replace("-", "");
            string strcontentType = "application/word";

            HttpResponse response = HttpContext.Current.Response;

            response.ClearContent();
            response.ClearHeaders();
            response.BufferOutput = true;
            response.ContentType = strcontentType;

            response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            response.Write(strBody.ToString());
            response.Flush();
            response.Close();
            response.End();
        }

        public static void ExportHTMLToWord3(string heading, String strBody)
        {
            string strFile = string.Concat(heading, ".doc").Replace(" ", "_").Replace("-", "");
            string strcontentType = "application/word";

            HttpResponse response = HttpContext.Current.Response;

            response.ClearContent();
            response.ClearHeaders();
            response.BufferOutput = true;
            response.ContentType = strcontentType;

            response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            response.Write(strBody);
            response.Flush();
            response.Close();
            response.End();
        }
    }
}