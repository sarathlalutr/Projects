using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace Libraries
{
    public static class ExcelCreater
    {
        public static void exportToExcel(string sheetName, DataTable dt, string dateFrom, string dateTo)
        {
            //using (var ms = new MemoryStream())
            //{
            //    string imgPath = "";
            //    string excelFile = CreateSheet(sheetName, dt, sheetName, imgPath, dateFrom, dateTo);

            //    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            //    response.ContentType = "application/pdf";
            //    //response.ContentType = "application/octet-stream";
            //    //String fileName = string.Concat(heading,
            //    //    DateTime.Now.ToString()
            //    //        .Replace(":", string.Empty)
            //    //        .Replace("/", string.Empty)
            //    //        .Replace(" ", string.Empty), ".pdf");

            //    //String fileName = string.Concat(heading, ".pdf").Replace(' ', '_');

            //    response.AddHeader("content-disposition", "attachment; filename= " + excelFile);
            //    //System.Web.HttpContext.Current.Response.Write(pdfDoc);
            //    var bytes = ms.ToArray();
            //    response.OutputStream.Write(bytes, 0, bytes.Length);
            //    response.OutputStream.Flush();
            //    response.Flush();
            //    response.End();
            //}
        }

        /// <summary>
        ///     http://zeeshanumardotnet.blogspot.in/2010/08/creating-advanced-excel-2007-reports-on.html
        ///     http://zeeshanumardotnet.blogspot.in/2011/06/creating-reports-in-excel-2007-using.html
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="dt"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static bool CreateSheet(string sheetName, DataTable dt, string subHeading)
        {
            using (var ms = new MemoryStream())
            {
                using (var p = new ExcelPackage())
                {
                    List<int> maximumLengthForColumns =
                        Enumerable.Range(0, dt.Columns.Count)
                            .Select(col => dt.AsEnumerable()
                                .Select(row => row[col].ToString().Trim())
                                .Max(
                                    val =>
                                        val.Length < dt.Columns[col].ColumnName.Length
                                            ? dt.Columns[col].ColumnName.Length
                                            : val.Length)).ToList();

                    //Here setting some document properties
                    p.Workbook.Properties.Author = "NBAD";
                    p.Workbook.Properties.Title = sheetName;

                    //Create a sheet
                    p.Workbook.Worksheets.Add(sheetName);
                    ExcelWorksheet ws = p.Workbook.Worksheets[1];
                    ws.Name = sheetName; //Setting Sheet's name
                    ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
                    ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

                    //string imagePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Images\\freedom.jpg");

                    //DataTable dt = CreateDataTable(); //My Function which generates DataTable

                    //Merging cells and create a center heading for out table
                    ws.Cells[4, 1].Value = sheetName;
                    ws.Cells[5, 1].Value = subHeading;

                    ws.Cells[4, 1, 4, dt.Columns.Count].Merge = true;
                    ws.Cells[5, 1, 5, dt.Columns.Count].Merge = true;

                    //ws.Cells[4, 1, 5, dt.Columns.Count].Style.Font.Bold = true;
                    //ws.Cells[4, 1, 5, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[4, 1].Style.Font.Bold = true;
                    ws.Cells[4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[5, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //Merge Excel Columns: Merging cells and create a center heading for our table
                    //ws.Cells[4, 4].Value = sheetName;
                    //ws.Cells[4, 4, 4, dt.Columns.Count].Merge = true;

                    int colIndex = 1;
                    int rowIndex = 7;
                    int maxLen = 0;
                    foreach (DataColumn dc in dt.Columns) //Creating Headings
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Font.Bold = true;
                        cell.Style.Font.Size = 13;
                        ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] + 3;
                        // ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] > dc.ColumnName.Length ? maximumLengthForColumns[maxLen] + 5 : dc.ColumnName.Length + 5;
                        //Setting the background color of header cells to Gray
                        var fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.LightGray;
                        fill.BackgroundColor.SetColor(Color.Gray);


                        //Setting Top/left,right/bottom borders.
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                        //Setting Value in cell
                        cell.Value = dc.ColumnName;

                        colIndex++;
                        maxLen++;
                    }

                    foreach (DataRow dr in dt.Rows) // Adding Data into rows
                    {
                        colIndex = 1;
                        rowIndex++;

                        foreach (DataColumn dc in dt.Columns)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];
                            //Setting Value in cell
                            cell.Value = dr[dc.ColumnName];

                            //Setting borders of cell
                            var border = cell.Style.Border;
                            border.Left.Style =
                                border.Right.Style = ExcelBorderStyle.Thin;
                            colIndex++;

                            var dataBorder = cell.Style.Border;
                            border.Bottom.Style =
                                border.Top.Style =
                                    border.Left.Style =
                                        border.Right.Style = ExcelBorderStyle.None;
                        }
                    }

                    if (sheetName == "Key Inventory Report")
                    {
                        ws.Cells[rowIndex + 2, 9].Value = "Total";
                        ws.Cells[rowIndex + 2, 9].Style.Font.Bold = true;
                        //Setting Background fill color to Gray

                        colIndex = 10;
                        //rowIndex = 8;
                        for (int i = 0; i < 5; i++) //Creating Sum Formula
                        {
                            var cell = ws.Cells[rowIndex + 2, colIndex];
                            cell.Style.Font.Bold = true;

                            //Setting Sum Formula
                            cell.Formula = "Sum(" +
                                           ws.Cells[8, colIndex].Address +
                                           ":" +
                                           ws.Cells[rowIndex, colIndex].Address +
                                           ")";

                            colIndex++;
                        }
                    }

                    string imagepath = string.Concat(HttpContext.Current.Server.MapPath("Images"), "/NBADLogo.png");

                    AddImage(ws, 4, 1, imagepath);

                    //Generate A File

                    p.SaveAs(ms);

                    HttpResponse response = HttpContext.Current.Response;
                    response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //response.ContentType = "application/octet-stream";
                    //String fileName = string.Concat(heading,
                    //    DateTime.Now.ToString()
                    //        .Replace(":", string.Empty)
                    //        .Replace("/", string.Empty)
                    //        .Replace(" ", string.Empty), ".pdf");

                    String fileName = string.Concat(sheetName, ".xlsx").Replace(' ', '_').Replace("-", "");

                    response.AddHeader("content-disposition", "attachment; filename= " + fileName);
                    //System.Web.HttpContext.Current.Response.Write(pdfDoc);
                    byte[] bytes = ms.ToArray();
                    response.OutputStream.Write(bytes, 0, bytes.Length);
                    response.OutputStream.Flush();
                    response.Flush();
                    response.End();

                    //Byte[] bin = p.GetAsByteArray();
                    //string file = filePath + ".xlsx";
                    //File.WriteAllBytes(file, bin);
                    //return file;
                }
            }
            return true;
        }

        //public static bool CreateSheetFromMultiData(string sheetName, DataSet ds, string subHeading, string reportName)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        using (var p = new ExcelPackage())
        //        {
        //            List<int> maximumLengthForColumns0 = null;
        //            List<int> maximumLengthForColumns1 = null;
        //            List<int> maximumLengthForColumns2 = null;
        //            List<int> maximumLengthForColumns3 = null;
        //            List<int> maximumLengthForColumns4 = null;

        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                maximumLengthForColumns0 =
        //                    Enumerable.Range(0, ds.Tables[0].Columns.Count)
        //                        .Select(col => ds.Tables[0].AsEnumerable()
        //                            .Select(row => row[col].ToString().Trim())
        //                            .Max(
        //                                val =>
        //                                    val.Length < ds.Tables[0].Columns[col].ColumnName.Length
        //                                        ? ds.Tables[0].Columns[col].ColumnName.Length
        //                                        : val.Length)).ToList();
        //            }

        //            if (ds.Tables[1].Rows.Count > 0)
        //            {
        //                maximumLengthForColumns1 =
        //                    Enumerable.Range(0, ds.Tables[1].Columns.Count)
        //                        .Select(col => ds.Tables[1].AsEnumerable()
        //                            .Select(row => row[col].ToString().Trim())
        //                            .Max(
        //                                val =>
        //                                    val.Length < ds.Tables[1].Columns[col].ColumnName.Length
        //                                        ? ds.Tables[1].Columns[col].ColumnName.Length
        //                                        : val.Length)).ToList();
        //            }

        //            if (ds.Tables[2].Rows.Count > 0)
        //            {
        //                maximumLengthForColumns2 =
        //                    Enumerable.Range(0, ds.Tables[2].Columns.Count)
        //                        .Select(col => ds.Tables[2].AsEnumerable()
        //                            .Select(row => row[col].ToString().Trim())
        //                            .Max(
        //                                val =>
        //                                    val.Length < ds.Tables[2].Columns[col].ColumnName.Length
        //                                        ? ds.Tables[2].Columns[col].ColumnName.Length
        //                                        : val.Length)).ToList();
        //            }

        //            if (ds.Tables[3].Rows.Count > 0)
        //            {
        //                maximumLengthForColumns3 =
        //                    Enumerable.Range(0, ds.Tables[3].Columns.Count)
        //                        .Select(col => ds.Tables[3].AsEnumerable()
        //                            .Select(row => row[col].ToString().Trim())
        //                            .Max(
        //                                val =>
        //                                    val.Length < ds.Tables[3].Columns[col].ColumnName.Length
        //                                        ? ds.Tables[3].Columns[col].ColumnName.Length
        //                                        : val.Length)).ToList();
        //            }

        //            if (ds.Tables.Count == 5)
        //            {
        //                if (ds.Tables[4].Rows.Count > 0)
        //                {
        //                    maximumLengthForColumns4 =
        //                        Enumerable.Range(0, ds.Tables[4].Columns.Count)
        //                            .Select(col => ds.Tables[4].AsEnumerable()
        //                                .Select(row => row[col].ToString().Trim())
        //                                .Max(
        //                                    val =>
        //                                        val.Length < ds.Tables[4].Columns[col].ColumnName.Length
        //                                            ? ds.Tables[4].Columns[col].ColumnName.Length
        //                                            : val.Length)).ToList();
        //                }
        //            }

        //            //Here setting some document properties
        //            p.Workbook.Properties.Author = "Etihad";
        //            p.Workbook.Properties.Title = sheetName;

        //            //Create a sheet
        //            p.Workbook.Worksheets.Add(sheetName);
        //            ExcelWorksheet ws = p.Workbook.Worksheets[1];
        //            ws.Name = sheetName; //Setting Sheet's name
        //            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
        //            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

        //            //string imagePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Images\\freedom.jpg");

        //            //DataTable dt = CreateDataTable(); //My Function which generates DataTable

        //            //Merging cells and create a center heading for out table
        //            ws.Cells[4, 1].Value = sheetName;
        //            ws.Cells[5, 1].Value = subHeading;

        //            ws.Cells[4, 1, 4, ds.Tables[0].Columns.Count].Merge = true;
        //            ws.Cells[5, 1, 5, ds.Tables[0].Columns.Count].Merge = true;

        //            //ws.Cells[4, 1, 5, dt.Columns.Count].Style.Font.Bold = true;
        //            //ws.Cells[4, 1, 5, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            ws.Cells[4, 1].Style.Font.Bold = true;
        //            ws.Cells[4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            ws.Cells[5, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        //            //Merge Excel Columns: Merging cells and create a center heading for our table
        //            //ws.Cells[4, 4].Value = sheetName;
        //            //ws.Cells[4, 4, 4, dt.Columns.Count].Merge = true;

        //            int colIndex = 1;
        //            int rowIndex = 7;
        //            int maxLen = 0;

        //            if (reportName == "All Key")
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    #region Tables[0]

        //                    ws.Cells[rowIndex, 1].Value = "Total Key Issued";
        //                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
        //                    ws.Cells[rowIndex, 1, rowIndex, ds.Tables[0].Columns.Count].Merge = true;
        //                    rowIndex++;
        //                    foreach (DataColumn dc in ds.Tables[0].Columns) //Creating Headings
        //                    {
        //                        var cell = ws.Cells[rowIndex, colIndex];
        //                        cell.Style.Font.Bold = true;
        //                        cell.Style.Font.Size = 13;
        //                        ws.Column(colIndex).Width = maximumLengthForColumns0[maxLen] + 3;
        //                        // ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] > dc.ColumnName.Length ? maximumLengthForColumns[maxLen] + 5 : dc.ColumnName.Length + 5;
        //                        //Setting the background color of header cells to Gray
        //                        var fill = cell.Style.Fill;
        //                        fill.PatternType = ExcelFillStyle.LightGray;
        //                        fill.BackgroundColor.SetColor(Color.Gray);


        //                        //Setting Top/left,right/bottom borders.
        //                        var border = cell.Style.Border;
        //                        border.Bottom.Style =
        //                            border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        //                        //Setting Value in cell
        //                        cell.Value = dc.ColumnName;

        //                        colIndex++;
        //                        maxLen++;
        //                    }

        //                    foreach (DataRow dr in ds.Tables[0].Rows) // Adding Data into rows
        //                    {
        //                        colIndex = 1;
        //                        rowIndex++;

        //                        foreach (DataColumn dc in ds.Tables[0].Columns)
        //                        {
        //                            var cell = ws.Cells[rowIndex, colIndex];
        //                            //Setting Value in cell
        //                            cell.Value = dr[dc.ColumnName];

        //                            //Setting borders of cell
        //                            var border = cell.Style.Border;
        //                            border.Left.Style =
        //                                border.Right.Style = ExcelBorderStyle.Thin;
        //                            colIndex++;

        //                            var dataBorder = cell.Style.Border;
        //                            border.Bottom.Style =
        //                                border.Top.Style =
        //                                    border.Left.Style =
        //                                        border.Right.Style = ExcelBorderStyle.None;
        //                        }
        //                    }

        //                    #endregion

        //                    rowIndex++;
        //                }

        //                if (ds.Tables[1].Rows.Count > 0)
        //                {
        //                    #region Tables[1]

        //                    maxLen = 0;
        //                    colIndex = 1;
        //                    rowIndex++;
        //                    ws.Cells[rowIndex, 1].Value = "Total Key Returned";
        //                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
        //                    ws.Cells[rowIndex, 1, rowIndex, ds.Tables[1].Columns.Count].Merge = true;
        //                    rowIndex++;
        //                    foreach (DataColumn dc in ds.Tables[1].Columns) //Creating Headings
        //                    {
        //                        var cell = ws.Cells[rowIndex, colIndex];
        //                        cell.Style.Font.Bold = true;
        //                        cell.Style.Font.Size = 13;
        //                        ws.Column(colIndex).Width = maximumLengthForColumns1[maxLen] + 3;
        //                        // ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] > dc.ColumnName.Length ? maximumLengthForColumns[maxLen] + 5 : dc.ColumnName.Length + 5;
        //                        //Setting the background color of header cells to Gray
        //                        var fill = cell.Style.Fill;
        //                        fill.PatternType = ExcelFillStyle.LightGray;
        //                        fill.BackgroundColor.SetColor(Color.Gray);


        //                        //Setting Top/left,right/bottom borders.
        //                        var border = cell.Style.Border;
        //                        border.Bottom.Style =
        //                            border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        //                        //Setting Value in cell
        //                        cell.Value = dc.ColumnName;

        //                        colIndex++;
        //                        maxLen++;
        //                    }

        //                    foreach (DataRow dr in ds.Tables[1].Rows) // Adding Data into rows
        //                    {
        //                        colIndex = 1;
        //                        rowIndex++;

        //                        foreach (DataColumn dc in ds.Tables[1].Columns)
        //                        {
        //                            var cell = ws.Cells[rowIndex, colIndex];
        //                            //Setting Value in cell
        //                            cell.Value = dr[dc.ColumnName];

        //                            //Setting borders of cell
        //                            var border = cell.Style.Border;
        //                            border.Left.Style =
        //                                border.Right.Style = ExcelBorderStyle.Thin;
        //                            colIndex++;

        //                            var dataBorder = cell.Style.Border;
        //                            border.Bottom.Style =
        //                                border.Top.Style =
        //                                    border.Left.Style =
        //                                        border.Right.Style = ExcelBorderStyle.None;
        //                        }
        //                    }

        //                    #endregion

        //                    rowIndex++;
        //                }

        //                if (ds.Tables[2].Rows.Count > 0)
        //                {
        //                    #region Tables[2]

        //                    maxLen = 0;
        //                    colIndex = 1;
        //                    rowIndex++;
        //                    ws.Cells[rowIndex, 1].Value = "Total Key Lost";
        //                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
        //                    ws.Cells[rowIndex, 1, rowIndex, ds.Tables[2].Columns.Count].Merge = true;
        //                    rowIndex++;
        //                    foreach (DataColumn dc in ds.Tables[2].Columns) //Creating Headings
        //                    {
        //                        var cell = ws.Cells[rowIndex, colIndex];
        //                        cell.Style.Font.Bold = true;
        //                        cell.Style.Font.Size = 13;
        //                        ws.Column(colIndex).Width = maximumLengthForColumns2[maxLen] + 3;
        //                        // ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] > dc.ColumnName.Length ? maximumLengthForColumns[maxLen] + 5 : dc.ColumnName.Length + 5;
        //                        //Setting the background color of header cells to Gray
        //                        var fill = cell.Style.Fill;
        //                        fill.PatternType = ExcelFillStyle.LightGray;
        //                        fill.BackgroundColor.SetColor(Color.Gray);


        //                        //Setting Top/left,right/bottom borders.
        //                        var border = cell.Style.Border;
        //                        border.Bottom.Style =
        //                            border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        //                        //Setting Value in cell
        //                        cell.Value = dc.ColumnName;

        //                        colIndex++;
        //                        maxLen++;
        //                    }

        //                    foreach (DataRow dr in ds.Tables[2].Rows) // Adding Data into rows
        //                    {
        //                        colIndex = 1;
        //                        rowIndex++;

        //                        foreach (DataColumn dc in ds.Tables[2].Columns)
        //                        {
        //                            var cell = ws.Cells[rowIndex, colIndex];
        //                            //Setting Value in cell
        //                            cell.Value = dr[dc.ColumnName];

        //                            //Setting borders of cell
        //                            var border = cell.Style.Border;
        //                            border.Left.Style =
        //                                border.Right.Style = ExcelBorderStyle.Thin;
        //                            colIndex++;

        //                            var dataBorder = cell.Style.Border;
        //                            border.Bottom.Style =
        //                                border.Top.Style =
        //                                    border.Left.Style =
        //                                        border.Right.Style = ExcelBorderStyle.None;
        //                        }
        //                    }

        //                    #endregion

        //                    rowIndex++;
        //                }

        //                if (ds.Tables[3].Rows.Count > 0)
        //                {
        //                    #region Tables[3]

        //                    maxLen = 0;
        //                    colIndex = 1;
        //                    rowIndex++;
        //                    ws.Cells[rowIndex, 1].Value = "Total Cylinder Change";
        //                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
        //                    ws.Cells[rowIndex, 1, rowIndex, ds.Tables[3].Columns.Count].Merge = true;
        //                    rowIndex++;
        //                    foreach (DataColumn dc in ds.Tables[3].Columns) //Creating Headings
        //                    {
        //                        var cell = ws.Cells[rowIndex, colIndex];
        //                        cell.Style.Font.Bold = true;
        //                        cell.Style.Font.Size = 13;
        //                        ws.Column(colIndex).Width = maximumLengthForColumns3[maxLen] + 3;
        //                        // ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] > dc.ColumnName.Length ? maximumLengthForColumns[maxLen] + 5 : dc.ColumnName.Length + 5;
        //                        //Setting the background color of header cells to Gray
        //                        var fill = cell.Style.Fill;
        //                        fill.PatternType = ExcelFillStyle.LightGray;
        //                        fill.BackgroundColor.SetColor(Color.Gray);


        //                        //Setting Top/left,right/bottom borders.
        //                        var border = cell.Style.Border;
        //                        border.Bottom.Style =
        //                            border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        //                        //Setting Value in cell
        //                        cell.Value = dc.ColumnName;

        //                        colIndex++;
        //                        maxLen++;
        //                    }

        //                    foreach (DataRow dr in ds.Tables[3].Rows) // Adding Data into rows
        //                    {
        //                        colIndex = 1;
        //                        rowIndex++;

        //                        foreach (DataColumn dc in ds.Tables[3].Columns)
        //                        {
        //                            var cell = ws.Cells[rowIndex, colIndex];
        //                            //Setting Value in cell
        //                            cell.Value = dr[dc.ColumnName];

        //                            //Setting borders of cell
        //                            var border = cell.Style.Border;
        //                            border.Left.Style =
        //                                border.Right.Style = ExcelBorderStyle.Thin;
        //                            colIndex++;

        //                            var dataBorder = cell.Style.Border;
        //                            border.Bottom.Style =
        //                                border.Top.Style =
        //                                    border.Left.Style =
        //                                        border.Right.Style = ExcelBorderStyle.None;
        //                        }
        //                    }

        //                    #endregion
        //                }
        //            }

        //            else if (reportName == "User Activity")
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    #region Tables[0]

        //                    ws.Cells[rowIndex, 1].Value = "Check-In Activity";
        //                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
        //                    ws.Cells[rowIndex, 1, rowIndex, ds.Tables[0].Columns.Count].Merge = true;
        //                    rowIndex++;
        //                    foreach (DataColumn dc in ds.Tables[0].Columns) //Creating Headings
        //                    {
        //                        var cell = ws.Cells[rowIndex, colIndex];
        //                        cell.Style.Font.Bold = true;
        //                        cell.Style.Font.Size = 13;
        //                        ws.Column(colIndex).Width = maximumLengthForColumns0[maxLen] + 3;
        //                        // ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] > dc.ColumnName.Length ? maximumLengthForColumns[maxLen] + 5 : dc.ColumnName.Length + 5;
        //                        //Setting the background color of header cells to Gray
        //                        var fill = cell.Style.Fill;
        //                        fill.PatternType = ExcelFillStyle.LightGray;
        //                        fill.BackgroundColor.SetColor(Color.Gray);


        //                        //Setting Top/left,right/bottom borders.
        //                        var border = cell.Style.Border;
        //                        border.Bottom.Style =
        //                            border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        //                        //Setting Value in cell
        //                        cell.Value = dc.ColumnName;

        //                        colIndex++;
        //                        maxLen++;
        //                    }

        //                    foreach (DataRow dr in ds.Tables[0].Rows) // Adding Data into rows
        //                    {
        //                        colIndex = 1;
        //                        rowIndex++;

        //                        foreach (DataColumn dc in ds.Tables[0].Columns)
        //                        {
        //                            var cell = ws.Cells[rowIndex, colIndex];
        //                            //Setting Value in cell
        //                            cell.Value = dr[dc.ColumnName];

        //                            //Setting borders of cell
        //                            var border = cell.Style.Border;
        //                            border.Left.Style =
        //                                border.Right.Style = ExcelBorderStyle.Thin;
        //                            colIndex++;

        //                            var dataBorder = cell.Style.Border;
        //                            border.Bottom.Style =
        //                                border.Top.Style =
        //                                    border.Left.Style =
        //                                        border.Right.Style = ExcelBorderStyle.None;
        //                        }
        //                    }

        //                    #endregion

        //                    rowIndex++;
        //                }

        //                if (ds.Tables[1].Rows.Count > 0)
        //                {
        //                    #region Tables[1]

        //                    maxLen = 0;
        //                    colIndex = 1;
        //                    rowIndex++;
        //                    ws.Cells[rowIndex, 1].Value = "Check-Out Activity";
        //                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
        //                    ws.Cells[rowIndex, 1, rowIndex, ds.Tables[1].Columns.Count].Merge = true;
        //                    rowIndex++;
        //                    foreach (DataColumn dc in ds.Tables[1].Columns) //Creating Headings
        //                    {
        //                        var cell = ws.Cells[rowIndex, colIndex];
        //                        cell.Style.Font.Bold = true;
        //                        cell.Style.Font.Size = 13;
        //                        ws.Column(colIndex).Width = maximumLengthForColumns1[maxLen] + 3;
        //                        // ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] > dc.ColumnName.Length ? maximumLengthForColumns[maxLen] + 5 : dc.ColumnName.Length + 5;
        //                        //Setting the background color of header cells to Gray
        //                        var fill = cell.Style.Fill;
        //                        fill.PatternType = ExcelFillStyle.LightGray;
        //                        fill.BackgroundColor.SetColor(Color.Gray);


        //                        //Setting Top/left,right/bottom borders.
        //                        var border = cell.Style.Border;
        //                        border.Bottom.Style =
        //                            border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        //                        //Setting Value in cell
        //                        cell.Value = dc.ColumnName;

        //                        colIndex++;
        //                        maxLen++;
        //                    }

        //                    foreach (DataRow dr in ds.Tables[1].Rows) // Adding Data into rows
        //                    {
        //                        colIndex = 1;
        //                        rowIndex++;

        //                        foreach (DataColumn dc in ds.Tables[1].Columns)
        //                        {
        //                            var cell = ws.Cells[rowIndex, colIndex];
        //                            //Setting Value in cell
        //                            cell.Value = dr[dc.ColumnName];

        //                            //Setting borders of cell
        //                            var border = cell.Style.Border;
        //                            border.Left.Style =
        //                                border.Right.Style = ExcelBorderStyle.Thin;
        //                            colIndex++;

        //                            var dataBorder = cell.Style.Border;
        //                            border.Bottom.Style =
        //                                border.Top.Style =
        //                                    border.Left.Style =
        //                                        border.Right.Style = ExcelBorderStyle.None;
        //                        }
        //                    }

        //                    #endregion

        //                    rowIndex++;
        //                }

        //                if (ds.Tables[2].Rows.Count > 0)
        //                {
        //                    #region Tables[2]

        //                    maxLen = 0;
        //                    colIndex = 1;
        //                    rowIndex++;
        //                    ws.Cells[rowIndex, 1].Value = "Cylinder Change Activity";
        //                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
        //                    ws.Cells[rowIndex, 1, rowIndex, ds.Tables[2].Columns.Count].Merge = true;
        //                    rowIndex++;
        //                    foreach (DataColumn dc in ds.Tables[2].Columns) //Creating Headings
        //                    {
        //                        var cell = ws.Cells[rowIndex, colIndex];
        //                        cell.Style.Font.Bold = true;
        //                        cell.Style.Font.Size = 13;
        //                        ws.Column(colIndex).Width = maximumLengthForColumns2[maxLen] + 3;
        //                        // ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] > dc.ColumnName.Length ? maximumLengthForColumns[maxLen] + 5 : dc.ColumnName.Length + 5;
        //                        //Setting the background color of header cells to Gray
        //                        var fill = cell.Style.Fill;
        //                        fill.PatternType = ExcelFillStyle.LightGray;
        //                        fill.BackgroundColor.SetColor(Color.Gray);


        //                        //Setting Top/left,right/bottom borders.
        //                        var border = cell.Style.Border;
        //                        border.Bottom.Style =
        //                            border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        //                        //Setting Value in cell
        //                        cell.Value = dc.ColumnName;

        //                        colIndex++;
        //                        maxLen++;
        //                    }

        //                    foreach (DataRow dr in ds.Tables[2].Rows) // Adding Data into rows
        //                    {
        //                        colIndex = 1;
        //                        rowIndex++;

        //                        foreach (DataColumn dc in ds.Tables[2].Columns)
        //                        {
        //                            var cell = ws.Cells[rowIndex, colIndex];
        //                            //Setting Value in cell
        //                            cell.Value = dr[dc.ColumnName];

        //                            //Setting borders of cell
        //                            var border = cell.Style.Border;
        //                            border.Left.Style =
        //                                border.Right.Style = ExcelBorderStyle.Thin;
        //                            colIndex++;

        //                            var dataBorder = cell.Style.Border;
        //                            border.Bottom.Style =
        //                                border.Top.Style =
        //                                    border.Left.Style =
        //                                        border.Right.Style = ExcelBorderStyle.None;
        //                        }
        //                    }

        //                    #endregion

        //                    rowIndex++;
        //                }

        //                if (ds.Tables[3].Rows.Count > 0)
        //                {
        //                    #region Tables[3]

        //                    maxLen = 0;
        //                    colIndex = 1;
        //                    rowIndex++;
        //                    ws.Cells[rowIndex, 1].Value = "Cylinder Change - Re Issue Activity";
        //                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
        //                    ws.Cells[rowIndex, 1, rowIndex, ds.Tables[3].Columns.Count].Merge = true;
        //                    rowIndex++;
        //                    foreach (DataColumn dc in ds.Tables[3].Columns) //Creating Headings
        //                    {
        //                        var cell = ws.Cells[rowIndex, colIndex];
        //                        cell.Style.Font.Bold = true;
        //                        cell.Style.Font.Size = 13;
        //                        ws.Column(colIndex).Width = maximumLengthForColumns3[maxLen] + 3;
        //                        // ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] > dc.ColumnName.Length ? maximumLengthForColumns[maxLen] + 5 : dc.ColumnName.Length + 5;
        //                        //Setting the background color of header cells to Gray
        //                        var fill = cell.Style.Fill;
        //                        fill.PatternType = ExcelFillStyle.LightGray;
        //                        fill.BackgroundColor.SetColor(Color.Gray);


        //                        //Setting Top/left,right/bottom borders.
        //                        var border = cell.Style.Border;
        //                        border.Bottom.Style =
        //                            border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        //                        //Setting Value in cell
        //                        cell.Value = dc.ColumnName;

        //                        colIndex++;
        //                        maxLen++;
        //                    }

        //                    foreach (DataRow dr in ds.Tables[3].Rows) // Adding Data into rows
        //                    {
        //                        colIndex = 1;
        //                        rowIndex++;

        //                        foreach (DataColumn dc in ds.Tables[3].Columns)
        //                        {
        //                            var cell = ws.Cells[rowIndex, colIndex];
        //                            //Setting Value in cell
        //                            cell.Value = dr[dc.ColumnName];

        //                            //Setting borders of cell
        //                            var border = cell.Style.Border;
        //                            border.Left.Style =
        //                                border.Right.Style = ExcelBorderStyle.Thin;
        //                            colIndex++;

        //                            var dataBorder = cell.Style.Border;
        //                            border.Bottom.Style =
        //                                border.Top.Style =
        //                                    border.Left.Style =
        //                                        border.Right.Style = ExcelBorderStyle.None;
        //                        }
        //                    }

        //                    #endregion

        //                    rowIndex++;
        //                }

        //                if (ds.Tables[4].Rows.Count > 0)
        //                {
        //                    #region Tables[4]

        //                    maxLen = 0;
        //                    colIndex = 1;
        //                    rowIndex++;
        //                    ws.Cells[rowIndex, 1].Value = "Lost Key - Re Issue Activity";
        //                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
        //                    ws.Cells[rowIndex, 1, rowIndex, ds.Tables[4].Columns.Count].Merge = true;
        //                    rowIndex++;
        //                    foreach (DataColumn dc in ds.Tables[4].Columns) //Creating Headings
        //                    {
        //                        var cell = ws.Cells[rowIndex, colIndex];
        //                        cell.Style.Font.Bold = true;
        //                        cell.Style.Font.Size = 13;
        //                        ws.Column(colIndex).Width = maximumLengthForColumns4[maxLen] + 3;
        //                        // ws.Column(colIndex).Width = maximumLengthForColumns[maxLen] > dc.ColumnName.Length ? maximumLengthForColumns[maxLen] + 5 : dc.ColumnName.Length + 5;
        //                        //Setting the background color of header cells to Gray
        //                        var fill = cell.Style.Fill;
        //                        fill.PatternType = ExcelFillStyle.LightGray;
        //                        fill.BackgroundColor.SetColor(Color.Gray);


        //                        //Setting Top/left,right/bottom borders.
        //                        var border = cell.Style.Border;
        //                        border.Bottom.Style =
        //                            border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        //                        //Setting Value in cell
        //                        cell.Value = dc.ColumnName;

        //                        colIndex++;
        //                        maxLen++;
        //                    }

        //                    foreach (DataRow dr in ds.Tables[4].Rows) // Adding Data into rows
        //                    {
        //                        colIndex = 1;
        //                        rowIndex++;

        //                        foreach (DataColumn dc in ds.Tables[4].Columns)
        //                        {
        //                            var cell = ws.Cells[rowIndex, colIndex];
        //                            //Setting Value in cell
        //                            cell.Value = dr[dc.ColumnName];

        //                            //Setting borders of cell
        //                            var border = cell.Style.Border;
        //                            border.Left.Style =
        //                                border.Right.Style = ExcelBorderStyle.Thin;
        //                            colIndex++;

        //                            var dataBorder = cell.Style.Border;
        //                            border.Bottom.Style =
        //                                border.Top.Style =
        //                                    border.Left.Style =
        //                                        border.Right.Style = ExcelBorderStyle.None;
        //                        }
        //                    }

        //                    #endregion
        //                }
        //            }

        //            string imagepath = string.Concat(HttpContext.Current.Server.MapPath("images"), "/report_logo.jpg");

        //            AddImage(ws, 0, 1, imagepath);

        //            //Generate A File

        //            p.SaveAs(ms);

        //            HttpResponse response = HttpContext.Current.Response;
        //            response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            //response.ContentType = "application/octet-stream";
        //            //String fileName = string.Concat(heading,
        //            //    DateTime.Now.ToString()
        //            //        .Replace(":", string.Empty)
        //            //        .Replace("/", string.Empty)
        //            //        .Replace(" ", string.Empty), ".pdf");

        //            String fileName = string.Concat(sheetName, ".xlsx").Replace(' ', '_').Replace("-", "");

        //            response.AddHeader("content-disposition", "attachment; filename= " + fileName);
        //            //System.Web.HttpContext.Current.Response.Write(pdfDoc);
        //            byte[] bytes = ms.ToArray();
        //            response.OutputStream.Write(bytes, 0, bytes.Length);
        //            response.OutputStream.Flush();
        //            response.Flush();
        //            response.End();

        //            //Byte[] bin = p.GetAsByteArray();
        //            //string file = filePath + ".xlsx";
        //            //File.WriteAllBytes(file, bin);
        //            //return file;
        //        }
        //    }
        //    return true;
        //}

        private static void AddImage(ExcelWorksheet ws, int columnIndex, int rowIndex, string filePath)
        {
            //How to Add a Image using EP Plus
            var image = new Bitmap(filePath);
            ExcelPicture picture = null;
            if (image != null)
            {
                picture = ws.Drawings.AddPicture("pic" + rowIndex + columnIndex, image);
                picture.From.Column = columnIndex;
                picture.From.Row = rowIndex;
                //picture.From.ColumnOff = Pixel2MTU(2); //Two pixel space for better alignment
                //picture.From.RowOff = Pixel2MTU(2);//Two pixel space for better alignment
                //picture.SetSize(100, 100);
            }
        }
    }
}