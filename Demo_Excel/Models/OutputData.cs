using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Excel.Models
{
    public class OutputData
    {
        public string InstructionAddress { get; set; }
        public string Postcode { get; set; }
        public string Airbus_Fetched_Address { get; set; }
        public string Matched_Address { get; set; }
        public int Confidence_Ratio { get; set; }
    }

    public class SearchTerms
    {
        public string postcode { get; set; }
    }

    public class Address
    {
        public string full_address_string { get; set; }
        public string property_code { get; set; }
        public string confidence { get; set; }
    }

    public class Root
    {
        public SearchTerms search_terms { get; set; }
        public List<Address> addresses { get; set; }
    }
}
