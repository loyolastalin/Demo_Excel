using System;
using System.Net;
using Demo_Excel.Models;
using Newtonsoft.Json;

namespace Demo_Excel
{
    internal class RestAPIClient
    {
      
        public static string GetPropertyCodeContent(string propertyCodeUrl, string propertyCode)
        {
            string conent = string.Empty;
            try
            {

                PropertyCode_Root data = GetContent<PropertyCode_Root>(propertyCodeUrl, propertyCode.ToString());

                // Write values.
                ConsoleLogWriter.WritelineMessage("--- WebClient result ---", ConsoleColor.DarkYellow);
                var address = data.address;
                conent = $" Address : {address.road}  {address.town} {address.country} {address.postcode} \n PROPERTY NUMBER : {address.property_number} \n UPRN : {address.uprn} \n PROPERTY CODE {data.search_terms.property_code}";
                ConsoleLogWriter.WritelineMessage(conent, ConsoleColor.Green);
            }

            catch (Exception ex)
            {
                ConsoleLogWriter.WritelineMessage(ex.Message, ConsoleColor.Red); ;
            }

            return conent;
        }

        public static string GetPostCodeContent(string postCodeUrl, string postCode)
        {
            //return "101, Salisbury Rd, Southall, UB2 5QF Property Code 1032198";
            string conent = string.Empty;
            try
            {


                Root postCodeData = GetContent<Root>(postCodeUrl, postCode);

                // Write values.
                ConsoleLogWriter.WritelineMessage("--- WebClient result ---", ConsoleColor.DarkYellow);
                foreach (var item in postCodeData.addresses)
                {
                    // conent += item.full_address_string + "Property Code " + item.property_code + Environment.NewLine;
                    conent += $"{item.full_address_string} Property Code {item.property_code} \n";
                }
                ConsoleLogWriter.WritelineMessage(conent, ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleLogWriter.WritelineMessage(ex.Message, ConsoleColor.Red); ;
            }

            return conent;
        }

        private static T GetContent<T>(string url, string value)
        {
            T content = default;
            string constructedUrl = url + value;
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

                        content = JsonConvert.DeserializeObject<T>(json_data);

                    }

                }
            }
            catch (Exception ex)
            {
                ConsoleLogWriter.WritelineMessage(ex.Message, ConsoleColor.Red); ;
            }

            return content;
        }
    }
}
