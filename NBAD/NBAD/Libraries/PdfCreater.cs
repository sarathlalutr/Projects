using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Image = iTextSharp.text.Image;

namespace PdfExport
{
    public static class PdfCreater
    {
        public static bool ExportToPdf1(DataTable dt, string filename)
        {
            var GridView1 = new GridView();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            HttpResponse Response = HttpContext.Current.Response;

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".pdf");
            var sWriter = new StringWriter();
            var hTWriter = new HtmlTextWriter(sWriter);
            GridView1.RenderControl(hTWriter);
            var sReader = new StringReader(sWriter.ToString());
            var pdf = new Document(PageSize.A4);
            var worker = new HTMLWorker(pdf);
            PdfWriter.GetInstance(pdf, Response.OutputStream);
            pdf.Open();
            worker.Parse(sReader);
            pdf.Close();
            Response.Write(pdf);
            Response.Flush();
            Response.End();

            return true;
        }

        /// <summary>
        ///     http://www.technologycrowds.com/2013/09/how-can-we-export-datatable-to-pdf.html#.U7Upz_mSzX0
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="heading"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="subHeading"></param>
        /// <returns></returns>
        public static bool ExportToPdf(DataTable dt, string heading, string subHeading)
        {
            using (var ms = new MemoryStream())
            {
                //Document pdfDoc = new Document(PageSize.A4, 10, 10, 5, 5);
                var pdfDoc = new Document(PageSize.A4, 5, 5, 5, 50);

                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                //pdfWriter.PageEvent = new ITextEvents();
                //pdfWriter.PageEvent = new itsEventsHandler();
                ITextEvents obj=new ITextEvents();
                pdfWriter.PageEvent = obj;

                pdfDoc.Open();

                #region Header

                string imagepath = HttpContext.Current.Server.MapPath("Images");
                //Image logo = Image.GetInstance(imagepath + "/report_logo.jpg");
                Image logo = Image.GetInstance(imagepath + "/NBADLogo.PNG");
                logo.ScaleToFit(80f, 60f);
                logo.Alignment = Image.TEXTWRAP | Element.ALIGN_RIGHT;
                pdfDoc.Add(logo);

                #endregion

                #region Footer

                //Footer Image 
                //string footerimagepath = HttpContext.Current.Server.MapPath("images");
                //iTextSharp.text.Image imgfoot = iTextSharp.text.Image.GetInstance(imagepath + "/report_footer.jpg");
                //imgfoot.SetAbsolutePosition(0, 0);

                //pdfDoc.AddImage(imgfoot);
                //ITextEvents obj = new ITextEvents();
                //obj.OnOpenDocument(pdfWriter,pdfDoc);
                //obj.OnEndPage(pdfWriter,pdfDoc);
                //obj.OnCloseDocument(pdfWriter,pdfDoc);
                
                #endregion

                var c = new Chunk("" + heading + "", FontFactory.GetFont("Verdana", 8, Font.BOLD));
                var p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(c);
                pdfDoc.Add(p);

                var d = new Chunk(subHeading, FontFactory.GetFont("Verdana", 7));
                var p2 = new Paragraph();
                p2.Alignment = Element.ALIGN_CENTER;
                p2.Add(d);
                p2.SpacingAfter = 15f;
                pdfDoc.Add(p2);

                #region Image insert

                //string clientLogo = Server.MapPath(".") + "/logo/tpglogo.jpg";
                //string imageFilePath = Server.MapPath(".") + "/logo/tpglogo.jpg";
                //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
                ////Resize image depend upon your need   
                //jpg.ScaleToFit(80f, 60f);
                ////Give space before image   
                //jpg.SpacingBefore = 0f;
                ////Give some space after the image   
                //jpg.SpacingAfter = 1f;
                //jpg.Alignment = Element.HEADER;
                //pdfDoc.Add(jpg); 

                #endregion

                Font font8 = FontFactory.GetFont("ARIAL", 5);
                Font headFont = FontFactory.GetFont("ARIAL", 4, Font.BOLD);

                //Font font8 = FontFactory.GetFont("ARIAL", 7);
                //Font headFont = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                if (dt != null)
                {
                    //Craete instance of the pdf table and set the number of column in that table  
                    var PdfTable = new PdfPTable(dt.Columns.Count);
                    //PdfTable.WidthPercentage = 100;

                    float[] widths;
                    if (heading == "Lost Key Report")
                    {
                        font8 = FontFactory.GetFont("ARIAL", 7);
                        headFont = FontFactory.GetFont("ARIAL", 6, Font.BOLD);

                        PdfTable.WidthPercentage = 100;

                        widths = new[] { 5f, 20f, 15f, 8f, 7f, 8f, 15f, 15f, 7f };
                    }
                    else if (heading == "Cylinder Change Report")
                    {
                        PdfTable.WidthPercentage = 100;

                        widths = new[] { 5f, 10f, 15f, 8f, 7f, 8f, 15f, 15f, 7f, 10f };
                    }
                    else if (heading == "Key Inventory Report")
                    {
                        PdfTable.WidthPercentage = 100;

                        widths = new[] { 3f, 7f, 6f, 6f, 6f, 10f, 4f, 4f, 7f, 5f, 4f, 4f, 8f, 8f };
                    }
                    else if (heading == "Manual Entry Report")
                    {
                        PdfTable.TotalWidth = 590f;
                        //widths = new[] { 20f, 30f, 60f, 70f, 60f, 50f, 50f, 50f, 60f, 50f, 35f, 30f, 30f };
                        widths = new[] { 40f, 60f, 30f, 50f, 50f, 50f,50F,40,50F,50,50,50f };
                    }
                    else if (heading == "Worked Day Report")
                    {
                        PdfTable.TotalWidth = 590f;
                        //widths = new[] { 20f, 30f, 60f, 70f, 60f, 50f, 50f, 50f, 60f, 50f, 35f, 30f, 30f };
                        widths = new[] { 40f, 60f, 30f, 50f, 50f, 50f, 50F };
                    }
                    else if (heading == "All Swipe Report")
                    {
                        PdfTable.TotalWidth = 590f;
                        //widths = new[] { 20f, 30f, 60f, 70f, 60f, 50f, 50f, 50f, 60f, 50f, 35f, 30f, 30f };
                        widths = new[] { 40f, 60f, 30f, 50f, 50f, 50f, 50F, 40, 50F, 50f};
                    }
                    else if (heading == "Punctuality Report")
                    {
                        PdfTable.TotalWidth = 590f;
                        //widths = new[] { 20f, 30f, 60f, 70f, 60f, 50f, 50f, 50f, 60f, 50f, 35f, 30f, 30f };
                        widths = new[] { 40f, 60f, 30f, 50f, 50f, 50f  };
                    }
                    else if (heading == "Log Report")
                    {
                        PdfTable.TotalWidth = 590f;
                        //widths = new[] { 20f, 30f, 60f, 70f, 60f, 50f, 50f, 50f, 60f, 50f, 35f, 30f, 30f };
                        widths = new[] { 40f, 60f, 30f, 50f };
                    }

                    else 
                    {
                        PdfTable.TotalWidth = 590f;
                        //widths = new[] { 20f, 30f, 60f, 70f, 60f, 50f, 50f, 50f, 60f, 50f, 35f, 30f, 30f };
                        widths = new[] { 40f, 60f, 30f, 50f, 50f, 50f, 50F, 40, 50F, 50f };
                    }
                    //float[] widths = new float[] { 20f, 20f, 80f, 80f, 60f, 50f, 50f, 50f, 80f, 80f, 130f, 100f };
                    PdfPCell pCell = null;

                    foreach (DataColumn col in dt.Columns)
                    {
                        pCell = new PdfPCell(new Phrase(col.ColumnName, headFont));
                        PdfTable.SetWidths(widths);
                        pCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        pCell.BackgroundColor = new BaseColor(220, 220, 220);
                        PdfTable.AddCell(pCell);
                        //sarat changes
                        //pCell.NoWrap = true;
                        //pCell.Padding = 0;
                    }

                    pCell = null;
                    for (int rows = 0; rows < dt.Rows.Count; rows++)
                    {
                        for (int column = 0; column < dt.Columns.Count; column++)
                        {
                            pCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), font8)));

                            PdfTable.AddCell(pCell);
                            //sarat changes

                            pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            pCell.NoWrap = false;
                            pCell.Padding = 0;
                        }
                    }
                    //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table 
                    pdfDoc.Add(PdfTable); // add pdf table to the document   
                }
                pdfDoc.Close();
                HttpResponse response = HttpContext.Current.Response;
                response.ContentType = "application/pdf";
                //response.ContentType = "application/octet-stream";
                //String fileName = string.Concat(heading,
                //    DateTime.Now.ToString()
                //        .Replace(":", string.Empty)
                //        .Replace("/", string.Empty)
                //        .Replace(" ", string.Empty), ".pdf");

                String fileName = string.Concat(heading, ".pdf").Replace(' ', '_').Replace("-", "");

                response.AddHeader("content-disposition", "attachment; filename= " + fileName);
                //System.Web.HttpContext.Current.Response.Write(pdfDoc);
                byte[] bytes = ms.ToArray();
                response.OutputStream.Write(bytes, 0, bytes.Length);


               

                response.OutputStream.Flush();
                response.Flush();
                response.End();
            }
            //HttpContext.Current.ApplicationInstance.CompleteRequest();  
            return true;
        }

        public static bool ExportToPdf(DataSet ds, string heading, string subHeading, string reportName)
        {
            using (var ms = new MemoryStream())
            {
                //Document pdfDoc = new Document(PageSize.A4, 10, 10, 5, 5);
                var pdfDoc = new Document(PageSize.A4, 5, 5, 5, 50);

                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                //pdfWriter.PageEvent = new ITextEvents();
                pdfWriter.PageEvent = new itsEventsHandler();

                pdfDoc.Open();

                #region Header

                string imagepath = HttpContext.Current.Server.MapPath("images");
                Image logo = Image.GetInstance(imagepath + "/report_logo.jpg");
                logo.ScaleToFit(80f, 60f);
                logo.Alignment = Image.TEXTWRAP | Element.ALIGN_RIGHT;
                pdfDoc.Add(logo);

                #endregion

                #region Footer

                //Footer Image 
                //string footerimagepath = HttpContext.Current.Server.MapPath("images");
                //iTextSharp.text.Image imgfoot = iTextSharp.text.Image.GetInstance(imagepath + "/report_footer.jpg");
                //imgfoot.SetAbsolutePosition(0, 0);

                //pdfDoc.AddImage(imgfoot);

                #endregion

                var c = new Chunk("" + heading + "", FontFactory.GetFont("Verdana", 8, Font.BOLD));
                var p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(c);
                pdfDoc.Add(p);

                var d = new Chunk(subHeading, FontFactory.GetFont("Verdana", 7));
                var p2 = new Paragraph();
                p2.Alignment = Element.ALIGN_CENTER;
                p2.Add(d);
                p2.SpacingAfter = 15f;
                pdfDoc.Add(p2);

                #region Image insert

                //string clientLogo = Server.MapPath(".") + "/logo/tpglogo.jpg";
                //string imageFilePath = Server.MapPath(".") + "/logo/tpglogo.jpg";
                //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
                ////Resize image depend upon your need   
                //jpg.ScaleToFit(80f, 60f);
                ////Give space before image   
                //jpg.SpacingBefore = 0f;
                ////Give some space after the image   
                //jpg.SpacingAfter = 1f;
                //jpg.Alignment = Element.HEADER;
                //pdfDoc.Add(jpg); 

                #endregion

                Font font8 = FontFactory.GetFont("ARIAL", 8);
                Font headFont = FontFactory.GetFont("ARIAL", 7, Font.BOLD);

                Font font7 = FontFactory.GetFont("ARIAL", 7);
                Font headFont6 = FontFactory.GetFont("ARIAL", 6, Font.BOLD);


                float[] widths;
                PdfPCell pCell = null;
                if (reportName == "All Key")
                {
                    #region Tables[0]

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var issued = new Chunk("Total Key Issued", FontFactory.GetFont("Verdana", 8));
                        var pIssued = new Paragraph();
                        pIssued.Add(issued);
                        pIssued.SpacingAfter = 5f;
                        pdfDoc.Add(pIssued);

                        //Craete instance of the pdf table and set the number of column in that table  
                        var pdfTable = new PdfPTable(ds.Tables[0].Columns.Count);
                        pdfTable.WidthPercentage = 100;
                        widths = new[] { 5f, 15f, 10f, 10f, 10f, 10f, 20f, 20f };

                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            pCell = new PdfPCell(new Phrase(col.ColumnName, headFont));
                            pdfTable.SetWidths(widths);
                            pCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pCell.BackgroundColor = new BaseColor(220, 220, 220);
                            pdfTable.AddCell(pCell);
                            //sarat changes
                            //pCell.NoWrap = true;
                            //pCell.Padding = 0;
                        }

                        pCell = null;
                        for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                        {
                            for (int column = 0; column < ds.Tables[0].Columns.Count; column++)
                            {
                                pCell =
                                    new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[rows][column].ToString(), font8)));

                                pdfTable.AddCell(pCell);
                                //sarat changes

                                pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pCell.NoWrap = false;
                                pCell.Padding = 0;
                            }
                        }
                        //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table 
                        pdfDoc.Add(pdfTable); // add pdf table to the document   
                    }

                    #endregion

                    #region Tables[1]

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        var issued = new Chunk("Total Key Returned", FontFactory.GetFont("Verdana", 8));
                        var pIssued = new Paragraph();
                        pIssued.Add(issued);
                        pIssued.SpacingAfter = 5f;
                        pdfDoc.Add(pIssued);

                        //Craete instance of the pdf table and set the number of column in that table  
                        var pdfTable = new PdfPTable(ds.Tables[1].Columns.Count);
                        pdfTable.WidthPercentage = 100;
                        widths = new[] { 5f, 15f, 10f, 10f, 10f, 10f, 20f, 20f };

                        foreach (DataColumn col in ds.Tables[1].Columns)
                        {
                            pCell = new PdfPCell(new Phrase(col.ColumnName, headFont));
                            pdfTable.SetWidths(widths);
                            pCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pCell.BackgroundColor = new BaseColor(220, 220, 220);
                            pdfTable.AddCell(pCell);
                            //sarat changes
                            //pCell.NoWrap = true;
                            //pCell.Padding = 0;
                        }

                        pCell = null;
                        for (int rows = 0; rows < ds.Tables[1].Rows.Count; rows++)
                        {
                            for (int column = 0; column < ds.Tables[1].Columns.Count; column++)
                            {
                                pCell =
                                    new PdfPCell(new Phrase(new Chunk(ds.Tables[1].Rows[rows][column].ToString(), font8)));

                                pdfTable.AddCell(pCell);
                                //sarat changes

                                pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pCell.NoWrap = false;
                                pCell.Padding = 0;
                            }
                        }
                        //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table 
                        pdfDoc.Add(pdfTable); // add pdf table to the document   
                    }

                    #endregion

                    #region Tables[2]

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        var issued = new Chunk("Total Key Lost", FontFactory.GetFont("Verdana", 8));
                        var pIssued = new Paragraph();
                        pIssued.Add(issued);
                        pIssued.SpacingAfter = 5f;
                        pdfDoc.Add(pIssued);

                        //Craete instance of the pdf table and set the number of column in that table  
                        var pdfTable = new PdfPTable(ds.Tables[2].Columns.Count);
                        pdfTable.WidthPercentage = 100;
                        widths = new[] { 5f, 15f, 10f, 10f, 10f, 10f, 20f, 20f };

                        foreach (DataColumn col in ds.Tables[2].Columns)
                        {
                            pCell = new PdfPCell(new Phrase(col.ColumnName, headFont));
                            pdfTable.SetWidths(widths);
                            pCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pCell.BackgroundColor = new BaseColor(220, 220, 220);
                            pdfTable.AddCell(pCell);
                            //sarat changes
                            //pCell.NoWrap = true;
                            //pCell.Padding = 0;
                        }

                        pCell = null;
                        for (int rows = 0; rows < ds.Tables[2].Rows.Count; rows++)
                        {
                            for (int column = 0; column < ds.Tables[2].Columns.Count; column++)
                            {
                                pCell =
                                    new PdfPCell(new Phrase(new Chunk(ds.Tables[2].Rows[rows][column].ToString(), font8)));

                                pdfTable.AddCell(pCell);
                                //sarat changes

                                pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pCell.NoWrap = false;
                                pCell.Padding = 0;
                            }
                        }
                        //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table 
                        pdfDoc.Add(pdfTable); // add pdf table to the document   
                    }

                    #endregion

                    #region Tables[3]

                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        var issued = new Chunk("Total Cylinder Change", FontFactory.GetFont("Verdana", 8));
                        var pIssued = new Paragraph();
                        pIssued.Add(issued);
                        pIssued.SpacingAfter = 5f;
                        pdfDoc.Add(pIssued);

                        //Craete instance of the pdf table and set the number of column in that table  
                        var pdfTable = new PdfPTable(ds.Tables[3].Columns.Count);
                        pdfTable.WidthPercentage = 100;
                        widths = new[] { 5f, 15f, 10f, 10f, 10f, 10f, 20f, 20f };

                        foreach (DataColumn col in ds.Tables[3].Columns)
                        {
                            pCell = new PdfPCell(new Phrase(col.ColumnName, headFont));
                            pdfTable.SetWidths(widths);
                            pCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pCell.BackgroundColor = new BaseColor(220, 220, 220);
                            pdfTable.AddCell(pCell);
                            //sarat changes
                            //pCell.NoWrap = true;
                            //pCell.Padding = 0;
                        }

                        pCell = null;
                        for (int rows = 0; rows < ds.Tables[3].Rows.Count; rows++)
                        {
                            for (int column = 0; column < ds.Tables[3].Columns.Count; column++)
                            {
                                pCell =
                                    new PdfPCell(new Phrase(new Chunk(ds.Tables[3].Rows[rows][column].ToString(), font8)));

                                pdfTable.AddCell(pCell);
                                //sarat changes

                                pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pCell.NoWrap = false;
                                pCell.Padding = 0;
                            }
                        }
                        //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table 
                        pdfDoc.Add(pdfTable); // add pdf table to the document   
                    }

                    #endregion
                }
                else if (reportName == "User Activity")
                {
                    #region Tables[0]

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var issued = new Chunk("Check-In Activity", FontFactory.GetFont("Verdana", 8));
                        var pIssued = new Paragraph();
                        pIssued.Add(issued);
                        pIssued.SpacingAfter = 5f;
                        pdfDoc.Add(pIssued);

                        //Craete instance of the pdf table and set the number of column in that table  
                        var pdfTable = new PdfPTable(ds.Tables[0].Columns.Count);
                        pdfTable.WidthPercentage = 100;
                        widths = new[] { 5f, 15f, 10f, 15f, 15f, 7f, 10f, 8f, 15f };

                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            pCell = new PdfPCell(new Phrase(col.ColumnName, headFont6));
                            pdfTable.SetWidths(widths);
                            pCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pCell.BackgroundColor = new BaseColor(220, 220, 220);
                            pdfTable.AddCell(pCell);
                            //sarat changes
                            //pCell.NoWrap = true;
                            //pCell.Padding = 0;
                        }

                        pCell = null;
                        for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                        {
                            for (int column = 0; column < ds.Tables[0].Columns.Count; column++)
                            {
                                pCell =
                                    new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[rows][column].ToString(), font7)));

                                pdfTable.AddCell(pCell);
                                //sarat changes

                                pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pCell.NoWrap = false;
                                pCell.Padding = 0;
                            }
                        }
                        //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table 
                        pdfDoc.Add(pdfTable); // add pdf table to the document   
                    }

                    #endregion

                    #region Tables[1]

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        var issued = new Chunk("Check-Out Activity", FontFactory.GetFont("Verdana", 8));
                        var pIssued = new Paragraph();
                        pIssued.Add(issued);
                        pIssued.SpacingAfter = 5f;
                        pdfDoc.Add(pIssued);

                        //Craete instance of the pdf table and set the number of column in that table  
                        var pdfTable = new PdfPTable(ds.Tables[1].Columns.Count);
                        pdfTable.WidthPercentage = 100;
                        widths = new[] { 5f, 15f, 10f, 15f, 15f, 7f, 10f, 8f, 15f };

                        foreach (DataColumn col in ds.Tables[1].Columns)
                        {
                            pCell = new PdfPCell(new Phrase(col.ColumnName, headFont6));
                            pdfTable.SetWidths(widths);
                            pCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pCell.BackgroundColor = new BaseColor(220, 220, 220);
                            pdfTable.AddCell(pCell);
                            //sarat changes
                            //pCell.NoWrap = true;
                            //pCell.Padding = 0;
                        }

                        pCell = null;
                        for (int rows = 0; rows < ds.Tables[1].Rows.Count; rows++)
                        {
                            for (int column = 0; column < ds.Tables[1].Columns.Count; column++)
                            {
                                pCell =
                                    new PdfPCell(new Phrase(new Chunk(ds.Tables[1].Rows[rows][column].ToString(), font7)));

                                pdfTable.AddCell(pCell);
                                //sarat changes

                                pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pCell.NoWrap = false;
                                pCell.Padding = 0;
                            }
                        }
                        //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table 
                        pdfDoc.Add(pdfTable); // add pdf table to the document   
                    }

                    #endregion

                    #region Tables[2]

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        var issued = new Chunk("Cylinder Change Activity", FontFactory.GetFont("Verdana", 8));
                        var pIssued = new Paragraph();
                        pIssued.Add(issued);
                        pIssued.SpacingAfter = 5f;
                        pdfDoc.Add(pIssued);

                        //Craete instance of the pdf table and set the number of column in that table  
                        var pdfTable = new PdfPTable(ds.Tables[2].Columns.Count);
                        pdfTable.WidthPercentage = 100;
                        widths = new[] { 3f, 10f, 7f, 15f, 10f, 7f, 10f, 8f, 15f, 15f };

                        foreach (DataColumn col in ds.Tables[2].Columns)
                        {
                            pCell = new PdfPCell(new Phrase(col.ColumnName, headFont6));
                            pdfTable.SetWidths(widths);
                            pCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pCell.BackgroundColor = new BaseColor(220, 220, 220);
                            pdfTable.AddCell(pCell);
                            //sarat changes
                            //pCell.NoWrap = true;
                            //pCell.Padding = 0;
                        }

                        pCell = null;
                        for (int rows = 0; rows < ds.Tables[2].Rows.Count; rows++)
                        {
                            for (int column = 0; column < ds.Tables[2].Columns.Count; column++)
                            {
                                pCell =
                                    new PdfPCell(new Phrase(new Chunk(ds.Tables[2].Rows[rows][column].ToString(), font7)));

                                pdfTable.AddCell(pCell);
                                //sarat changes

                                pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pCell.NoWrap = false;
                                pCell.Padding = 0;
                            }
                        }
                        //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table 
                        pdfDoc.Add(pdfTable); // add pdf table to the document   
                    }

                    #endregion

                    #region Tables[3]

                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        var issued = new Chunk("Cylinder Change - Re Issue Activity", FontFactory.GetFont("Verdana", 8));
                        var pIssued = new Paragraph();
                        pIssued.Add(issued);
                        pIssued.SpacingAfter = 5f;
                        pdfDoc.Add(pIssued);

                        //Craete instance of the pdf table and set the number of column in that table  
                        var pdfTable = new PdfPTable(ds.Tables[3].Columns.Count);
                        pdfTable.WidthPercentage = 100;
                        widths = new[] { 3f, 10f, 7f, 15f, 10f, 7f, 10f, 8f, 15f, 15f };

                        foreach (DataColumn col in ds.Tables[3].Columns)
                        {
                            pCell = new PdfPCell(new Phrase(col.ColumnName, headFont6));
                            pdfTable.SetWidths(widths);
                            pCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pCell.BackgroundColor = new BaseColor(220, 220, 220);
                            pdfTable.AddCell(pCell);
                            //sarat changes
                            //pCell.NoWrap = true;
                            //pCell.Padding = 0;
                        }

                        pCell = null;
                        for (int rows = 0; rows < ds.Tables[3].Rows.Count; rows++)
                        {
                            for (int column = 0; column < ds.Tables[3].Columns.Count; column++)
                            {
                                pCell =
                                    new PdfPCell(new Phrase(new Chunk(ds.Tables[3].Rows[rows][column].ToString(), font7)));

                                pdfTable.AddCell(pCell);
                                //sarat changes

                                pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pCell.NoWrap = false;
                                pCell.Padding = 0;
                            }
                        }
                        //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table 
                        pdfDoc.Add(pdfTable); // add pdf table to the document   
                    }

                    #endregion

                    #region Tables[4]

                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        var issued = new Chunk("Lost Key - Re Issue Activity", FontFactory.GetFont("Verdana", 8));
                        var pIssued = new Paragraph();
                        pIssued.Add(issued);
                        pIssued.SpacingAfter = 5f;
                        pdfDoc.Add(pIssued);

                        //Craete instance of the pdf table and set the number of column in that table  
                        var pdfTable = new PdfPTable(ds.Tables[4].Columns.Count);
                        pdfTable.WidthPercentage = 100;
                        widths = new[] { 3f, 10f, 7f, 15f, 10f, 7f, 10f, 8f, 15f, 15f };

                        foreach (DataColumn col in ds.Tables[4].Columns)
                        {
                            pCell = new PdfPCell(new Phrase(col.ColumnName, headFont6));
                            pdfTable.SetWidths(widths);
                            pCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pCell.BackgroundColor = new BaseColor(220, 220, 220);
                            pdfTable.AddCell(pCell);
                            //sarat changes
                            //pCell.NoWrap = true;
                            //pCell.Padding = 0;
                        }

                        pCell = null;
                        for (int rows = 0; rows < ds.Tables[4].Rows.Count; rows++)
                        {
                            for (int column = 0; column < ds.Tables[4].Columns.Count; column++)
                            {
                                pCell =
                                    new PdfPCell(new Phrase(new Chunk(ds.Tables[4].Rows[rows][column].ToString(), font7)));

                                pdfTable.AddCell(pCell);
                                //sarat changes

                                pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pCell.NoWrap = false;
                                pCell.Padding = 0;
                            }
                        }
                        //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table 
                        pdfDoc.Add(pdfTable); // add pdf table to the document   
                    }

                    #endregion
                }

                pdfDoc.Close();
                HttpResponse response = HttpContext.Current.Response;
                response.ContentType = "application/pdf";
                //response.ContentType = "application/octet-stream";
                //String fileName = string.Concat(heading,
                //    DateTime.Now.ToString()
                //        .Replace(":", string.Empty)
                //        .Replace("/", string.Empty)
                //        .Replace(" ", string.Empty), ".pdf");

                String fileName = string.Concat(heading, ".pdf").Replace(' ', '_').Replace("-", "");

                response.AddHeader("content-disposition", "attachment; filename= " + fileName);
                //System.Web.HttpContext.Current.Response.Write(pdfDoc);
                byte[] bytes = ms.ToArray();
                response.OutputStream.Write(bytes, 0, bytes.Length);
                response.OutputStream.Flush();
                response.Flush();
                response.End();
            }
            //HttpContext.Current.ApplicationInstance.CompleteRequest();  
            return true;
        }
    }

    /// <summary>
    ///     http://stackoverflow.com/questions/18996323/add-header-and-footer-for-pdf-using-itextsharp
    /// </summary>
    public class ITextEvents : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        private DateTime PrintTime = DateTime.Now;
        private BaseFont bf;
        private PdfContentByte cb;

        // we will put the final number of pages in a template
        private PdfTemplate footerTemplate;
        private PdfTemplate headerTemplate;

        #region Fields

        #endregion

        #region Properties

        public string Header { get; set; }

        #endregion

        // this is the BaseFont we are going to use for the header / footer

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (IOException ioe)
            {
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            var baseFontNormal = new Font(Font.FontFamily.HELVETICA, 12f, Font.NORMAL, BaseColor.BLACK);

            var baseFontBig = new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD, BaseColor.BLACK);

            //Phrase p1Header = new Phrase("Sample Header Here", baseFontNormal);

            //Create PdfTable object
            //PdfPTable pdfTab = new PdfPTable(3);

            ////We will have to create separate cells to include image logo and 2 separate strings
            ////Row 1
            //PdfPCell pdfCell1 = new PdfPCell();
            //PdfPCell pdfCell2 = new PdfPCell(p1Header);
            //PdfPCell pdfCell3 = new PdfPCell();
            String text = "Page " + writer.PageNumber + " of ";


            //Add paging to header
            //{
            //    cb.BeginText();
            //    cb.SetFontAndSize(bf, 12);
            //    cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
            //    cb.ShowText(text);
            //    cb.EndText();
            //    float len = bf.GetWidthPoint(text, 12);
            //    //Adds "12" in Page 1 of 12
            //    cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45));
            //}
            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 6);
                cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(30));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 6);


                cb.BeginText();
                cb.SetFontAndSize(bf, 8);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT,
                    "Printed On " + PrintTime.ToString(),
                    document.PageSize.GetLeft(40),
                    document.PageSize.GetBottom(30), 0);

                cb.EndText();
                //Footer Image
                //string footerimagepath = HttpContext.Current.Server.MapPath("Images");
                //Image imgfoot = Image.GetInstance(footerimagepath + "/report_footer.jpg");
                //Image imgfoot = Image.GetInstance(footerimagepath + "/NBADLogo.PNG");

                //Header Image
                //iTextSharp.text.Image imghead = iTextSharp.text.Image.GetInstance(clsAppConfig.ReportHeaderImage);


                //imgfoot.SetAbsolutePosition(180, 50);
                //footerTemplate.AddImage(imgfoot);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));
            }
            ////Row 2
            //PdfPCell pdfCell4 = new PdfPCell(new Phrase("Sub Header Description", baseFontNormal));
            ////Row 3


            //PdfPCell pdfCell5 = new PdfPCell(new Phrase("Date:" + PrintTime.ToShortDateString(), baseFontBig));
            //PdfPCell pdfCell6 = new PdfPCell();
            //PdfPCell pdfCell7 = new PdfPCell(new Phrase("TIME:" + string.Format("{0:t}", DateTime.Now), baseFontBig));


            //set the alignment of all three cells and set border to 0
            //pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfCell4.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfCell7.HorizontalAlignment = Element.ALIGN_CENTER;


            //pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;
            //pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            //pdfCell4.VerticalAlignment = Element.ALIGN_TOP;
            //pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            //pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            //pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;


            //pdfCell4.Colspan = 3;


            //pdfCell1.Border = 0;
            //pdfCell2.Border = 0;
            //pdfCell3.Border = 0;
            //pdfCell4.Border = 0;
            //pdfCell5.Border = 0;
            //pdfCell6.Border = 0;
            //pdfCell7.Border = 0;


            ////add all three cells into PdfTable
            //pdfTab.AddCell(pdfCell1);
            //pdfTab.AddCell(pdfCell2);
            //pdfTab.AddCell(pdfCell3);
            //pdfTab.AddCell(pdfCell4);
            //pdfTab.AddCell(pdfCell5);
            //pdfTab.AddCell(pdfCell6);
            //pdfTab.AddCell(pdfCell7);

            //pdfTab.TotalWidth = document.PageSize.Width - 80f;
            //pdfTab.WidthPercentage = 70;
            ////pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;


            ////call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            ////first param is start row. -1 indicates there is no end row and all the rows to be included to write
            ////Third and fourth param is x and y position to start writing
            //pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
            ////set pdfContent value

            ////Move the pointer and draw line to separate header section from rest of page
            //cb.MoveTo(40, document.PageSize.Height - 100);
            //cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
            //cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            //cb.MoveTo(40, document.PageSize.GetBottom(50));
            //cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            //cb.Stroke();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 6);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 6);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();
        }
    }

    /// <summary>
    ///     http://forums.asp.net/t/1591421.aspx?iTextSharp+Add+image+to+page+header
    /// </summary>
    public class itsEventsHandler : PdfPageEventHelper
    {
        private BaseFont helv;
        private PdfTemplate total;

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            //Footer Image
            //string footerimagepath = HttpContext.Current.Server.MapPath("Images");
            //Image imgfoot = Image.GetInstance(footerimagepath + "/report_footer.jpg");
            //Image imgfoot = Image.GetInstance(footerimagepath + "/NBADLogo.PNG");
            //imgfoot.ScalePercent(20);

            //Header Image
            //iTextSharp.text.Image imghead = iTextSharp.text.Image.GetInstance(footerimagepath + "/report_footer.jpg");


            //imgfoot.SetAbsolutePosition(0, 0);
            //imghead.SetAbsolutePosition(0, 0);


            //PdfContentByte cbhead = writer.DirectContent;
            //PdfTemplate tp = cbhead.CreateTemplate(273, 95);
            //tp.AddImage(imghead);


            PdfContentByte cbfoot = writer.DirectContent;
            PdfTemplate tpl = cbfoot.CreateTemplate(700, 100);
            //tpl.SetHorizontalScaling(100);
            //imgfoot.ScaleToFit(document.PageSize.Width, 50);
            //imgfoot.SpacingAfter = 15f;
            //tpl.AddImage(imgfoot);


            //cbhead.AddTemplate(tp, 0, 842 - 95);
            cbfoot.AddTemplate(tpl, 0, 0);


            helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            PdfContentByte cb = writer.DirectContent;
            cbfoot.SaveState();
            //document.SetMargins(35, 35, 100, 82);


            //string text = "Developed by ";
            //float textBase = document.Bottom - 62;
            //float textSize = 9;
            //cbfoot.BeginText();
            //cbfoot.SetFontAndSize(helv, 9);
            //cbfoot.SetTextMatrix(document.Left, textBase);
            //cbfoot.ShowText(text);
            //cbfoot.SetColorFill(BaseColor.BLUE);
            //cbfoot.ShowText("www.nishit.com");
            //cbfoot.EndText();
            //cbfoot.AddTemplate(total, document.Left + textSize, textBase);
            cb.RestoreState();


            //document.NewPage();
            base.OnStartPage(writer, document);
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            total = writer.DirectContent.CreateTemplate(100, 100);
            total.BoundingBox = new Rectangle(-20, -20, 100, 100);


            helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
        }
    }
}