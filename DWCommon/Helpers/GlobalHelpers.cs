using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWCommon.Helpers
{
    public static class GlobalHelpers
    {
        public static KeyValuePair<bool, int> ConvertStringToInt(string str)
        {
            try
            {
                int x = Convert.ToInt32(Math.Round(Convert.ToDouble(str)));
                return new KeyValuePair<bool, int>(true, x);
            }
            catch (Exception)
            {

                return new KeyValuePair<bool, int>(false, 0);
            }
        }

        public static KeyValuePair<bool, int> ConvertObjectToInt(this object obj)
        {
            try
            {
                int x = Convert.ToInt32(Math.Round(Convert.ToDouble(obj)));
                return new KeyValuePair<bool, int>(true, x);
            }
            catch (Exception)
            {

                return new KeyValuePair<bool, int>(false, 0);
            }
        }

        public static KeyValuePair<bool, string> ConverStringToSqlDate(this string obj)
        {
            try
            {
                var x = DateTime.Parse(obj);
                return new KeyValuePair<bool, string>(true, string.Format("{0}-{1}-{2}", 
                    x.Year, x.Month, x.Day));
            }
            catch (Exception)
            {

                return new KeyValuePair<bool, string>(false, "YYYY-MM-DD");
            }
        }
        public static string FormatSqlString(string str)
        {

            var res = str.Replace("'", @"''");

            return res;
        }


    }
}
