using Common.Excel;
using Demo_Excel.Models;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Excel
{
    internal class ParalelAsyncExecutor : BaseAsyncExecutor
    {
        public static async Task processDataParallelAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(excelPath) && File.Exists(excelPath))
                {
                    DataTable dataTable = Reader.ReadExcel(excelPath);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        postCodeResults.Add(Task.Run(() => GetPostCodeContent(post_code_Url, row[1].ToString())));

                        var results = await Task.WhenAll(postCodeResults);
                        foreach (var result in results)
                        {

                            // ConsoleLogWriter.WritelineMessage(result, ConsoleColor.Green);

                            outputDatatasks.Add(Task.Run(() => GetOutputData(row[0].ToString(), result, row[1].ToString(), property_code_Url)));

                            var resultoutputDatatasks = await Task.WhenAll(outputDatatasks);

                            foreach (var item in resultoutputDatatasks)
                            {
                                outputDatas.Add(item);
                            }
                        }
                    }

                    Operations.ExportData(outputDatas, Path.Combine(outputPath, $"Output_{DateTime.Now:ddMMyyyy_hhmmss}.xlsx"));
                }
                else
                {
                    ConsoleLogWriter.WritelineMessage($"Input excel path is not configured in appsetting or file is not available at location.", ConsoleColor.Red);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while processing excel... {ex}");
            }
        }
        private static async Task<OutputData> GetOutputData(string expected, string actual, string postcode, string property_code_Url)
        {
            // Fetches the rating using Fuzzy Logic
            var matchedCode = FuzzyProcessor.GetTopMatchedPostCode(expected, actual);
            var postCodeAddressConent = matchedCode.Item1;
            string property_code = postCodeAddressConent.Substring(postCodeAddressConent.IndexOf("Property Code") + "Property Code ".Length);

            // Fetches the property code
            var propertyCodeData = await Task.Run(() => GetPropertyCodeContent(property_code_Url, property_code));

            return new OutputData
            {
                InstructionAddress = expected,
                Postcode = postcode,
                Airbus_Fetched_Address = actual,
                Confidence_Ratio = matchedCode.Item2,
                Matched_PropetyCode_Address = matchedCode.Item1,
                Matched_UPRN_Address = propertyCodeData
            };
        }
    }
}
