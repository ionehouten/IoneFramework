using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Ione.Framework.Core
{
    /// <summary>
    /// Converter 
    /// Berisi fungsi-fungsi untuk mengkonversi tipe data
    /// </summary>
    public static partial class Converter
    {
        /// <summary>
        /// Konversi object ke decimal
        /// </summary>
        /// <example>
        /// Contoh konversi ke decimal
        /// <code>
        /// string value =  100;
        /// decimal contoh1 =  value.ToDecimal();
        /// decimal contoh2 =  Converter.ToDecimal(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>decimal</returns>
        public static Decimal ToDecimal(this object input)
        {
            Decimal output;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(string))
                        if (string.IsNullOrEmpty((string)input))
                            return 0;
                    return Convert.ToDecimal(input, CultureInfo.InvariantCulture);
                }
                else
                {
                    output = 0;
                }
            }
            catch
            {
                output = 0;
            }
            return output;

        }

        /// <summary>
        /// Konversi object ke double
        /// </summary>
        /// <example>
        /// Contoh konversi ke double
        /// <code>
        /// string value =  100;
        /// double contoh1 =  value.ToDouble();
        /// double contoh2 =  Converter.ToDouble(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>double</returns>
        public static double ToDouble(this object input)
        {
            double output = 0;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(string))
                        if (string.IsNullOrEmpty((string)input))
                            return 0;
                    return Convert.ToDouble(input, CultureInfo.InvariantCulture);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke nullable decimal
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable decimal
        /// <code>
        /// string value =  100;
        /// decimal? contoh1 =  value.ToDecimalNull();
        /// decimal? contoh2 =  Converter.ToDecimalNull(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>decimal?</returns>
        public static decimal? ToDecimalNull(this object input)
        {
            decimal? output = null;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(string))
                        if (string.IsNullOrEmpty((string)input))
                            return 0;
                    return Convert.ToDecimal(input, CultureInfo.InvariantCulture);
                }
            }
            catch
            {
            }
            return output;

        }

        /// <summary>
        /// Konversi object ke nullable double
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable double
        /// <code>
        /// string value =  100;
        /// double? contoh1 =  value.ToDoubleNull();
        /// double? contoh2 =  Converter.ToDoubleNull(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>double?</returns>
        public static double? ToDoubleNull(this object input)
        {
            double? output = null;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(string))
                        if (string.IsNullOrEmpty((string)input))
                            return 0;
                    return Convert.ToDouble(input, CultureInfo.InvariantCulture);
                }
            }
            catch
            {
            }
            return output;

        }

        /// <summary>
        /// Konversi object ke Int16
        /// </summary>
        /// <example>
        /// Contoh konversi ke Int16
        /// <code>
        /// string value =  100;
        /// Int16 contoh1 =  value.ToInt16();
        /// Int16 contoh2 =  Converter.ToInt16(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>Int16</returns>
        public static Int16 ToInt16(this object input)
        {
            Int16 output = 0;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(string))
                        if (string.IsNullOrEmpty((string)input))
                            return 0;
                    return Convert.ToInt16(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke UInt16
        /// </summary>
        /// <example>
        /// Contoh konversi ke UInt16
        /// <code>
        /// string value =  100;
        /// UInt16 contoh1 =  value.ToUInt16();
        /// UInt16 contoh2 =  Converter.ToUInt16(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>Int16</returns>
        public static UInt16 ToUInt16(this object input)
        {
            UInt16 output = 0;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(string))
                        if (string.IsNullOrEmpty((string)input))
                            return 0;
                    return Convert.ToUInt16(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke Int32
        /// </summary>
        /// <example>
        /// Contoh konversi ke Int32
        /// <code>
        /// string value =  100;
        /// Int32 contoh1 =  value.ToInt32();
        /// Int32 contoh2 =  Converter.ToInt32(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>Int32</returns>
        public static Int32 ToInt32(this object input)
        {
            Int32 output = 0;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(DBNull))
                        return 0;
                    if (input.GetType() == typeof(string))
                        if (string.IsNullOrEmpty((string)input))
                            return 0;
                    return Convert.ToInt32(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke UInt32
        /// </summary>
        /// <example>
        /// Contoh konversi ke UInt32
        /// <code>
        /// string value =  100;
        /// UInt32 contoh1 =  value.ToUInt32();
        /// UInt32 contoh2 =  Converter.ToUInt32(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>UInt32</returns>
        public static UInt32 ToUInt32(this object input)
        {
            UInt32 output = 0;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(string))
                        if (string.IsNullOrEmpty((string)input))
                            return 0;
                    return Convert.ToUInt32(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke Int64
        /// </summary>
        /// <example>
        /// Contoh konversi ke Int64
        /// <code>
        /// string value =  100;
        /// Int64 contoh1 =  value.ToInt64();
        /// Int64 contoh2 =  Converter.ToInt64(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>Int64</returns>
        public static Int64 ToInt64(this object input)
        {
            Int64 output = 0;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(string))
                        if (string.IsNullOrEmpty((string)input))
                            return 0;
                    return Convert.ToInt64(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke UInt64
        /// </summary>
        /// <example>
        /// Contoh konversi ke UInt64
        /// <code>
        /// string value =  100;
        /// UInt64 contoh1 =  value.ToUInt64();
        /// UInt64 contoh2 =  Converter.ToUInt64(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>UInt64</returns>
        public static UInt64 ToUInt64(this object input)
        {
            UInt64 output = 0;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(string))
                        if (string.IsNullOrEmpty((string)input))
                            return 0;
                    return Convert.ToUInt64(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke nullable Int16
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable Int16
        /// <code>
        /// string value =  100;
        /// Int16? contoh1 =  value.ToInt16Null();
        /// Int16? contoh2 =  Converter.ToInt16Null(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>Int16?</returns>
        public static Int16? ToInt16Null(this object input)
        {
            Nullable<Int16> output = null;
            try
            {
                if ((input != null))
                {
                    return Convert.ToInt16(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke nullable UInt16
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable UInt16
        /// <code>
        /// string value =  100;
        /// UInt16? contoh1 =  value.ToUInt16Null();
        /// UInt16? contoh2 =  Converter.ToUInt16Null(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>UInt16?</returns>
        public static UInt16? ToUInt16Null(this object input)
        {
            Nullable<UInt16> output = null;
            try
            {
                if ((input != null))
                {
                    return Convert.ToUInt16(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke nullable Int32
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable Int32
        /// <code>
        /// string value =  100;
        /// Int32? contoh1 =  value.ToInt32Null();
        /// Int32? contoh2 =  Converter.ToInt32Null(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>Int32?</returns>
        public static Int32? ToInt32Null(this object input)
        {
            Nullable<Int32> output = null;
            try
            {
                if ((input != null))
                {
                    if (input.GetType() == typeof(DBNull))
                        return null;
                    return Convert.ToInt32(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke nullable UInt32
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable UInt32
        /// <code>
        /// string value =  100;
        /// UInt32? contoh1 =  value.ToUInt32Null();
        /// UInt32? contoh2 =  Converter.ToUInt32Null(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>Int32?</returns>
        public static UInt32? ToUInt32Null(this object input)
        {
            Nullable<UInt32> output = null;
            try
            {
                if ((input != null))
                {
                    return Convert.ToUInt32(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke nullable Int64
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable Int64
        /// <code>
        /// string value =  100;
        /// Int64? contoh1 =  value.ToInt64Null();
        /// Int64? contoh2 =  Converter.ToInt64Null(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>Int64?</returns>
        public static Int64? ToInt64Null(this object input)
        {
            Nullable<Int64> output = null;
            try
            {
                if ((input != null))
                {
                    return Convert.ToInt64(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke nullable UInt64
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable UInt64
        /// <code>
        /// string value =  100;
        /// UInt64? contoh1 =  value.ToUInt64Null();
        /// UInt64? contoh2 =  Converter.ToUInt64Null(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>UInt64?</returns>
        public static UInt64? ToUInt64Null(this object input)
        {
            Nullable<UInt64> output = null;
            try
            {
                if ((input != null))
                {
                    return Convert.ToUInt64(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke string
        /// </summary>
        /// <example>
        /// Contoh konversi ke string
        /// <code>
        /// int value =  100;
        /// string contoh1 =  value.ToString();
        /// string contoh2 =  Converter.ToString(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>string</returns>
        public static string ToString(this object input)
        {
            string output = null;
            try
            {
                if ((input != null))
                {
                    return Convert.ToString(input).Trim();
                }
            }
            catch
            {
            }
            return output;

        }

        /// <summary>
        /// Konversi object ke bool
        /// </summary>
        /// <example>
        /// Contoh konversi ke bool
        /// <code>
        /// int value =  1;
        /// bool contoh1 =  value.ToBoolean();
        /// bool contoh2 =  Converter.ToBoolean(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>bool</returns>
        public static bool ToBoolean(this object input)
        {
            bool output = false;
            try
            {
                if ((input != null))
                {
                    return Convert.ToBoolean(input);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke nullable TimeSpan
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable TimeSpan
        /// <code>
        /// DateTime value =  DateTime.Now;
        /// TimeSpan? contoh1 =  value.ToTimeSpanNull();
        /// TimeSpan? contoh2 =  Converter.ToTimeSpanNull(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>TimeSpan?</returns>
        public static TimeSpan? ToTimeSpanNull(this object input)
        {
            Nullable<TimeSpan> output = null;
            try
            {
                if ((input != null))
                {
                    output = TimeSpan.Parse(input.ToString());
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke nullable DateTime
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable DateTime
        /// <code>
        /// string value =  "2017-02-02";
        /// DateTime? contoh1 =  value.ToDateTime();
        /// DateTime? contoh2 =  Converter.ToDateTime(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>DateTime?</returns>
        public static DateTime? ToDateTime(this object input)
        {
            DateTime? output = null;
            try
            {
                if ((input != null))
                {
                    string str_date = string.Format("{0:yyyy-MM-dd}", input);
                    if (str_date.Length > 8)
                    {
                        str_date = str_date.Substring(0, 8);
                    }
                    if (str_date != "0/0/0000")
                    {
                        output = Convert.ToDateTime(input);
                    }
                    else
                    {
                        output = null;
                    }

                }
            }
            catch
            {
            }
            return output;
        }


        /// <summary>
        /// Konversi object ke nullable DateTime
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable DateTime
        /// <code>
        /// string value =  "2017-02-02";
        /// DateTime? contoh1 =  value.ToDateTime();
        /// DateTime? contoh2 =  Converter.ToDateTime(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>DateTime?</returns>
        public static DateTime? ToDateTimeGmt(this object input, int gmt = 7)
        {
            DateTime? output = null;

            try
            {
                if ((input != null))
                {
                    output = Convert.ToDateTime(input);

                    
                    double hours = gmt - (Double.Parse(output.Value.ToString("zz").Replace("+", "")));
                    DateTime convertedDate = output.Value.ToUniversalTime();
                    output = output.Value.AddHours(hours);
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi object ke DateTime
        /// </summary>
        /// <example>
        /// Contoh konversi ke DateTime
        /// <code>
        /// string value =  "2017-02-02";
        /// DateTime contoh1 =  value.ToDateTime();
        /// DateTime contoh2 =  Converter.ToDateTime(value);
        /// </code>
        /// </example>
        /// <param name="input">string</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(this string input)
        {
            DateTime output = new DateTime(1999, 09, 09);
            try
            {
                if ((input != null))
                {
                    output = Convert.ToDateTime(input);
                }

            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi DateTime? ke DateTime
        /// </summary>
        /// <example>
        /// Contoh konversi ke DateTime
        /// <code>
        /// string value =  "2017-02-02";
        /// DateTime contoh1 =  value.ToDateTime();
        /// DateTime contoh2 =  Converter.ToDateTime(value);
        /// </code>
        /// </example>
        /// <param name="input">string</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(this DateTime? input)
        {
            DateTime output = new DateTime(1999, 09, 09);
            try
            {
                if ((input != null))
                {
                    output = Convert.ToDateTime(input);
                }

            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi UnixTimestamp ke DateTime
        /// </summary>
        /// <param name="input">long</param>
        /// <returns>DateTime</returns>
        public static DateTime? ToDateTimeFromUnix(this double input)
        {
            DateTime output = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            try
            {
                if ((input >= 0))
                {
                    output = output.AddSeconds(input).ToLocalTime();
                }
                else
                {
                    output = output.Subtract(TimeSpan.FromSeconds(input));
                }

            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Konversi Excel Date ke DateTime
        /// </summary>
        /// <example>
        /// Contoh konversi ke DateTime
        /// <code>
        /// string value =  "2017-02-02";
        /// DateTime contoh1 =  value.ToDateTimeFromExcel();
        /// DateTime contoh2 =  Converter.ToDateTimeFromExcel(value);
        /// </code>
        /// </example>
        /// <param name="date">string</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTimeFromExcel(this string date)
        {
            try
            {
                if (DateTime.TryParse(date, out DateTime dt))
                {
                    return dt;
                }

                if (double.TryParse(date, out double oaDate))
                {
                    return DateTime.FromOADate(oaDate);
                }

            }
            catch 
            {
                
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Konversi object ke TimeSpan
        /// </summary>
        /// <example>
        /// Contoh konversi ke TimeSpan
        /// <code>
        /// DateTime value =  DateTime.Now;
        /// TimeSpan contoh1 =  value.ToTimeSpan();
        /// TimeSpan contoh2 =  Converter.ToTimeSpan(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan ToTimeSpan(this object input)
        {
            TimeSpan output = new TimeSpan(00, 00, 00);
            try
            {
                if ((input != null))
                {
                    TimeSpan.TryParse(input.ToString(), out output);
                }
            }
            catch
            {
            }
            return output;
        }

        ///// <summary>
        ///// Konversi object ke Image
        ///// </summary>
        ///// <example>
        ///// Contoh konversi ke Image
        ///// <code>
        ///// byte[] value = File.ReadAllBytes("imagepath");
        ///// Image contoh1 =  value.ToImage();
        ///// Image contoh2 =  Converter.ToImage(value);
        ///// </code>
        ///// </example>
        ///// <param name="input">object</param>
        ///// <returns>Image</returns>
        //public static Image ToImage(this object input)
        //{
        //    Image output = null;
        //    using (output = null)
        //    {
        //        try
        //        {
        //            if ((input != null))
        //            {
        //                if (!string.IsNullOrEmpty(input.ToString()))
        //                {
        //                    byte[] bytes = (byte[])input;
        //                    MemoryStream ms = new MemoryStream(bytes);
        //                    output = Image.FromStream(ms);
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }
        //        return output;
        //    }

        //}

        /// <summary>
        /// Konversi string ke byte[]
        /// </summary>
        /// <example>
        /// Contoh konversi ke byte[]
        /// <code>
        /// string value = "c:/";
        /// byte[] contoh1 =  value.ToByte();
        /// byte[] contoh2 =  Converter.ToByte(value);
        /// </code>
        /// </example>
        /// <param name="input">string</param>
        /// <returns>byte[]</returns>
        public static byte[] ToByte(this string input)
        {
            byte[] output = null;
            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    FileStream fs = new FileStream(input,
                                           FileMode.Open,
                                           FileAccess.Read);
                    output = new byte[fs.Length];
                    fs.Read(output, 0, Convert.ToInt32(fs.Length));
                }
            }
            catch
            {
            }
            return output;

        }

        ///// <summary>
        ///// Konversi Image ke byte[]
        ///// </summary>
        ///// <example>
        ///// Contoh konversi ke byte[]
        ///// <code>
        ///// Image value = Image.FromFile("imagepath");
        ///// byte[] contoh1 =  value.ToByte();
        ///// byte[] contoh2 =  Converter.ToByte(value);
        ///// </code>
        ///// </example>
        ///// <param name="input">Image</param>
        ///// <returns>byte[]</returns>
        //public static byte[] ToByte(this Image input)
        //{
        //    byte[] output = null;
        //    try
        //    {
        //        if ((input != null))
        //        {
        //            MemoryStream ms = new MemoryStream();
        //            //input.Save(ms, input.RawFormat);
        //            input.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //            output = ms.ToArray();
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    return output;
        //}

        ///// <summary>
        ///// Konversi object ke byte[]
        ///// </summary>
        ///// <example>
        ///// Contoh konversi ke byte[]
        ///// <code>
        ///// string value = "100";
        ///// byte[] contoh1 =  value.ToByte();
        ///// byte[] contoh2 =  Converter.ToByte(value);
        ///// </code>
        ///// </example>
        ///// <param name="input">object</param>
        ///// <returns>byte[]</returns>
        //public static byte[] ToByte(this object input)
        //{
        //    if (input == null)
        //        return null;
        //    BinaryFormatter bf = new BinaryFormatter();
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        bf.Serialize(ms, input);
        //        return ms.ToArray();
        //    }
        //}

        /// <summary>
        /// Konversi object ke nullable byte
        /// </summary>
        /// <example>
        /// Contoh konversi ke nullable byte
        /// <code>
        /// string value = "100";
        /// byte? contoh1 =  value.ToByteNull();
        /// byte? contoh2 =  Converter.ToByteNull(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>byte?</returns>
        public static byte? ToByteNull(this object input)
        {
            byte? output = null;
            try
            {
                if ((input != null))
                {
                    output = Convert.ToByte(input);
                }
            }
            catch
            {
            }
            return output;



        }

        /// <summary>
        /// Konversi object FromBase64String ke byte[] 
        /// </summary>
        /// <example>
        /// Contoh konversi FromBase64String ke byte[]
        /// <code>
        /// string value = "100";
        /// byte[] contoh1 =  value.ToByte();
        /// byte[] contoh2 =  Converter.ToByte(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>byte[]</returns>
        public static byte[] ToByteArrayFromBase64(this object input)
        {
            byte[] output = null;
            try
            {
                if ((input != null))
                {
                    output = Convert.FromBase64String(input.ToString());
                }
            }
            catch
            {
            }

            return output;
        }

        /// <summary>
        /// Konversi string ke byte[]
        /// </summary>
        /// <example>
        /// Contoh konversi ke byte[]
        /// <code>
        /// string value = "100";
        /// byte[] contoh1 =  value.ToByte();
        /// byte[] contoh2 =  Converter.ToByte(value);
        /// </code>
        /// </example>
        /// <param name="input">string</param>
        /// <returns>byte[]</returns>
        public static byte[] ToByteFromHex(this string input)
        {
            byte[] output = null;
            try
            {
                input = input.Replace("-", "");
                byte[] raw = new byte[input.Length / 2];
                for (int i = 0; i < raw.Length; i++)
                {
                    raw[i] = Convert.ToByte(input.Substring(i * 2, 2), 16);
                }
                output = raw;
            }
            catch
            {
            }
            return output;

        }

        /// <summary>
        /// Konversi byte[] ke Hexadecimal menggunakan metode BitConverter
        /// </summary>
        /// <example>
        /// Contoh konversi ke Hexadecimal
        /// <code>
        /// byte[] value = File.ReadAllBytes("path");
        /// string contoh1 =  value.ToHexStringUsingBit();
        /// string contoh2 =  Converter.ToHexStringUsingBit(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>string</returns>
        public static string ToHexStringUsingBit(this byte[] input)
        {
            string output = null;
            try
            {
                if (input != null)
                {
                    output = BitConverter.ToString(input).Replace("-", "");//Str to Hex
                }
            }
            catch
            {
            }
            return output;

        }

        /// <summary>
        /// Konversi byte[] ke Hexadecimal menggunakan metode ASCIIEncoding
        /// </summary>
        /// <example>
        /// Contoh konversi ke Hexadecimal
        /// <code>
        /// byte[] value = File.ReadAllBytes("path");
        /// string contoh1 =  value.ToHexStringUsingASCIIEncoding();
        /// string contoh2 =  Converter.ToHexStringUsingASCIIEncoding(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>string</returns>
        public static string ToHexStringUsingASCIIEncoding(this byte[] input)
        {

            string output = null;
            try
            {
                if (input != null)
                {
                    output = ASCIIEncoding.Default.GetString(input);
                }
            }
            catch
            {
            }
            return output;

        }

        /// <summary>
        /// Konversi byte[] ke Hexadecimal menggunakan metode Base64
        /// </summary>
        /// <example>
        /// Contoh konversi ke Hexadecimal
        /// <code>
        /// byte[] value = File.ReadAllBytes("path");
        /// string contoh1 =  value.ToHexStringUsingBase64();
        /// string contoh2 =  Converter.ToHexStringUsingBase64(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>string</returns>
        public static string ToHexStringUsingBase64(this byte[] input)
        {
            /*
            StringBuilder hex = new StringBuilder(input.Length * 2);
            foreach (byte b in input)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();*/
            string output = null;
            try
            {
                if (input != null)
                {
                    output = Convert.ToBase64String(input);
                    //output = Encoding.UTF8.GetString(input);
                    //output = HttpServerUtility.UrlTokenEncode(bytes); 
                    /*
                    StringBuilder hex = new StringBuilder(input.Length * 2);
                    foreach (byte b in input)
                        hex.AppendFormat("{0:x2}", b);
                    output =  hex.ToString();*/

                    //output = BitConverter.ToString(input);
                    //return output.Replace("-", "");
                }
            }
            catch
            {
            }
            return output;

        }

        /// <summary>
        /// Konversi byte[] ke Hexadecimal menggunakan metode StringBuilder
        /// </summary>
        /// <example>
        /// Contoh konversi ke Hexadecimal
        /// <code>
        /// byte[] value = File.ReadAllBytes("path");
        /// string contoh1 =  value.ToHexStringUsingStringBuilder();
        /// string contoh2 =  Converter.ToHexStringUsingStringBuilder(value);
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <returns>string</returns>
        public static string ToHexStringUsingStringBuilder(this byte[] input)
        {
            string output = null;
            try
            {
                if (input != null)
                {

                    StringBuilder hex = new StringBuilder(input.Length * 2);
                    foreach (byte b in input)
                        hex.AppendFormat("{0:x2}", b);
                    output = hex.ToString();
                }
            }
            catch
            {
            }
            return output;

        }

        /// <summary>
        /// Konversi string ke literal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToLiteral(this string input)
        {
            var literal = new StringBuilder(input.Length + 2);
            //literal.Append("\"");
            foreach (var c in input)
            {
                switch (c)
                {
                    case '\'': literal.Append(@"\'"); break;
                    case '\"': literal.Append("\\\""); break;
                    case '\\': literal.Append(@"\\"); break;
                    case '\0': literal.Append(@"\0"); break;
                    case '\a': literal.Append(@"\a"); break;
                    case '\b': literal.Append(@"\b"); break;
                    case '\f': literal.Append(@"\f"); break;
                    case '\n': literal.Append(@"\n"); break;
                    case '\r': literal.Append(@"\r"); break;
                    case '\t': literal.Append(@"\t"); break;
                    case '\v': literal.Append(@"\v"); break;
                    default:
                        if (Char.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.Control)
                        {
                            literal.Append(c);
                        }
                        else
                        {
                            literal.Append(@"\u");
                            literal.Append(((ushort)c).ToString("x4"));
                        }
                        break;
                }
            }
            //literal.Append("\"");
            return literal.ToString();
        }

        /// <summary>
        /// Konversi decimal ke string currency
        /// </summary>
        /// <param name="input"></param>
        /// <param name="currencySymbol"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStringCurrency(this decimal input, string currencySymbol = "Rp.", string format = "#,#")
        {
            string output;
            try
            {
                if ((input != 0) && (!input.Equals(DBNull.Value)))
                {
                    return string.Format("{0} {1}", currencySymbol, input.ToString(format, CultureInfo.CurrentCulture));

                }
                else
                {
                    return string.Format("{0} {1}", currencySymbol, "0");
                }
            }
            catch
            {
                output = "";
            }
            return output;
        }
        /// <summary>
        /// Konversi decimal null ke string currency
        /// </summary>
        /// <param name="input"></param>
        /// <param name="currencySymbol"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStringCurrency(decimal? input = null, string currencySymbol = "Rp.", string format = "#,#")
        {
            string output;
            decimal value = Converter.ToDecimal(input);
            try
            {
                if ((value != 0) && (!value.Equals(DBNull.Value)))
                {
                    return string.Format("{0} {1}", currencySymbol, value.ToString(format, CultureInfo.CurrentCulture));

                }
                else
                {
                    return string.Format("{0} {1}", currencySymbol, "0");
                }
            }
            catch
            {
                output = "";
            }
            return output;
        }

        /// <summary>
        /// Format Date 
        /// </summary>
        /// <example>
        /// Contoh Format Date
        /// <code>
        /// DateTime value =  DateTime.Now;
        /// string contoh1 =  value.FormatDate();
        /// string contoh2 =  Converter.FormatDate(value, "dd-MM-yyyy");
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <param name="format">dd-MM-yyyy</param>
        /// <returns>string</returns>
        public static string FormatDate(this object input, string format = "dd-MM-yyyy")
        {
            string output = "";
            DateTime? toDateTime;
            try
            {
                if ((input != null))
                {
                    toDateTime = Converter.ToDateTime(input);
                    if (toDateTime != null)
                    {
                        output = toDateTime.Value.ToString(format);
                    }
                }
            }
            catch
            {
            }
            return output;
        }

        /// <summary>
        /// Format Time 
        /// </summary>
        /// <example>
        /// Contoh Format Time
        /// <code>
        /// DateTime value =  DateTime.Now;
        /// string contoh1 =  value.FormatTime();
        /// string contoh2 =  Converter.FormatTime(value, "HH:mm:ss");
        /// </code>
        /// </example>
        /// <param name="input">object</param>
        /// <param name="format">HH:mm:ss</param>
        /// <returns>string</returns>
        public static string FormatTime(this object input, string format = "HH:mm:ss")
        {
            string output = "";
            DateTime? toDateTime;
            try
            {
                if ((input != null))
                {
                    toDateTime = Converter.ToDateTime(input);
                    if (toDateTime != null)
                    {
                        output = toDateTime.Value.ToString(format);
                    }
                }
            }
            catch
            {
            }
            return output;
        }

        ///// <summary>
        ///// Mengubah ukuran gambar
        ///// </summary>
        ///// <param name="img"></param>
        ///// <param name="size"></param>
        ///// <returns></returns>
        //public static Image ResizeImage(Image img, Size size)
        //{
        //    Bitmap newImage = new Bitmap(size.Width, size.Height);
        //    if (img != null)
        //    {
        //        using (Graphics gr = Graphics.FromImage(newImage))
        //        {
        //            gr.SmoothingMode = SmoothingMode.HighQuality;
        //            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //            gr.DrawImage(img, new Rectangle(0, 0, size.Width, size.Height));

        //        }
        //    }

        //    return (Image)newImage;
        //}

        /// <summary>
        /// Cek Null Date (Khusus aplikasi SIMAN)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime? CheckDate(this DateTime? input)
        {
            DateTime? output = null;
            try
            {
                if (Converter.FormatDate(input, "dd/MM/yyyy") != "11/11/1111" && Converter.FormatDate(input, "dd/MM/yyyy") != "11-11-1111")
                {
                    output = input;
                    if (output.Value.Year < 1970 && output.Value.TimeOfDay != TimeSpan.Zero)
                    {
                        output = input.Value.AddDays(1).Date + new TimeSpan(0, 0, 0);
                    }
                }
            }
            catch
            {

            }
            
            return output;
        }

        /// <summary>
        /// ToUnixTimestamp
        /// </summary>
        /// <param name="input"></param>
        /// <returns>double</returns>
        public static double ToUnixTimestamp(this DateTime input)
        {
            double output = 0;
            
            try
            {
                output = (input.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            }
            catch
            {
            }
            return output;
        }
        /// <summary>
        /// ToRoman
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }
        /// <summary>
        /// SerializeObject
        /// </summary>
        /// <param name="input"></param>
        /// <returns>string</returns>
        public static string SerializeObject(this object input)
        {
            string output = "";

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(input.GetType());

                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, input);
                    output =  textWriter.ToString();
                    //remove whitespace
                    //output = Regex.Replace(output, @"\t|\n|\r", "");
                }
            }
            catch (Exception ex)
            {
                ex.SaveLog();
            }
            return output;
        }

        /// <summary>
        /// SerializeObject
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string</returns>
        public static T DeserializeObject<T>(this string data) where T : new()
        {
            T output = new T();

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(output.GetType());

                using (TextReader reader = new StringReader(data.Trim()))
                {
                    output = (T)xmlSerializer.Deserialize(reader);
                }
            }
            catch(Exception ex)
            {
                ex.SaveLog();
            }
            return output;
        }


        /// <summary>
        /// GetSignaturType
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Authenticator.OAuth.SignatureTypes GetSignaturType(this string input)
        {
            Authenticator.OAuth.SignatureTypes output = Authenticator.OAuth.SignatureTypes.HMACSHA1;
            switch (input)
            {
                case "HMAC-SHA1":
                    output = Authenticator.OAuth.SignatureTypes.HMACSHA1;
                    break;
                case "HMAC-SHA256":
                    output = Authenticator.OAuth.SignatureTypes.HMACSHA256;
                    break;
                case "PLAINTEXT":
                    output = Authenticator.OAuth.SignatureTypes.PLAINTEXT;
                    break;
            }
            return output;
        }
        /// <summary>
        /// GetSignaturType
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetSignaturType(this Authenticator.OAuth.SignatureTypes input)
        {
            string output = "HMAC-SHA1";
            if(input == Authenticator.OAuth.SignatureTypes.HMACSHA1)
            {
                output = "HMAC-SHA1";
            }
            else if(input == Authenticator.OAuth.SignatureTypes.HMACSHA256)
            {
                output = "HMAC-SHA256";
            }
            else if (input == Authenticator.OAuth.SignatureTypes.PLAINTEXT)
            {
                output = "PLAINTEXT";
            }
            return output;
        }

        /// <summary>
        /// FormatBytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytes(this long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
    }



    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string Format;
        public CustomDateTimeConverter(string format)
        {
            Format = format;
        }
        public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
        {
            writer.WriteStringValue(date.ToString(Format));
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), Format, null);
        }
    }
}
