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
        public string Matched_PropetyCode_Address { get; set; }
        public string Matched_UPRN_Address { get; set; }
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

    public class PropertyCode_SearchTerms
    {
        public string property_code { get; set; }
    }

    public class PropertyCode_Address
    {
        public string property_number { get; set; }
        public string road { get; set; }
        public string town { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public string epsg { get; set; }
        public string position_x { get; set; }
        public string position_y { get; set; }
        public string uprn { get; set; }
    }

    public class PropertyCode_Root
    {
        public PropertyCode_SearchTerms search_terms { get; set; }
        public PropertyCode_Address address { get; set; }
    }
}
