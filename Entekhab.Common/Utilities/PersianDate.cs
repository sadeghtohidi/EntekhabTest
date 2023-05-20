using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasad.Common.Utilities
{
    public static class PersianDate
    {
        public static string tarikh_saat()
        {
            var shamsi = new System.Globalization.PersianCalendar();
            string tarikh_saat = shamsi.GetYear(DateTime.Now).ToString() + "/" + shamsi.GetMonth(DateTime.Now).ToString("00") + "/" + shamsi.GetDayOfMonth(DateTime.Now).ToString("00") + " " + DateTime.Now.ToString("HH:mm");
            return tarikh_saat;
        }
        public static string tarikh()
        {
            var shamsi = new System.Globalization.PersianCalendar();
            string tarikh_saat = shamsi.GetYear(DateTime.Now).ToString() + "/" + shamsi.GetMonth(DateTime.Now).ToString("00") + "/" + shamsi.GetDayOfMonth(DateTime.Now).ToString("00");
            return tarikh_saat;
        }
        public static string MiladiToShamsi(DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();

            DateTime dt = dateTime;

            string month, day;
            if (pc.GetMonth(dt) < 10)
                month = "0" + pc.GetMonth(dt).ToString();
            else
                month = pc.GetMonth(dt).ToString();
            if (pc.GetDayOfMonth(dt) < 10)
                day = "0" + pc.GetDayOfMonth(dt).ToString();
            else
                day = pc.GetDayOfMonth(dt).ToString();

            string PersianDate = string.Format("{0}/{1}/{2} {3}:{4}", pc.GetYear(dt), month, day, pc.GetHour(dt), pc.GetMinute(dt));
            return PersianDate;
        }
    }
}
