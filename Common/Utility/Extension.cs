using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utility
{
    public static class Extension
    {
        public static List<string> GetList(this string input, char saperator = ',')
        {
            List<string> list = new List<string>();
            list = input.Split(saperator).Where(a => a.Length > 0).Select(a => a.Trim()).ToList();
            return list;
        }
        public static bool ToBool(this string str)
        {
            if (bool.TryParse(str, out bool boolVal))
                return boolVal;
            else
                return false;
        }
        public static int ToInt(this string str)
        {
            if (int.TryParse(str, out int iVal))
                return iVal;
            else
                return 0;
        }
    }
}
