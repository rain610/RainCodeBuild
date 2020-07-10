using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Shared
{
    public class NpoiHelper<T>
    {
        public class NpoiMemoryStream : MemoryStream
        {
            public bool AllowClose { get; set; }

            public NpoiMemoryStream()
            {
                AllowClose = true;
            }

            public override void Close()
            {
                if (AllowClose)
                {
                    base.Close();
                }
                
            }
        }

        private string headerText;
        private List<string> headerList { get; set; } = new List<string>();
        private List<string> fieldList { get; set; } = new List<string>();
        private List<T> dataList { get; set; }

        public NpoiHelper(string text, List<string> headers, List<string> fields, List<T> data)
        {
            if (headers?.Count <= 0)
            {
                throw new Exception("表头列不能为null或为空");
            }
            if (fields?.Count <= 0)
            {
                throw new Exception("导出字段的集合不能为null或为空");
            }
            this.headerText = text;
            this.headerList = headers;
            this.dataList = data;
            this.fieldList = fields;
        }

        public MemoryStream Export()
        {
            IWorkbook workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet();
            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            //取得列宽
            int[] arrColWidth = new int[headerList.Count];
            for (var i = 0; i < headerList.Count; i++)
            {
                arrColWidth[i] = Encoding.GetEncoding(936).GetBytes(headerList[i].ToString()).Length;
            }
            int rowIndex = 0;
            var filedDic = new Dictionary<string, object>();
            var fieldIndexDic = new Dictionary<string, int>();
            var j = 0;
            foreach (var field in fieldList)
            {
                filedDic[field] = new object();
                fieldIndexDic[field] = j;
                j++;
            }
            if (rowIndex == 65535 || rowIndex == 0)
            {
                if (rowIndex != 0)
                {
                    sheet = workbook.CreateSheet();
                }
                if (string.IsNullOrWhiteSpace(headerText) == false)
                {
                    rowIndex = CreateHeaderText(workbook, sheet, rowIndex);
                }
                rowIndex = CreateHeader(workbook, sheet, rowIndex, arrColWidth);
            }
            PropertyInfo[] properties = null;
            //var data = dataList.Take(50);
            foreach (var item in dataList)
            {
                if (properties == null)
                {
                    Type t = item.GetType();
                    properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                }
                IRow dataRow = sheet.CreateRow(rowIndex);
                if (properties.Length <= 0) { return null; }
                var contentCellStyle = SetContentCellStyle(workbook);
                foreach (var pro in properties)
                {
                    if (filedDic.ContainsKey(pro.Name))
                    {
                        var index = fieldIndexDic[pro.Name];
                        filedDic[pro.Name] = pro.GetValue(item);
                        var cell = dataRow.CreateCell(index);
                        SetCellValue(cell, pro, filedDic[pro.Name], workbook);
                        cell.CellStyle = contentCellStyle;
                    }
                }
                rowIndex++;
            }
            AutoColumnWidth(sheet, headerList.Count);


            ////插入图片
            //XSSFClientAnchor anchor2 = new XSSFClientAnchor(0, 0, 0, 0, 0, 5, 6, 10);
            //byte[] bytes = System.IO.File.ReadAllBytes(@"C:\Users\Administrator\Desktop\image\mqxs.png");
            //int picID = workbook.AddPicture(bytes, PictureType.PNG);
            //IPicture pic = patriarch.CreatePicture(anchor2, picID);
            //pic.Resize();

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    workbook.Write(ms);
            //    ms.Flush();
            //    //ms.Position = 0;
            //    return ms;
            //}
            var ms = new NpoiMemoryStream();
            ms.AllowClose = false;
            workbook.Write(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            ms.AllowClose = true;
            return ms;
        }

        private int CreateHeaderText(IWorkbook workbook, ISheet sheet, int rowIndex)
        {
            IRow headerRow = sheet.CreateRow(rowIndex);
            headerRow.HeightInPoints = 25;
            headerRow.CreateCell(0).SetCellValue(headerText);
            CellRangeAddress region = new CellRangeAddress(0, 0, 0, headerList.Count - 1);
            sheet.AddMergedRegion(region);
            ICellStyle headStyle = workbook.CreateCellStyle();
            headStyle.Alignment = HorizontalAlignment.Center;
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 20;
            font.Boldweight = 700;
            font.FontName = "微软雅黑";
            headStyle.SetFont(font);
            headerRow.GetCell(0).CellStyle = headStyle;
            return ++rowIndex;
        }

        private int CreateHeader(IWorkbook workbook, ISheet sheet, int rowIndex, int[] arrColWidth)
        {
            IRow headerRow1 = sheet.CreateRow(rowIndex);
            ICellStyle headStyle1 = workbook.CreateCellStyle();
            headStyle1.Alignment = HorizontalAlignment.Center;
            headStyle1.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            headStyle1.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            headStyle1.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            headStyle1.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            IFont font1 = workbook.CreateFont();
            font1.FontHeightInPoints = 14;
            font1.Boldweight = 700;
            font1.FontName = "微软雅黑";
            headStyle1.SetFont(font1);
            for (var m = 0; m < headerList.Count; m++)
            {
                headerRow1.CreateCell(m).SetCellValue(headerList[m]);
                headerRow1.GetCell(m).CellStyle = headStyle1;
                //设置列宽
                sheet.SetColumnWidth(m, (arrColWidth[m] + 1) * 256);
            }
            return ++rowIndex;
        }

        /// <summary>
        /// 读Excel单元格的数据
        /// </summary>
        /// <param name="cell">Excel单元格</param>
        /// <param name="type">列数据类型</param>
        /// <param name="value"></param>
        /// <param name="book"></param>
        /// <returns>object 单元格数据</returns>
        private static void SetCellValue(ICell cell, PropertyInfo type, object value, IWorkbook book)
        {
            string dataType = type.PropertyType.FullName;
            if (typeof(string).Equals(type.PropertyType))
            {
                cell.SetCellValue(Convert.ToString(value));
            }
            else if (typeof(Nullable<int>).Equals(type.PropertyType) || typeof(int).Equals(type.PropertyType))
            {
                cell.SetCellValue(Convert.ToInt32(value));
            }
            else if (typeof(Nullable<bool>).Equals(type.PropertyType))
            {
                cell.SetCellValue(Convert.ToBoolean(value));
            }
            else if (typeof(Nullable<DateTime>).Equals(type.PropertyType))
            {
                cell.SetCellValue(Convert.ToDateTime(value));
            }
            //switch (dataType)
            //{
            //    case "System.String"://字符串类型

            //        cell.SetCellValue(Convert.ToString(value));
            //        break;
            //    case "System.DateTime"://日期类型

            //        ICellStyle cellDateStyle = book.CreateCellStyle();
            //        IDataFormat format = book.CreateDataFormat();
            //        cellDateStyle.DataFormat = format.GetFormat("yyyy-MM-dd hh:mm:ss");//yyyy-mm-dd hh:mm:ss
            //        cell.CellStyle = cellDateStyle;

            //        cell.SetCellValue(Convert.ToDateTime(value));
            //        break;
            //    case "System.Boolean"://布尔型
            //        cell.SetCellValue(Convert.ToBoolean(value));
            //        break;
            //    case "System.Int16"://整型
            //    case "System.Int32":
            //    case "System.Int64":
            //    case "System.Byte":
            //        cell.SetCellValue(Convert.ToUInt64(value));
            //        break;
            //    case "System.Decimal"://浮点型
            //    case "System.Double":
            //        cell.SetCellValue(Convert.ToDouble(value));
            //        break;
            //    case "System.DBNull"://空值处理
            //        cell.SetCellValue("");
            //        break;
            //    default:
            //        throw (new Exception(dataType + "：类型数据无法处理!"));
            //}


        }

        /// <summary>
        /// 当数据量很大时设置excel边框会非常卡
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private ICellStyle SetContentCellStyle(IWorkbook workbook)
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();
            //cellStyle.Alignment = HorizontalAlignment.Center;
            //cellStyle.WrapText = true;//自动换行
            //cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            //cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 10;
            font.Boldweight = 300;
            font.FontName = "微软雅黑";
            cellStyle.SetFont(font);
            return cellStyle;
        }

        public void AutoColumnWidth(ISheet sheet, int cols)
        {
            var maxWidth = 40;
            for (int col = 0; col < cols; col++)
            {
                sheet.AutoSizeColumn(col);//自适应宽度，但是其实还是比实际文本要宽
                int columnWidth = sheet.GetColumnWidth(col) / 256;//获取当前列宽度
                for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    ICell cell = row.GetCell(col);
                    cell.CellStyle.WrapText = true;
                    int contextLength = Encoding.UTF8.GetBytes(cell.ToString()).Length;//获取当前单元格的内容宽度
                    columnWidth = columnWidth < contextLength ? contextLength : columnWidth;

                }
                if (columnWidth > maxWidth)
                {
                    sheet.SetColumnWidth(col, maxWidth * 256);
                }
                else
                {
                    sheet.SetColumnWidth(col, columnWidth * 256);
                }
            }
        }
    }
}
