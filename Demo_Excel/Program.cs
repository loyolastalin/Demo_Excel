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

                        // Fetches the rating using Fuzzy Logic
                        var matchedCode = FuzzyProcessor.GetTopMatchedPostCode(row[0].ToString(), result);

                        outputDatas.Add(new OutputData
                        {
                            InstructionAddress = row[0].ToString(), Postcode = row[1].ToString(), 
                            Airbus_Fetched_Address = result,
                            Confidence_Ratio = matchedCode.Item2, 
                            Matched_Address = matchedCode.Item1 
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
            // return Task.Run(() => RestAPIClient.GetContent(url, postcode));
        var instactedAddresses = @"5, Silvertown Sq, London, E16 1GW
Morrisons, 3, Silvertown Sq, Canning Town, London, E16 1GW
Superdrug, 5, Silvertown Sq, Canning Town, London, E16 1GW
Wm Morrison Supermarkets Plc, 3, Silvertown Sq, London, E16 1GW
1, Exeter Rd, Canning Town, London, E16 1GN
1, Exeter Rd, London, E16 1GN
1, Hastings Rd, Canning Town, London, E16 1GF
1, Hastings Rd, London, E16 1GF
1, The Crystal, Siemens Brothers Way, Canning Town, London, E16 1GB
10, Exeter Rd, Canning Town, London, E16 1GN
10, Exeter Rd, London, E16 1GN
10, Hastings Rd, Canning Town, London, E16 1GJ
10, Hastings Rd, London, E16 1GJ
11, Hastings Rd, Canning Town, London, E16 1GF
11, Hastings Rd, London, E16 1GF
12, Exeter Rd, Canning Town, London, E16 1GN
12, Exeter Rd, London, E16 1GN
12, Hastings Rd, Canning Town, London, E16 1GJ
12, Hastings Rd, London, E16 1GJ
13, Hastings Rd, Canning Town, London, E16 1GF
13, Hastings Rd, London, E16 1GF
14, Exeter Rd, Canning Town, London, E16 1GN
14, Exeter Rd, London, E16 1GN
14, Hastings Rd, Canning Town, London, E16 1GJ
14, Hastings Rd, London, E16 1GJ
15, Hastings Rd, Canning Town, London, E16 1GF
15, Hastings Rd, London, E16 1GF
16, Hastings Rd, Canning Town, London, E16 1GJ
16, Hastings Rd, London, E16 1GJ
17, Hastings Rd, Canning Town, London, E16 1GF
17, Hastings Rd, London, E16 1GF
18, Hastings Rd, Canning Town, London, E16 1GJ
18, Hastings Rd, London, E16 1GJ
19, Hastings Rd, Canning Town, London, E16 1GF
19, Hastings Rd, London, E16 1GF
2, Exeter Rd, Canning Town, London, E16 1GN
2, Exeter Rd, London, E16 1GN
2, Hastings Rd, Canning Town, London, E16 1GJ
2, Hastings Rd, London, E16 1GJ
20, Hastings Rd, Canning Town, London, E16 1GJ
20, Hastings Rd, London, E16 1GJ
21, Hastings Rd, Canning Town, London, E16 1GF
21, Hastings Rd, London, E16 1GF
22, Hastings Rd, Canning Town, London, E16 1GJ
22, Hastings Rd, London, E16 1GJ
23, Hastings Rd, Canning Town, London, E16 1GF
23, Hastings Rd, London, E16 1GF
25, Hastings Rd, Canning Town, London, E16 1GF
25, Hastings Rd, London, E16 1GF
26, Hastings Rd, Canning Town, London, E16 1GJ
26, Hastings Rd, London, E16 1GJ
27, Hastings Rd, Canning Town, London, E16 1GF
27, Hastings Rd, London, E16 1GF
3, Exeter Rd, Canning Town, London, E16 1GN
3, Exeter Rd, London, E16 1GN
3, Hastings Rd, Canning Town, London, E16 1GF
3, Hastings Rd, London, E16 1GF
31, Hastings Rd, Canning Town, London, E16 1GF
31, Hastings Rd, London, E16 1GF
33, Hastings Rd, Canning Town, London, E16 1GF
33, Hastings Rd, London, E16 1GF
35, Hastings Rd, Canning Town, London, E16 1GF
35, Hastings Rd, London, E16 1GF
37, Hastings Rd, Canning Town, London, E16 1GF
37, Hastings Rd, London, E16 1GF
39, Hastings Rd, Canning Town, London, E16 1GF
39, Hastings Rd, London, E16 1GF
4, Exeter Rd, Canning Town, London, E16 1GN
4, Exeter Rd, London, E16 1GN
4, Hastings Rd, Canning Town, London, E16 1GJ
4, Hastings Rd, London, E16 1GJ
43, Hastings Rd, Canning Town, London, E16 1GF
43, Hastings Rd, London, E16 1GF
45, Hastings Rd, Canning Town, London, E16 1GF
45, Hastings Rd, London, E16 1GF
47, Hastings Rd, Canning Town, London, E16 1GF
47, Hastings Rd, London, E16 1GF
5, Exeter Rd, Canning Town, London, E16 1GN
5, Exeter Rd, London, E16 1GN
5, Hastings Rd, Canning Town, London, E16 1GF
5, Hastings Rd, London, E16 1GF
6, Exeter Rd, Canning Town, London, E16 1GN
6, Exeter Rd, London, E16 1GN
6, Hastings Rd, Canning Town, London, E16 1GJ
6, Hastings Rd, London, E16 1GJ
7, Exeter Rd, Canning Town, London, E16 1GN
7, Exeter Rd, London, E16 1GN
8, Hastings Rd, Canning Town, London, E16 1GJ
8, Hastings Rd, London, E16 1GJ
9, Hastings Rd, Canning Town, London, E16 1GF
9, Hastings Rd, London, E16 1GF
Flat 1, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 1, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 10, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 10, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 100, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 101, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 102, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 11, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 11, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 12, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 12, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 13, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 13, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 14, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 14, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 15, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 15, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 16, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 16, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 17, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 17, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 18, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 18, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 19, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 19, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 2, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 2, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 20, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 20, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 21, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 21, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 22, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 22, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 23, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 23, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 24, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 24, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 25, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 25, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 26, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 26, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 27, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 27, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 28, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 28, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 29, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 29, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 3, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 3, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 30, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 30, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 31, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 31, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 32, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 32, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 33, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 33, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 34, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 34, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 35, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 35, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 36, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 36, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 37, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 37, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 38, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 39, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 4, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 4, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 40, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 41, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 42, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 43, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 44, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 45, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 46, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 47, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 48, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 49, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 5, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 5, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 50, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 51, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 52, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 53, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 54, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 55, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 56, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 57, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 58, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 59, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 6, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 6, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 60, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 61, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 62, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 63, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 64, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 65, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 66, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 67, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 68, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 69, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 7, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 7, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 70, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 71, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 72, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 73, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 74, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 75, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 76, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 77, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 78, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 79, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 8, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 8, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 80, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 81, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 82, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 83, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 84, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 85, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 86, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 87, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 88, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 89, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 9, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
Flat 9, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
Flat 90, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 91, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 92, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 93, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 94, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 95, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 96, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 97, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 98, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Flat 99, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
Strawberry Star, The Pavillion, Siemens Brothers Way, Canning Town, London, E16 1GB
The Crystal, 1, Siemens Brothers Way, London, E16 1GB
The Oiler Bar, Western Gateway, Canning Town, London, E16 1GB
Concierge And Offices, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
New Sustainability Centre, The Crystal, 1, Siemens Brothers Way, London, E16 1GB
1, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
1, 24, Holly Ct, Hastings Rd, Canning Town, London, E16 1GJ
1, 29, Tilly Ct, Hastings Rd, Canning Town, London, E16 1GF
1, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
1, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
1, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
1, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
1, Burnt Ash Apartments, 29, Tarling Rd, London, E16 1GA
1, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
1, Evan House, 8, Exeter Rd, London, E16 1GP
1, Flat 1, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 10, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 11, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 12, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 13, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 14, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 15, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 16, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 17, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 18, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 19, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 2, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 20, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 21, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 22, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 23, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 24, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 25, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 26, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 27, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 28, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 29, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 3, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 30, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 31, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 32, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 33, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 34, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 35, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 36, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 37, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 4, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 5, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 6, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 7, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 8, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Flat 9, Rathbone Market, Barking Rd, Canning Town, London, E16 1GS
1, Florian Ct, 41, Hastings Rd, London, E16 1GH
1, Holly Ct, 24, Hastings Rd, London, E16 1GJ
1, Jubilee Ct, 99, Rathbone St, London, E16 1GU
1, Maddison Ct, 7, Hastings Rd, London, E16 1GG
1, Tilly Ct, 29, Hastings Rd, London, E16 1GF
10, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
10, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
10, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
10, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
10, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
10, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
10, Evan House, 8, Exeter Rd, London, E16 1GP
10, Florian Ct, 41, Hastings Rd, London, E16 1GH
10, Jubilee Ct, 99, Rathbone St, London, E16 1GU
10, Maddison Ct, 7, Hastings Rd, London, E16 1GG
11, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
11, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
11, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
11, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
11, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
11, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
11, Evan House, 8, Exeter Rd, London, E16 1GP
11, Florian Ct, 41, Hastings Rd, London, E16 1GH
11, Jubilee Ct, 99, Rathbone St, London, E16 1GU
11, Maddison Ct, 7, Hastings Rd, London, E16 1GG
12, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
12, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
12, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
12, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
12, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
12, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
12, Evan House, 8, Exeter Rd, London, E16 1GP
12, Flat 1, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 10, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 100, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 101, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 102, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 11, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 12, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 13, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 14, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 15, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 16, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 17, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 18, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 19, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 2, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 20, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 21, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 22, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 23, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 24, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 25, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 26, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 27, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 28, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 29, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 3, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 30, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 31, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 32, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 33, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 34, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 35, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 36, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 37, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 38, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 39, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 4, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 40, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 41, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 42, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 43, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 44, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 45, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 46, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 47, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 48, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 49, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 5, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 50, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 51, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 52, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 53, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 54, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 55, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 56, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 57, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 58, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 59, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 6, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 60, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 61, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 62, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 63, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 64, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 65, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 66, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 67, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 68, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 69, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 7, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 70, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 71, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 72, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 73, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 74, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 75, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 76, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 77, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 78, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 79, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 8, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 80, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 81, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 82, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 83, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 84, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 85, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 86, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 87, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 88, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 89, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 9, Rathbone Market, Barking Rd, Canning Town, London, E16 1GY
12, Flat 90, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 91, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 92, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 93, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 94, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 95, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 96, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 97, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 98, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Flat 99, Rathbone Market, Barking Rd, Canning Town, London, E16 1GZ
12, Florian Ct, 41, Hastings Rd, London, E16 1GH
12, Jubilee Ct, 99, Rathbone St, London, E16 1GU
12, Maddison Ct, 7, Hastings Rd, London, E16 1GG
13, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
13, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
13, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
13, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
13, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
13, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
13, Evan House, 8, Exeter Rd, London, E16 1GP
13, Florian Ct, 41, Hastings Rd, London, E16 1GH
13, Jubilee Ct, 99, Rathbone St, London, E16 1GU
13, Maddison Ct, 7, Hastings Rd, London, E16 1GG
14, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
14, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
14, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
14, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
14, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
14, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
14, Evan House, 8, Exeter Rd, London, E16 1GP
14, Florian Ct, 41, Hastings Rd, London, E16 1GH
14, Jubilee Ct, 99, Rathbone St, London, E16 1GU
14, Maddison Ct, 7, Hastings Rd, London, E16 1GG
15, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
15, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
15, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
15, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
15, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
15, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
15, Evan House, 8, Exeter Rd, London, E16 1GP
15, Florian Ct, 41, Hastings Rd, London, E16 1GH
15, Jubilee Ct, 99, Rathbone St, London, E16 1GU
15, Maddison Ct, 7, Hastings Rd, London, E16 1GG
16, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
16, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
16, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
16, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
16, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
16, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
16, Evan House, 8, Exeter Rd, London, E16 1GP
16, Florian Ct, 41, Hastings Rd, London, E16 1GH
16, Jubilee Ct, 99, Rathbone St, London, E16 1GU
16, Maddison Ct, 7, Hastings Rd, London, E16 1GG
17, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
17, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
17, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
17, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
17, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
17, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
17, Evan House, 8, Exeter Rd, London, E16 1GP
17, Florian Ct, 41, Hastings Rd, London, E16 1GH
17, Jubilee Ct, 99, Rathbone St, London, E16 1GU
17, Maddison Ct, 7, Hastings Rd, London, E16 1GG
18, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
18, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
18, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
18, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
18, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
18, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
18, Evan House, 8, Exeter Rd, London, E16 1GP
18, Florian Ct, 41, Hastings Rd, London, E16 1GH
18, Jubilee Ct, 99, Rathbone St, London, E16 1GU
18, Maddison Ct, 7, Hastings Rd, London, E16 1GG
19, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
19, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
19, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
19, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
19, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
19, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
19, Evan House, 8, Exeter Rd, London, E16 1GP
19, Florian Ct, 41, Hastings Rd, London, E16 1GH
19, Jubilee Ct, 99, Rathbone St, London, E16 1GU
19, Maddison Ct, 7, Hastings Rd, London, E16 1GG
1a, Burnt Ash Apartments, 29, Tarling Rd, London, E16 1GA
2, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
2, 24, Holly Ct, Hastings Rd, Canning Town, London, E16 1GJ
2, 29, Tilly Ct, Hastings Rd, Canning Town, London, E16 1GF
2, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
2, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
2, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
2, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
2, Burnt Ash Apartments, 29, Tarling Rd, London, E16 1GA
2, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
2, Evan House, 8, Exeter Rd, London, E16 1GP
2, Florian Ct, 41, Hastings Rd, London, E16 1GH
2, Holly Ct, 24, Hastings Rd, London, E16 1GJ
2, Jubilee Ct, 99, Rathbone St, London, E16 1GU
2, Maddison Ct, 7, Hastings Rd, London, E16 1GG
2, Tilly Ct, 29, Hastings Rd, London, E16 1GF
20, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
20, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
20, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
20, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
20, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
20, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
20, Evan House, 8, Exeter Rd, London, E16 1GP
20, Florian Ct, 41, Hastings Rd, London, E16 1GH
20, Jubilee Ct, 99, Rathbone St, London, E16 1GU
20, Maddison Ct, 7, Hastings Rd, London, E16 1GG
21, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
21, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
21, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
21, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
21, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
21, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
21, Evan House, 8, Exeter Rd, London, E16 1GP
21, Florian Ct, 41, Hastings Rd, London, E16 1GH
21, Jubilee Ct, 99, Rathbone St, London, E16 1GU
21, Maddison Ct, 7, Hastings Rd, London, E16 1GG
22, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
22, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
22, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
22, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
22, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
22, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
22, Evan House, 8, Exeter Rd, London, E16 1GP
22, Florian Ct, 41, Hastings Rd, London, E16 1GH
22, Jubilee Ct, 99, Rathbone St, London, E16 1GU
22, Maddison Ct, 7, Hastings Rd, London, E16 1GG
23, 1, Eddington Ct, Silvertown Sq, Canning Town, London, E16 1GW
23, 41, Florian Ct, Hastings Rd, Canning Town, London, E16 1GH
23, 7, Maddison Ct, Hastings Rd, Canning Town, London, E16 1GG
23, 8, Evan House, Exeter Rd, Canning Town, London, E16 1GP
23, 99, Jubilee Ct, Rathbone St, Canning Town, London, E16 1GU
23, Eddington Ct, 1, Silvertown Sq, London, E16 1GW
23, Evan Ho";
            //  return Task.Run(() => instactedAddresses);
            return Task.Run(() => RestAPIClient.GetContent(url, postcode));
        }

    }
}
