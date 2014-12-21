using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceBetterTrading.Domain.Extension
{
    public static class StringExtension
    {
        public static int ParseThousandtoString(this string thousandthStr)
        {
            int _value = -1;
            if (!string.IsNullOrEmpty(thousandthStr))
            {
                try
                {
                    _value = int.Parse(thousandthStr, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                }
                catch (Exception ex)
                {
                    _value = -1;
                }
            }
            return _value;
        }

        /// <summary>
        /// 將特殊字元轉換成0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ParseSymbolsTostrZero(this string str)
        {
            string result = str;

            if (str.Trim() == "--")
                result = "0";

            return result;
        }
    }
}
