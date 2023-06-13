using System;
using System.Collections.Generic;
using System.Linq;

namespace Ione.Framework.Core.Helpers
{
    /// <summary>
    /// DateHelper
    /// Berisi fungsi-fungsi yang berkaitan dengan tanggal
    /// </summary>
    public class DateHelper
    {
        /// <summary>
        /// GetDay
        /// Hari dalam bahasa indonesia
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetDay(int day = 0)
        {
            switch (day)
            {
                case 0: return "Minggu";
                case 1: return "Senin";
                case 2: return "Selasa";
                case 3: return "Rabu";
                case 4: return "Kamis";
                case 5: return "Jumat";
                case 6: return "Sabtu";
                default: return "-";

            }
        }
        /// <summary>
        /// GetDay
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String GetDay(Object input)
        {
            string output = "";
            string day;
            string[] days = new string[] { };

            try
            {
                if ((input != null) && (!input.Equals(DBNull.Value)))
                {
                    day = Convert.ToString(input).Trim();
                    days = day.Split(';');
                    Array.Sort(days);

                    for (int i = 0; i < days.Count(); i++)
                    {
                        output += String.Concat(GetDayInString(days[i]), ",");
                    }
                    output = output.Substring(0, output.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                output = "";
                ex.SaveLog();
            }
            return output;
        }
        /// <summary>
        /// GetDayInString
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String GetDayInString(string input)
        {
            string output;
            try
            {
                switch (input)
                {
                    case "0":
                        output = "Minggu";
                        break;
                    case "1":
                        output = "Senin";
                        break;
                    case "2":
                        output = "Selasa";
                        break;
                    case "3":
                        output = "Rabu";
                        break;
                    case "4":
                        output = "Kamis";
                        break;
                    case "5":
                        output = "Jumat";
                        break;
                    case "6":
                        output = "Sabtu";
                        break;
                    default:
                        output = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                output = "";
                ex.SaveLog();
            }
            return output;
        }

        /// <summary>
        /// GetDayInNumber
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetDayInNumber(string input)
        {
            string output = string.Empty ;
            try
            {
                switch (input)
                {
                    case "Minggu":
                        output = "0";
                        break;
                    case "Senin":
                        output = "1";
                        break;
                    case "Selasa":
                        output = "2";
                        break;
                    case "Rabu":
                        output = "3";
                        break;
                    case "Kamis":
                        output = "4";
                        break;
                    case "Jumat":
                        output = "5";
                        break;
                    case "Sabtu":
                        output = "6";
                        break;
                    default:
                        output = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.SaveLog();
            }
            return output;
        }

        /// <summary>
        /// GetDayInNumberDb
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Nullable<Decimal> GetDayInNumberDb(string input)
        {
            decimal? output = null;

            try
            {
                if (!String.IsNullOrEmpty(input))
                {
                    input = input.ToUpper().Trim().Replace(" ", String.Empty);
                }
                switch (input)
                {
                    case "MINGGU":
                        output = 1;
                        break;
                    case "SENIN":
                        output = 2;
                        break;
                    case "SELASA":
                        output = 3;
                        break;
                    case "RABU":
                        output = 4;
                        break;
                    case "KAMIS":
                        output = 5;
                        break;
                    case "JUMAT":
                    case "JUM'AT":
                        output = 6;
                        break;
                    case "SABTU":
                        output = 7;
                        break;
                    default:
                        output = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.SaveLog();
            }
            return output;
        }
        /// <summary>
        /// GetFirstDayOfMonth
        /// Hari Pertama dalam bulan tertentu
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(DateTime date)
        {
            DateTime dtFrom = date;

            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            return dtFrom;
        }
        /// <summary>
        /// GetFirstDayOfMonth
        /// Hari Pertama dalam bulan tertentu
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(int month)
        {
            DateTime dtFrom = new DateTime(DateTime.Now.Year, month, 1);

            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));
            return dtFrom;

        }
        /// <summary>
        /// GetLastDayOfMonth
        /// Hari terakhir dalam bulan tertentu
        /// </summary>
        /// <param name="date"></param>
        /// <returns>DateTime</returns>
        public static DateTime GetLastDayOfMonth(DateTime date)
        {
            DateTime dtTo = date;
            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));

            return dtTo;

        }
        /// <summary>
        /// GetLastDayOfMonth
        /// Hari terakhir dalam bulan tertentu
        /// </summary>
        /// <param name="month"></param>
        /// <returns>DateTime</returns>
        public static DateTime GetLastDayOfMonth(int month)
        {
            DateTime dtTo = new DateTime(DateTime.Now.Year, month, 1);

            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));
            return dtTo;

        }
        /// <summary>
        /// GetTanggalText
        /// Tanggal dalam bentuk text
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetTanggalText(string day = "1")
        {
            switch (day)
            {
                case "01": return "Satu";
                case "02": return "Dua";
                case "03": return "Tiga";
                case "04": return "Empat";
                case "05": return "Lima";
                case "06": return "Enam";
                case "07": return "Tujuh";
                case "08": return "Delapan";
                case "09": return "Sembilan";
                case "10": return "Sepuluh";
                case "11": return "Sebelas";
                case "12": return "Dua belas";
                case "13": return "Tiga belas";
                case "14": return "Empat belas";
                case "15": return "Lima belas";
                case "16": return "Enam belas";
                case "17": return "Tujuh belas";
                case "18": return "Delapan belas";
                case "19": return "Sembilan belas";
                case "20": return "Dua puluh";
                case "21": return "Dua puluh Satu";
                case "22": return "Dua puluh Dua";
                case "23": return "Dua puluh Tiga";
                case "24": return "Dua puluh Empat";
                case "25": return "Dua puluh Lima";
                case "26": return "Dua puluh Enam";
                case "27": return "Dua puluh Tujuh";
                case "28": return "Dua puluh Delapan";
                case "29": return "Dua puluh Sembilan";
                case "30": return "Tiga puluh";
                case "31": return "Tiga puluh Satu";
                default: return "-";
            }
        }
        /// <summary>
        /// GetMonthText
        /// Bulan dalam bentuk text
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string GetMonthText(string month = "1")
        {
            switch (month)
            {
                case "01": return "Januari";
                case "02": return "Februari";
                case "03": return "Maret";
                case "04": return "April";
                case "05": return "Mei";
                case "06": return "Juni";
                case "07": return "Juli";
                case "08": return "Agustus";
                case "09": return "September";
                case "10": return "Oktober";
                case "11": return "November";
                case "12": return "Desember";
                default: return "-";

            }
        }
        /// <summary>
        /// GetAge
        /// Usia berdasarkan tanggal sekarang
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public static Int32 GetAge(string birthday)
        {
            Int32 output;
            try
            {
                if (!String.IsNullOrEmpty(birthday))
                {
                    int yearNow = DateTime.Today.Year;
                    int yearHis = Convert.ToInt32(birthday);
                    output = yearNow - yearHis;
                }
                else
                {
                    output = 0;
                }
            }
            catch (Exception ex)
            {
                output = 0;
                ex.SaveLog();
            }

            return output;
        }
        /// <summary>
        /// EachDay
        /// </summary>
        /// <param name="from"></param>
        /// <param name="thru"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
            /*Penggunaan*/
            /*
             foreach (DateTime day in EachDay(StartDate, EndDate)){}
             */
        }

    }
}
