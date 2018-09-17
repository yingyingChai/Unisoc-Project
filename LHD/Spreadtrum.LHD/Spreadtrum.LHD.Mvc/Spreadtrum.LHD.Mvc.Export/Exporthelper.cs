namespace Spreadtrum.LHD.Mvc.Export
{
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Xml;
    using NPOI.HSSF.Util;
    using NPOI.HSSF.UserModel;
    using System.Data;
    using System.Text;

    public class Exporthelper
    {
        public static string GetExport<T>(string sheetName, IList<T> TList, string StoragePath, string ConfigurationFile, string xlsName, int startRow = 0, int startCol = 0)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet) workbook.CreateSheet(sheetName);
            XmlDocument document1 = new XmlDocument();
            document1.Load(HttpContext.Current.Server.MapPath(ConfigurationFile));
            XmlNode node1 = document1.SelectSingleNode("table");
            XmlNodeList childNodes = node1.ChildNodes;
            int rownum = startRow;
            string text1 = node1.Attributes["titleCss"].Value;
            string text2 = node1.Attributes["rowCss"].Value;
            XSSFRow row = (XSSFRow) sheet.CreateRow(rownum);
            row.HeightInPoints = 25f;
            int[] numArray = new int[childNodes.Count];
            for (int i = 0; i < childNodes.Count; i++)
            {
                numArray[i] = 0;
                string str2 = ((XmlElement) childNodes[i]).GetAttribute("exportTo").ToString();
                if (numArray[i] < (str2.Length + 10))
                {
                    numArray[i] = str2.Length + 10;
                }
                sheet.SetColumnWidth(i, numArray[i] * 0x100);
                row.CreateCell(i + startCol).SetCellValue(str2);
            }
            foreach (object obj2 in TList)
            {
                rownum++;
                XSSFRow row2 = (XSSFRow) sheet.CreateRow(rownum);
                row2.HeightInPoints = 20f;
                for (int j = 0; j < childNodes.Count; j++)
                {
                    ICell cell1;
                    XmlElement element1 = (XmlElement) childNodes[j];
                    string name = element1.GetAttribute("name").ToString();
                    object obj3 = obj2.GetType().GetProperty(name).GetValue(obj2, null);
                    string str4 = (obj3 == null) ? "" : obj3.ToString();
                    if (numArray[j] < (str4.Length + 4))
                    {
                        numArray[j] = str4.Length + 4;
                    }
                    string attribute = element1.GetAttribute("CellType");
                    if (!(attribute == "strCell"))
                    {
                        if (attribute == "dateCell")
                        {
                            goto Label_0243;
                        }
                        if (attribute == "intCell")
                        {
                            goto Label_0271;
                        }
                        if (attribute == "coinCell")
                        {
                            goto Label_028B;
                        }
                        if (attribute == "pctCell")
                        {
                            goto Label_02A0;
                        }
                        if (attribute == "boolCell")
                        {
                            goto Label_02DD;
                        }
                    }
                    else
                    {
                        row2.CreateCell(j + startCol).SetCellValue(str4);
                    }
                    continue;
                Label_0243:
                    Convert.ToDateTime(str4).ToString("yyyy-MM-dd HH:mm:ss");
                    row2.CreateCell(j + startCol).SetCellValue(str4);
                    continue;
                Label_0271:
                    row2.CreateCell(j + startCol).SetCellValue(double.Parse(str4));
                    continue;
                Label_028B:
                    row2.CreateCell(j + startCol).SetCellValue(str4);
                    continue;
                Label_02A0:
                    cell1 = row2.CreateCell(j + startCol);
                    cell1.SetCellValue((double) (double.Parse(str4) / 100.0));
                    ICellStyle style = workbook.CreateCellStyle();
                    style.DataFormat = 10;
                    cell1.CellStyle = style;
                    continue;
                Label_02DD:
                    row2.CreateCell(j + startCol).SetCellValue(Convert.ToBoolean(str4) ? "Y" : "");
                }
            }
            string path = StoragePath + xlsName + ".xlsx";
            FileStream stream = new FileStream(HttpContext.Current.Server.MapPath(path), FileMode.Create);
            workbook.Write(stream);
            return path;
        }
        public static void ExportToExecel(HttpResponse Response, DataTable dtSource, string strFileName)
        {
            Response.ContentType = "application/vnd.ms-excel;charset=UTF-8";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", HttpUtility.UrlEncode(strFileName)));
            Response.Charset = "UTF-8";
            Response.Clear();
            MemoryStream ms = DataTableToExcel(dtSource, "");
            Response.BinaryWrite(ms.GetBuffer());
            Response.End();
        }
        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        private static MemoryStream DataTableToExcel(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            #region 右击文件 属性信息
            //{
            //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            //    dsi.Company = "NPOI";
            //    workbook.DocumentSummaryInformation = dsi;

            //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //    si.Author = "文件作者信息"; //填加xls文件作者信息
            //    si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
            //    si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
            //    si.Comments = "作者信息"; //填加xls文件作者信息
            //    si.Title = "标题信息"; //填加xls文件标题信息
            //    si.Subject = "主题信息";//填加文件主题信息
            //    si.CreateDateTime = System.DateTime.Now;
            //    workbook.SummaryInformation = si;
            //}
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;//设置低J列的宽度
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();//超过多少行以后新增一个sheet表
                    }

                    #region 表头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;//行高
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //  headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        // sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                        //headerRow.Dispose();
                    }
                    #endregion


                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        int colwidth = 0;
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);//设置列头的文本
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;//设置列头的样式
                            colwidth = (arrColWidth[column.Ordinal] + 1) * 256;
                            if (colwidth > 10000) colwidth = 10000;
                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, colwidth);
                        }
                        // headerRow.Dispose();
                    }
                    #endregion
                    rowIndex = 1;
                }
                #endregion


                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();//单元格的内容

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            System.DateTime dateV;
                            System.DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }
    }
}

