using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Peygir.Logic
{
    public class DateTimeFormatter
    {
        public string DateTimePattern { get; private set; }

        public Calendar Calendar { get; private set; }

        public DateTimeFormatter(string dateTimePattern, Calendar calendar)
            : this
            (
                dateTimePattern,
                calendar,
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.AMDesignator,
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.PMDesignator
            )
        {
            // Nothing.
        }

        public DateTimeFormatter(string dateTimePattern, Calendar calendar, string amDesignator, string pmDesignator)
        {
            Initialize(dateTimePattern, calendar, amDesignator, pmDesignator);
        }

        public DateTimeFormatter(string dateTimePattern, string calendarName)
            : this
            (
                dateTimePattern,
                calendarName,
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.AMDesignator,
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.PMDesignator
            )
        {
            // Nothing.
        }

        public DateTimeFormatter(string dateTimePattern, string calendarName, string amDesignator, string pmDesignator)
        {
            Calendar calendar = null;
            switch (calendarName)
            {
                case "ChineseLunisolarCalendar":
                    calendar = new ChineseLunisolarCalendar();
                    break;

                case "GregorianCalendar":
                    calendar = new GregorianCalendar();
                    break;

                case "HebrewCalendar":
                    calendar = new HebrewCalendar();
                    break;

                case "HijriCalendar":
                    calendar = new HijriCalendar();
                    break;

                case "JapaneseCalendar":
                    calendar = new JapaneseCalendar();
                    break;

                case "JapaneseLunisolarCalendar":
                    calendar = new JapaneseLunisolarCalendar();
                    break;

                case "JulianCalendar":
                    calendar = new JulianCalendar();
                    break;

                case "KoreanCalendar":
                    calendar = new KoreanCalendar();
                    break;

                case "KoreanLunisolarCalendar":
                    calendar = new KoreanLunisolarCalendar();
                    break;

                case "PersianCalendar":
                    calendar = new PersianCalendar();
                    break;

                case "TaiwanCalendar":
                    calendar = new TaiwanCalendar();
                    break;

                case "TaiwanLunisolarCalendar":
                    calendar = new TaiwanLunisolarCalendar();
                    break;

                case "ThaiBuddhistCalendar":
                    calendar = new ThaiBuddhistCalendar();
                    break;

                case "UmAlQuraCalendar":
                    calendar = new UmAlQuraCalendar();
                    break;
            }

            Initialize(dateTimePattern, calendar, amDesignator, pmDesignator);
        }

        public string Format(DateTime dateTime)
        {
            // Format   Description
            // --------------------------------------------------------------------------------
            // yyyyyy   The year in six digits. Pads with leading zeros to get six digits.
            // yyyyy    The year in five digits. Pads with leading zeros to get five digits.
            // yyyy     The year in four or five digits (depending on the calendar used), including the century. Pads with leading zeros to get four digits.
            // yyy      The year in three digits. If the year is less than 100, the year is displayed with a leading zero.
            // yy       The year without the century. If the year without the century is less than 10, the year is displayed with a leading zero.
            // y        The year without the century. If the year without the century is less than 10, the year is displayed with no leading zero.
            // MM       The numeric month. Single-digit months have a leading zero.
            // M        The numeric month. Single-digit months do not have a leading zero.
            // dd       The day of the month. Single-digit days have a leading zero.
            // d        The day of the month. Single-digit days do not have a leading zero.
            // HH       The hour in a 24-hour clock. Single-digit hours have a leading zero.
            // H        The hour in a 24-hour clock. Single-digit hours do not have a leading zero.
            // hh       The hour in a 12-hour clock. Single-digit hours have a leading zero.
            // h        The hour in a 12-hour clock. Single-digit hours do not have a leading zero.
            // tt       The AM/PM designator.
            // mm       The minute. Single-digit minutes have a leading zero.
            // m        The minute. Single-digit minutes do not have a leading zero.
            // ss       The second. Single-digit seconds have a leading zero.
            // s        The second. Single-digit seconds do not have a leading zero.

            string yyyyyy = string.Format("{0:000000}", Calendar.GetYear(dateTime));
            string yyyyy = string.Format("{0:00000}", Calendar.GetYear(dateTime));
            string yyyy = string.Format("{0:0000}", Calendar.GetYear(dateTime));
            string yyy = string.Format("{0:000}", Calendar.GetYear(dateTime));
            string yy = string.Format("{0:00}", Calendar.GetYear(dateTime) % 100);
            string y = string.Format("{0:0}", Calendar.GetYear(dateTime) % 100);

            string MM = string.Format("{0:00}", Calendar.GetMonth(dateTime));
            string M = string.Format("{0:0}", Calendar.GetMonth(dateTime));

            string dd = string.Format("{0:00}", Calendar.GetDayOfMonth(dateTime));
            string d = string.Format("{0:0}", Calendar.GetDayOfMonth(dateTime));

            int hour = Calendar.GetHour(dateTime);

            string HH = string.Format("{0:00}", hour);
            string H = string.Format("{0:0}", hour);

            string hh = string.Format("{0:00}", (hour % 12) == 0 ? 12 : hour % 12);
            string h = string.Format("{0:0}", (hour % 12) == 0 ? 12 : hour % 12);

            string tt = (hour < 12) ? amDesignator : pmDesignator;

            string mm = string.Format("{0:00}", Calendar.GetMinute(dateTime));
            string m = string.Format("{0:0}", Calendar.GetMinute(dateTime));

            string ss = string.Format("{0:00}", Calendar.GetSecond(dateTime));
            string s = string.Format("{0:0}", Calendar.GetSecond(dateTime));

            // Format.
            StringBuilder result = new StringBuilder(DateTimePattern);
            result.Replace("yyyyyy", yyyyyy);
            result.Replace("yyyyy", yyyyy);
            result.Replace("yyyy", yyyy);
            result.Replace("yyy", yyy);
            result.Replace("yy", yy);
            result.Replace("y", y);
            result.Replace("MM", MM);
            result.Replace("M", M);
            result.Replace("dd", dd);
            result.Replace("d", d);
            result.Replace("HH", HH);
            result.Replace("H", H);
            result.Replace("hh", hh);
            result.Replace("h", h);
            result.Replace("tt", tt);
            result.Replace("mm", mm);
            result.Replace("m", m);
            result.Replace("ss", ss);
            result.Replace("s", s);

            return result.ToString();
        }

        private void Initialize(string dateTimePattern, Calendar calendar, string amDesignator, string pmDesignator)
        {
            if (dateTimePattern == null)
            {
                throw new ArgumentNullException("dateTimePattern");
            }
            if (calendar == null)
            {
                throw new ArgumentNullException("calendar");
            }
            if (amDesignator == null)
            {
                throw new ArgumentNullException("amDesignator");
            }
            if (pmDesignator == null)
            {
                throw new ArgumentNullException("pmDesignator");
            }

            DateTimePattern = dateTimePattern;
            Calendar = calendar;
            this.amDesignator = amDesignator;
            this.pmDesignator = pmDesignator;
        }

        private string amDesignator;
        private string pmDesignator;
    }
}
