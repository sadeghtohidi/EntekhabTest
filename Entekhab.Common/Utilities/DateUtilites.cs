using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Common
{
    public static class DateUtilites
    {
        public static string ShamsiDateTimeNow()
        {
            var shamsi = new System.Globalization.PersianCalendar();
            string tarikh_saat = shamsi.GetYear(DateTime.Now).ToString() + "/" + shamsi.GetMonth(DateTime.Now).ToString("00") + "/" + shamsi.GetDayOfMonth(DateTime.Now).ToString("00") + " " + DateTime.Now.ToString("HH:mm");
            return tarikh_saat;
        }
        public static string ShamsiDateNow()
        {
            var shamsi = new System.Globalization.PersianCalendar();
            string tarikh_saat = shamsi.GetYear(DateTime.Now).ToString() + "/" + shamsi.GetMonth(DateTime.Now).ToString("00") + "/" + shamsi.GetDayOfMonth(DateTime.Now).ToString("00");
            return tarikh_saat;
        }
        public static string MiladiToShamsiDateTime(DateTime dateTime)
        {
            var shamsi = new System.Globalization.PersianCalendar();
            string tarikh_saat = shamsi.GetYear(dateTime).ToString() + "/" + shamsi.GetMonth(dateTime).ToString("00") + "/" + shamsi.GetDayOfMonth(dateTime).ToString("00") + " " + dateTime.ToString("HH:mm");
            return tarikh_saat;
        }
        public static string MiladiToShamsiDate(DateTime dateTime)
        {
            var shamsi = new System.Globalization.PersianCalendar();
            string tarikh_saat = shamsi.GetYear(dateTime).ToString() + "/" + shamsi.GetMonth(dateTime).ToString("00") + "/" + shamsi.GetDayOfMonth(dateTime).ToString("00");
            return tarikh_saat;
        }
        public static DateTime ShamsiToMiladiDate(string PersianDate)
        {
            var perDate = new System.Globalization.PersianCalendar();
            var dt = perDate.ToDateTime(int.Parse(GetPersianDateYear(PersianDate)), int.Parse(GetPersianDateMonth(PersianDate))
                                            , int.Parse(GetPersianDateDay(PersianDate)), 0, 0, 0, 0);
            return dt;
        }
        public static string MinuteToTime(int n)
        {
            int H = n / 60;
            int m = n % 60;
            string result = H.ToString();
            if (m < 10)
            {
                result = result + ":0" + m.ToString();
            }
            else
            {
                result = result + ":" + m.ToString();
            }
            return result;
        }

        public static int CalculateAge(DateTime BirthDate)
        {
            
                DateTime now = DateTime.Now;
                DateTime birthday = BirthDate;
                string us = birthday.Date.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                int? yearbirthday = System.Convert.ToInt32(us.Substring(0, 4));
                //int? yearbirthday = birthday.Year;

                int age = now.Year - (yearbirthday ?? 1);
                return age;
            
        }
        public static string MiladiToShamsiDateLong(DateTime dateTime) //پنج شنبه 9 خرداد 1392
        {
            var shamsi = new System.Globalization.PersianCalendar();
            //string tarikh_saat = shamsi.GetYear(dateTime).ToString() + "/" + shamsi.GetMonth(dateTime).ToString("00") + "/" + shamsi.GetDayOfMonth(dateTime).ToString("00");
            var tarikh_saat = GetDayOfWeekName(dateTime) + shamsi.GetDayOfMonth(dateTime).ToString("00") + GetMonthName(dateTime) + shamsi.GetYear(dateTime).ToString()  ;    
            return tarikh_saat;
        }
        public static string GetDayOfWeekName(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday: return "شنبه";
                case DayOfWeek.Sunday: return "يکشنبه";
                case DayOfWeek.Monday: return "دوشنبه";
                case DayOfWeek.Tuesday: return "سه شنبه";
                case DayOfWeek.Wednesday: return "چهارشنبه";
                case DayOfWeek.Thursday: return "پنجشنبه";
                case DayOfWeek.Friday: return "جمعه";
                default: return "";
            }
        }
        public static string GetMonthName(this DateTime date)
        {
            PersianCalendar jc = new PersianCalendar();
            string pdate = string.Format("{0:0000}/{1:00}/{2:00}", jc.GetYear(date), jc.GetMonth(date), jc.GetDayOfMonth(date));

            string[] dates = pdate.Split('/');
            int month = System.Convert.ToInt32(dates[1]);

            switch (month)
            {
                case 1: return "فررودين";
                case 2: return "ارديبهشت";
                case 3: return "خرداد";
                case 4: return "تير";
                case 5: return "مرداد";
                case 6: return "شهريور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دي";
                case 11: return "بهمن";
                case 12: return "اسفند";
                default: return "";
            }

        }
        public static int NumberOfweek(DayOfWeek dayOfWeek)
        {
            int first_day_ofmonth=0;
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    first_day_ofmonth = 1;
                    break;
                case DayOfWeek.Monday:
                    first_day_ofmonth = 2;
                    break;
                case DayOfWeek.Tuesday:
                    first_day_ofmonth = 3;
                    break;
                case DayOfWeek.Wednesday:
                    first_day_ofmonth = 4;
                    break;
                case DayOfWeek.Thursday:
                    first_day_ofmonth = 5;
                    break;
                case DayOfWeek.Friday:
                    first_day_ofmonth = 6;
                    break;
                case DayOfWeek.Saturday:
                    first_day_ofmonth = 0;
                    break;
                default:
                    break;
            }
           
            return first_day_ofmonth;
        }
        public static string NameMonthShamsi(int month_new)
        {
            string result = "";
            switch (month_new)
            {
                case 1:
                    result = "فروردین";
                    break;
                case 2:
                    result = "اردیبهشت";
                    break;
                case 3:
                    result = "خرداد";
                    break;
                case 4:
                    result = "تیر";
                    break;
                case 5:
                    result = "مرداد";
                    break;
                case 6:
                    result = "شهریور";
                    break;
                case 7:
                    result = "مهر";
                    break;
                case 8:
                    result = "آبان";
                    break;
                case 9:
                    result = "آذر";
                    break;
                case 10:
                    result = "دی";
                    break;
                case 11:
                    result = "بهمن";
                    break;
                case 12:
                    result = "اسفند";
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }
        public static string convertFaToEn(string Num)
        {
            string d = "";
            //var persianNumbers = [/۰/ g, /۱/ g, /۲/ g, /۳/ g, /۴/ g, /۵/ g, /۶/ g, /۷/ g, /۸/ g, /۹/ g];
            //var arabicNumbers = [/٠/ g, /١/ g, /٢/ g, /٣/ g, /٤/ g, /٥/ g, /٦/ g, /٧/ g, /٨/ g, /٩/ g];
            if (!string.IsNullOrEmpty(Num))
            {
                 d = ConvertDigitsToLatin(Num);
                d = d.Replace("۰", "0");
                d = d.Replace("۱", "1");
                d = d.Replace("۲", "2");
                d = d.Replace("۳", "3");
                d = d.Replace("۴", "4");
                d = d.Replace("۵", "5");
                d = d.Replace("۶", "6");
                d = d.Replace("۷", "7");
                d = d.Replace("۸", "8");
                d = d.Replace("۹", "9");

                d = d.Replace("۰", "0");
                d = d.Replace("۱", "1");
                d = d.Replace("۲", "2");
                d = d.Replace("٣", "3");
                d = d.Replace("٤", "4");
                d = d.Replace("٥", "5");
                d = d.Replace("٦", "6");
                d = d.Replace("٧", "7");
                d = d.Replace("۸", "8");
                d = d.Replace("۹", "9");
            }
                return (d);
        }
        public static string ConvertDigitsToLatin(this string s)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    //Persian digits
                    case '\u06f0':
                        sb.Append('0');
                        break;
                    case '\u06f1':
                        sb.Append('1');
                        break;
                    case '\u06f2':
                        sb.Append('2');
                        break;
                    case '\u06f3':
                        sb.Append('3');
                        break;
                    case '\u06f4':
                        sb.Append('4');
                        break;
                    case '\u06f5':
                        sb.Append('5');
                        break;
                    case '\u06f6':
                        sb.Append('6');
                        break;
                    case '\u06f7':
                        sb.Append('7');
                        break;
                    case '\u06f8':
                        sb.Append('8');
                        break;
                    case '\u06f9':
                        sb.Append('9');
                        break;

                    //Arabic digits    
                    case '\u0660':
                        sb.Append('0');
                        break;
                    case '\u0661':
                        sb.Append('1');
                        break;
                    case '\u0662':
                        sb.Append('2');
                        break;
                    case '\u0663':
                        sb.Append('3');
                        break;
                    case '\u0664':
                        sb.Append('4');
                        break;
                    case '\u0665':
                        sb.Append('5');
                        break;
                    case '\u0666':
                        sb.Append('6');
                        break;
                    case '\u0667':
                        sb.Append('7');
                        break;
                    case '\u0668':
                        sb.Append('8');
                        break;
                    case '\u0669':
                        sb.Append('9');
                        break;
                    default:
                        sb.Append(s[i]);
                        break;
                }
            }
            return sb.ToString();
        }
        private static string GetPersianDateYear(string PersianDate)
        {
            return PersianDate.Substring(0, 4);
        }

        private static string GetPersianDateMonth(string PersianDate)
        {
            if (PersianDate.Trim().Length > 8 || PersianDate.IndexOf('/') > 0 || PersianDate.IndexOf('-') > 0)
            {
                return PersianDate.Substring(5, 2);
            }
            else
            {
                return PersianDate.Substring(4, 2);
            }
        }

        private static string GetPersianDateDay(string PersianDate)
        {
            if (PersianDate.Trim().Length > 8 || PersianDate.IndexOf('/') > 0 || PersianDate.IndexOf('-') > 0)
            {
                return PersianDate.Substring(8, 2);
            }
            else
            {
                return PersianDate.Substring(6, 2);
            }
        }
    }
}
