using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Common.Excel
{
    public static class Operations
    {
        public static void ExportData<T>(List<T> exportData, string filePath, string workSheetName = "")
        {
            string workSheet = string.IsNullOrEmpty(workSheetName) ? "Result" : workSheetName;
            FileInfo fi = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fi))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets.Add(workSheet);
                excelWorksheet.Cells["A1"].LoadFromCollection(exportData, true);
                package.Save();
            }
        }
    }
}
