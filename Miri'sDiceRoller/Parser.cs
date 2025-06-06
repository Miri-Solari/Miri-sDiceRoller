using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Miri_sDiceRoller
{
    internal class Parser
    {
        public Dictionary<string, int> Parse(string txt) // Parse our input data with regular expression
        {
            var result = new Dictionary<string, int>();
            var tempRes = Regex.Match(txt, @"\s*\d\d*\s*d").Value.Replace("d", ""); // find Dice Pool
            if (tempRes == null)
            {
                tempRes = "1";
            }
            result.Add("DicePool", Convert.ToInt32(tempRes));

            tempRes = Regex.Match(txt, @"d\s*\d\d*").Value.Replace("d", ""); // find Dice
            if (tempRes == null)
            {
                tempRes = "1";
            }
            result.Add("Dice", Convert.ToInt32(tempRes));

            tempRes = Regex.Match(txt, @">\s*\d\d*").Value.Replace(">", ""); // find difficult
            if (tempRes == null)
            {
                tempRes = "1";
            }
            result.Add("Difficult", Convert.ToInt32(tempRes));

            tempRes = Regex.Match(txt, @"\[\s*\d*\s*,").Value.Replace("[", "").Replace(",", ""); // find critical failure threshold
            if (tempRes == "")
            {
                tempRes = "1";
            }
            result.Add("CritFail", Convert.ToInt32(tempRes));

            tempRes = Regex.Match(txt, @",\s*\d*\s*\]").Value.Replace("]", "").Replace(",", ""); // find critical Success threshold
            if (tempRes == "")
            {
                tempRes = result["Dice"].ToString();
            }
            result.Add("CritSuccess", Convert.ToInt32(tempRes));

            result.Add("Will", 0); // use or not Will
            if (txt.Contains("W"))
                result["Will"] = 1;

            result.Add("Spec", 0); // use or not Spec
            if (txt.Contains("S"))
                result["Spec"] = 1;

            return result;
        }
    }
}
