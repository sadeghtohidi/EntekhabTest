using System.Text.RegularExpressions;

namespace Entekhab.Common
{
    public static class UtilitiesMobile
    {
        public static string UnificationMobile(string mobile)
        {
            mobile = Common.DateUtilites.convertFaToEn(mobile);
            string temp = "";
            if (mobile.Contains("+98"))
            {
                temp = mobile.Replace("+98", "0");
            }
            else if (mobile.Substring(0, 2).Contains("98"))
            {
                temp = mobile.Replace("98", "0");
            }
            else if (mobile[0] == '9' && mobile[1] != '8')
            {
                temp = '0' + mobile;
            }
            else
            {
                temp = mobile.Replace("+", "00");
            }
            return temp;
        }

        public static bool IsValidMobileNumber(this string input)
        {
            //^(\+98|0)?9\d{9}$
            //@"^09[0|1|2|3][0-9]{8}$"
            const string pattern = @"^(\+98|0)?9\d{9}$";

            Regex reg = new Regex(pattern);
            return reg.IsMatch(input);
        }

     
    }
}
