using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DBModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace WebDemo.Pages
{
    public class ExportDemoModel : PageModel
    {
        [BindProperty]
        public FileUpload FileUpload { get; set; }
        public void OnGet()
        {

        }

        /// <summary>
        /// 附二医院提成单价
        /// </summary>
        private static int SD2HospitalUnitPrice = 5;

        /// <summary>
        /// 中医院提成单价
        /// </summary>
        private static int ZYYHospitalUnitPrice = 7;

        /// <summary>
        /// 华苏药店提成单价
        /// </summary>
        private static int DurgStoreUnitPrice = 8;

        /// <summary>
        /// HSSFWorkbook支持2003，XSSFWorkbook支持2007
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //this.Error = "输入错误";
                return Redirect("~/Upload");
            }
            //保存文件
            string path = Path.Combine(Environment.CurrentDirectory, "Upload", Guid.NewGuid().ToString("n"));
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await FileUpload.SelectedFile.CopyToAsync(fileStream);
            }
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = WorkbookFactory.Create(fs);
            //var sheetName = "附二院宁必泰";
            var sheetCount = workbook.NumberOfSheets;
            var list = new List<DoctorDosageModel>();
            for (var m = 0; m < sheetCount; m++)
            {
                var dealSheet = workbook.GetSheetAt(m);
                list.AddRange(GetDoctorDosageByDrugName(dealSheet));
            }
            var hospitalGroup = list.GroupBy(p => p.Hospital);
            Dictionary<string, List<DoctorDosageStatistics>> doctorDrugDic = new Dictionary<string, List<DoctorDosageStatistics>>();
            foreach (var hospitalDrug in hospitalGroup)
            {
                var dosageStatistics = new List<DoctorDosageStatistics>();
                if (!doctorDrugDic.ContainsKey(hospitalDrug.Key))
                {
                    doctorDrugDic.Add(hospitalDrug.Key, new List<DoctorDosageStatistics>());
                }
                var doctorDrug = hospitalDrug.GroupBy(p => p.Name);
                foreach (var item in doctorDrug)
                {
                    var statistics = new DoctorDosageStatistics
                    {
                        Name = item.Key
                    };
                    foreach (var b in item)
                    {
                        statistics.DoctorDosageList.Add(b);
                        statistics.Money += b.Money;
                    }
                    dosageStatistics.Add(statistics);
                }
                doctorDrugDic[hospitalDrug.Key].AddRange(dosageStatistics);
            }        
            //var result = GetExportToXLSX(list);
            var result = GetExportToXLSX(doctorDrugDic);
            string filename = $"用药量统计汇总{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Headers.Add("Content-Length", result.Length.ToString());
            Response.Headers.Add("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(filename, Encoding.UTF8));
            Response.Body.Write(result, 0, result.Length);
            Response.Body.Flush();
            Response.Body.Close();
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(filename);
            }
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.Headers.Add("Content-Disposition", "attachment;filename=" + "11.xlsx");
            //Response.Body.Write(result,0, result.Length);
            //Response.Body.Flush();
            //Response.Body.Close();

            //if (!string.IsNullOrEmpty(sheetName))
            //{
            //    sheet = workbook.GetSheet(sheetName);
            //    if (sheet == null)
            //    {
            //        sheet = workbook.GetSheetAt(0);
            //    }
            //}
            //else
            //{
            //    sheet = workbook.GetSheetAt(0);
            //}
            ////MemoryStream
            //var saveBook = workbook.CreateSheet("附二院");
            //var saveRow = sheet.CreateRow(0);

            //if (sheet != null)
            //{
            //    IRow firstRow = sheet.GetRow(0);
            //    int cellCount = firstRow.LastCellNum;

            //    var aa = firstRow.GetCell(1).StringCellValue;
            //}
            return Page();
        }

        private IEnumerable<DoctorDosageModel> GetDoctorDosageByDrugName(ISheet originSheet)
        {
            string sheetName = originSheet.SheetName;
            var list = new List<DoctorDosageModel>();
            for (int i = 1; i <= originSheet.LastRowNum; i++)
            {
                var dealRow = originSheet.GetRow(i);
                var dosageValue = 0;
                int.TryParse(dealRow.Cells[2].NumericCellValue.ToString(), out dosageValue);
                var drugStoreValue = 0;
                try
                {
                    int.TryParse(dealRow.Cells[3].NumericCellValue.ToString(), out drugStoreValue);
                }
                catch (Exception ex)
                {
                    drugStoreValue = 0;
                }
                var hospitalUnitPrice = sheetName.Contains("中医院") ? ZYYHospitalUnitPrice : SD2HospitalUnitPrice;
                var doctorDosage = new DoctorDosageModel
                {
                    DrugName = GetDrugName(sheetName),
                    Name = dealRow.Cells[0].StringCellValue,
                    OfficeName = dealRow.Cells[1].StringCellValue,
                    Dosage = dosageValue,
                    DrugStore = drugStoreValue,
                    Hospital = sheetName.Contains("中医院") ? "中医院" : "附二院",
                    Money = hospitalUnitPrice * dosageValue + DurgStoreUnitPrice * drugStoreValue,
                };
                list.Add(doctorDosage);
            }
            return list;
        }

        private string GetDrugName(string sheetName)
        {
            if (sheetName == "附二院宁必泰")
            {
                return "宁必泰";
            }
            else if (sheetName == "附二院坤泰")
            {
                return "坤泰";
            }
            else if (sheetName == "中医院")
            {
                return "坤泰";
            }
            return string.Empty;
        }

        public byte[] GetExportToXLSX(IEnumerable<DoctorDosageModel> doctorDosages)
        {
            IWorkbook workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("附二院");
            var arr = doctorDosages.ToArray();
            for (var i = 0; i < arr.Count(); i++)
            {
                var row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(arr[i].Name);
                row.CreateCell(1).SetCellValue(arr[i].OfficeName);
                row.CreateCell(2).SetCellValue(arr[i].DrugName);
                row.CreateCell(3).SetCellValue(arr[i].Dosage);
                row.CreateCell(4).SetCellValue(arr[i].Dosage * 5);
            }
            //转为字节数组
            var stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();
            return buf;
        }

        public byte[] GetExportToXLSX(Dictionary<string, List<DoctorDosageStatistics>> doctorDosageDic)
        {
            IWorkbook workbook = new XSSFWorkbook();
            foreach (var hospitalName in doctorDosageDic.Keys)
            {
                var sheet = workbook.CreateSheet(hospitalName);
                var arr = doctorDosageDic[hospitalName].ToArray();
                var index = 0;
                var rangeRowIndex = 0;
                var headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("姓名");
                headerRow.CreateCell(1).SetCellValue("科室");
                headerRow.CreateCell(2).SetCellValue("药品名");
                headerRow.CreateCell(3).SetCellValue("用量");
                headerRow.CreateCell(4).SetCellValue("华苏药店");
                headerRow.CreateCell(5).SetCellValue("金额");
                headerRow.CreateCell(6).SetCellValue("总金额");
                //headerRow.GetCell(0).CellStyle = SetCellStyle(workbook);
                foreach (var cell in headerRow.Cells)
                {
                    cell.CellStyle = SetCellStyle(workbook);
                }
                index += 1;
                rangeRowIndex += 1;
                for (var i = 0; i < arr.Count(); i++)
                {
                    var childCount = arr[i].DoctorDosageList.Count();
                    var allMoney = 0;
                    foreach (var item in arr[i].DoctorDosageList)
                    {
                        var row = sheet.CreateRow(index);
                        row.CreateCell(0).SetCellValue(item.Name);
                        row.CreateCell(1).SetCellValue(item.OfficeName);
                        row.CreateCell(2).SetCellValue(item.DrugName);
                        row.CreateCell(3).SetCellValue(item.Dosage);
                        row.CreateCell(4).SetCellValue(item.DrugStore);
                        row.CreateCell(5).SetCellValue(item.Money);
                        index += 1;
                        allMoney += item.Money;
                    }
                    if (childCount > 1)
                    {
                        sheet.AddMergedRegion(new CellRangeAddress(rangeRowIndex, rangeRowIndex + childCount - 1, 0, 0));
                        sheet.AddMergedRegion(new CellRangeAddress(rangeRowIndex, rangeRowIndex + childCount - 1, 6, 6));
                    }
                    sheet.GetRow(rangeRowIndex).CreateCell(6).SetCellValue(allMoney);
                    rangeRowIndex += childCount;
                }
            }
            
            //转为字节数组
            var stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();
            return buf;
        }

        private ICellStyle SetCellStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();//创建样式对象
            style.Alignment= HorizontalAlignment.Center;//设置单元格的样式：水平对齐居中
            IFont font = workbook.CreateFont();
            font.FontName = "微软雅黑"; //和excel里面的字体对应
            font.Boldweight = (short)FontBoldWeight.Bold;//字体加粗
            style.SetFont(font); //将字体样式赋给样式对象
            return style;
        }
    }
}