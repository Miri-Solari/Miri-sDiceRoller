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
        public Dictionary<string, int> Parse(string txt)
        {
            var result = new Dictionary<string, int>();
            var tempRes = Regex.Match(txt, @"\d\d*d").Value.Replace("d", "");
            if (tempRes == null)
            {
                tempRes = "1";
            }
            result.Add("DicePool", Convert.ToInt32(tempRes));

            tempRes = Regex.Match(txt, @"d\d\d*").Value.Replace("d", "");
            if (tempRes == null)
            {
                tempRes = "1";
            }
            result.Add("Dice", Convert.ToInt32(tempRes));

            tempRes = Regex.Match(txt, @">\d\d*").Value.Replace(">", "");
            if (tempRes == null)
            {
                tempRes = "1";
            }
            result.Add("Difficult", Convert.ToInt32(tempRes));

            tempRes = Regex.Match(txt, @"\[\d*,").Value.Replace("[", "").Replace(",", "");
            if (tempRes == "")
            {
                tempRes = "1";
            }
            result.Add("CritFail", Convert.ToInt32(tempRes));

            tempRes = Regex.Match(txt, @",\d*\]").Value.Replace("]", "").Replace(",", "");
            if (tempRes == "")
            {
                tempRes = result["Dice"].ToString();
            }
            result.Add("CritSuccess", Convert.ToInt32(tempRes));

            result.Add("Will", 0);
            if (txt.Contains("W"))
                result["Will"] = 1;

            result.Add("Spec", 0);
            if (txt.Contains("S"))
                result["Spec"] = 1;

            return result;
        }
    }
}
