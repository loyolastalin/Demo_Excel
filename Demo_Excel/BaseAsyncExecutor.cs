using Demo_Excel.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace Demo_Excel
{
    internal class BaseAsyncExecutor
    {
        protected static string excelPath = ConfigurationManager.AppSettings["inputExcelPath"];
        protected static string outputPath = ConfigurationManager.AppSettings["outputPath"];
        protected static string post_code_Url = ConfigurationManager.AppSettings["post_code_Url"];
        protected static string property_code_Url = ConfigurationManager.AppSettings["property_code_Url"];
        protected static List<Task<string>> postCodeResults = new List<Task<string>>();
        protected static List<Task<OutputData>> outputDatatasks = new List<Task<OutputData>>();
        protected static List<OutputData> outputDatas = new List<OutputData>();

        protected static Task<string> GetPostCodeContent(string url, string postcode)
        {
            // return Task.Run(() => RestAPIClient.GetContent(url, postcode));
            return Task.Run(() => RestAPIClient.GetPostCodeContent(url, postcode));
        }

        protected static Task<string> GetPropertyCodeContent(string url, string propertycode)
        {
            // return Task.Run(() => RestAPIClient.GetContent(url, postcode));
            return Task.Run(() => RestAPIClient.GetPropertyCodeContent(url, propertycode));
        }

    }
}
