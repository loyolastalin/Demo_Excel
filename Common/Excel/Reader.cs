using System;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace Common.Excel
{
    public static class Reader
    {
        public static DataTable ReadExcel(string excelPath, string sheetName = "")
        {
            try
            {
                FileStream fs = File.Open(excelPath, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelDataReader = Path.GetExtension(excelPath).ToUpper() == ".XLS" ?
                                                        ExcelReaderFactory.CreateBinaryReader(fs) :
                                                        ExcelReaderFactory.CreateOpenXmlReader(fs);

                DataSet ds;
                using (excelDataReader)
                {
                    ds = excelDataReader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        UseColumnDataType = false,
                        ConfigureDataTable = (tr) => new ExcelDataTableConfiguration { UseHeaderRow = true }
                    });
                }

                return string.IsNullOrEmpty(sheetName) ? ds.Tables[0] : ds.Tables[sheetName];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
