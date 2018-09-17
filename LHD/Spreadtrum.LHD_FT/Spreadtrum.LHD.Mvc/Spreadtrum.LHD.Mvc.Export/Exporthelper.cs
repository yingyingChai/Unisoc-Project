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
    }
}

