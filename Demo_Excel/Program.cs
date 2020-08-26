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
                string url = ConfigurationManager.AppSettings["baseUrl"];
                var outputPath = ConfigurationManager.AppSettings["outputPath"];

               
                if (!string.IsNullOrEmpty(excelPath) && File.Exists(excelPath))
                {
                    DataTable dataTable = Reader.ReadExcel(excelPath);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var result =  await Task.Run(() => DoWork(url,row[1].ToString()));

                        // Console.WriteLine($"result -> {result}");
                        ConsoleLogWriter.WritelineMessage(result, ConsoleColor.Green);

                        outputDatas.Add(new OutputData
                        {
                            InstructionAddress = row[0].ToString(), Postcode = row[1].ToString(), ResultData = result
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

        private static Task<string> DoWork(string url, string postcode)
        {
            return Task.Run(() => RestAPIClient.GetContent(url, postcode));
        }

    }
}
