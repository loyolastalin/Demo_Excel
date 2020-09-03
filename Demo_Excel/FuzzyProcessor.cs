using FuzzySharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Excel
{
    internal class FuzzyProcessor
    {
        public static Tuple<string, int> GetTopMatchedPostCode(string actualInstructionAddress, string expectedAddress)
        {
            var expectedAddressArray = expectedAddress.Split('\n');
            var max = 0;
            var final = string.Empty;

            foreach (var item in expectedAddressArray)
            {
                var ratio = Fuzz.PartialRatio(actualInstructionAddress, item);

                if (max < ratio)
                {
                    final = item;
                    max = ratio;

                }
                //Console.WriteLine($"{item} Ratio is  {ratio}.");
            }
            Console.WriteLine(string.Format("Result  {0} - final Ratio is  {1}.", final, max));
            return new Tuple<string, int>(final, max);
        }
    }
}
