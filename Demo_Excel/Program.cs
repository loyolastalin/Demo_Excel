using Common.Excel;
using Demo_Excel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Demo_Excel
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<OutputData> outputDatas = new List<OutputData>();

            try
            {
                string excelPath = ConfigurationManager.AppSettings["inputExcelPath"];
                var outputPath = ConfigurationManager.AppSettings["outputPath"];
                string post_code_Url = ConfigurationManager.AppSettings["post_code_Url"];
                string property_code_Url = ConfigurationManager.AppSettings["property_code_Url"];

                if (!string.IsNullOrEmpty(excelPath) && File.Exists(excelPath))
                {
                    DataTable dataTable = Reader.ReadExcel(excelPath);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var result = await Task.Run(() => GetPostCodeContent(post_code_Url, row[1].ToString()));

                        // Console.WriteLine($"result -> {result}");
                        ConsoleLogWriter.WritelineMessage(result, ConsoleColor.Green);

                        // Fetches the rating using Fuzzy Logic
                        var matchedCode = FuzzyProcessor.GetTopMatchedPostCode(row[0].ToString(), result);
                        var postCodeAddressConent = matchedCode.Item1;
                        string property_code = postCodeAddressConent.Substring(postCodeAddressConent.IndexOf("Property Code") + "Property Code ".Length);

                        // Fetches the property code
                        var propertyCodeData = await Task.Run(() => GetPropertyCodeContent(property_code_Url, property_code));

                        outputDatas.Add(new OutputData
                        {
                            InstructionAddress = row[0].ToString(),
                            Postcode = row[1].ToString(),
                            Airbus_Fetched_Address = result,
                            Confidence_Ratio = matchedCode.Item2,
                            Matched_PropetyCode_Address = matchedCode.Item1,
                            Matched_UPRN_Address = propertyCodeData
                        });
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

            ConsoleLogWriter.WritelineMessage("Completed, Press key to exit.", ConsoleColor.DarkBlue);
            Console.ReadLine();
        }

        private static Task<string> GetPostCodeContent(string url, string postcode)
        {
            // return Task.Run(() => RestAPIClient.GetContent(url, postcode));
            return Task.Run(() => RestAPIClient.GetPostCodeContent(url, postcode));
        }

        private static Task<string> GetPropertyCodeContent(string url, string propertycode)
        {
            // return Task.Run(() => RestAPIClient.GetContent(url, postcode));
            return Task.Run(() => RestAPIClient.GetPropertyCodeContent(url, propertycode));
        }
    }
}
