using System;
using System.Net;
using Demo_Excel.Models;
using Newtonsoft.Json;

namespace Demo_Excel
{
    internal class RestAPIClient
    {
        public static string GetContent(string baseUrl, string postCode)
        {
            string constructedUrl = baseUrl + postCode;
            string conent = string.Empty;
            try
            {

                ConsoleLogWriter.WritelineMessage(constructedUrl, ConsoleColor.White);
                ConsoleLogWriter.WritelineMessage(Environment.NewLine + new string('*', 50), ConsoleColor.White);

                using (WebClient client = new WebClient())
                {
                    client.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0)";
                    client.UseDefaultCredentials = true;

                    // Download data.
                    var json_data = client.DownloadString(constructedUrl);

                    if (!string.IsNullOrEmpty(json_data))
                    {

                        Root zzz = JsonConvert.DeserializeObject<Root>(json_data);

                        // Write values.
                        ConsoleLogWriter.WritelineMessage("--- WebClient result ---", ConsoleColor.DarkYellow);
                        foreach (var item in zzz.addresses)
                        {
                            conent += item.full_address_string + Environment.NewLine;
                        }
                        //ConsoleLogWriter.WritelineMessage(conent, ConsoleColor.Green);
                    }

                }
            }
            catch (Exception ex)
            {
                ConsoleLogWriter.WritelineMessage(ex.Message, ConsoleColor.Red); ;
            }

            return conent;
        }
    }
}
