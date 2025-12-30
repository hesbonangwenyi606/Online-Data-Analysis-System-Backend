using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.ServiceModel;
//using System.ServiceModel.Channels;
using System.Globalization;
//using Microsoft.SqlServer.Types;
using System.IO;
using System.Security.Cryptography;
using System.Text; 

namespace FOS.Shared.Common
{
    public class AppUtility
    {
        
        public static IFormatProvider getDateFormatProvider()
        {
            DateTimeFormatInfo dateFormat = (DateTimeFormatInfo)new System.Globalization.CultureInfo("da-DK").DateTimeFormat.Clone();
            dateFormat.ShortDatePattern = "yyyy-MM-dd";
            dateFormat.LongDatePattern = "yyyy-MM-dd";
            dateFormat.FullDateTimePattern = "yyyy-MM-dd";
            dateFormat.DateSeparator = "-";

            return dateFormat;
        }

        public static IFormatProvider getDateTimeFormatProvider()
        {
            DateTimeFormatInfo dateFormat = (DateTimeFormatInfo)new System.Globalization.CultureInfo("da-DK").DateTimeFormat.Clone();
            dateFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
            dateFormat.LongDatePattern = "yyyy-MM-dd HH:mm:ss";
            dateFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
            dateFormat.DateSeparator = "-";
            dateFormat.TimeSeparator = ":";

            return dateFormat;
        }

        public static string SafeSqlLiteral(string inputSQL)
        {
            if (!String.IsNullOrEmpty(inputSQL))
                return inputSQL.Replace("'", "''");
            else
                return "";
        }

        public static List<String> CommaValuesToList(string commaSeparatedText)
        {
            List<String> list = new List<string>();

            if (commaSeparatedText == null || commaSeparatedText == "")
                return list;

            list = commaSeparatedText.Split(',').Select(x => x.ToString().Trim().ToLower()).ToList();
            return list;

        }

        public static string ListToCommaValues(List<string> lst)
        {
            if (lst == null)
                return "";

            string retVal = "";
            foreach (string s in lst)
                retVal += s + ",";

            retVal = retVal.Trim().TrimEnd(',');

            return retVal;
        }

        public static string ListToLines(List<string> lst)
        { 
            string retVal = ListToCommaValues(lst).Replace(",", Environment.NewLine);
            
            return retVal;
        }

        public static string ListToSingleLineText(List<string> lst)
        {
            if (lst == null)
                return "";

            string retVal = "";
            foreach (string s in lst)
                retVal += s;

            retVal = retVal.Trim();

            return retVal;
        }

         

        #region Safe Type Conversion


        public static DateTime ToDateTime(Object val)
        {
            if (val == null)
                return DateTime.MinValue;

            DateTime dt;
            return DateTime.TryParse(val.ToString(), out dt) ? dt : DateTime.MinValue;
        }

        public static DateTime ToDateTime(Object val, string alternateVal)
        {
            if (val == null)
                return DateTime.MinValue;

            DateTime dt;
            return DateTime.TryParse(val.ToString(), out dt) ? dt : Convert.ToDateTime(alternateVal);
        }

        public static DateTime ToDateTime(Object val, DateTime alternateVal)
        {
            if (val == null)
                return DateTime.MinValue;

            DateTime dt;
            return DateTime.TryParse(val.ToString(), out dt) ? dt : alternateVal;
        }

        public static string ToStr(Object val, string defaultValue = "")
        {
            return val == null ? defaultValue : val.ToString();
        }

        public static int ToInt(Object val, int alternateVal)
        {
            int i;
            return int.TryParse(ToStr(val), out i) ? i : alternateVal;
        }

        public static int ToInt(Object val)
        {
            int i;
            return int.TryParse(ToStr(val), out i) ? i : 0;
        }

        public static Int16 ToInt16(Object val, Int16 alternateVal)
        {
            Int16 i;
            return Int16.TryParse(ToStr(val), out i) ? i : alternateVal;
        }

        public static Int16 ToInt16(Object val)
        {
            Int16 i;
            return Int16.TryParse(ToStr(val), out i) ? i : Convert.ToInt16(0);
        }

        public static Int32 ToInt32(Object val, Int32 alternateVal)
        {
            Int32 i;
            return Int32.TryParse(ToStr(val), out i) ? i : alternateVal;
        }

        public static Int32 ToConvertInt32(Object val)
        {
            Int32 i;
            return Int32.TryParse(ToStr(val), out i) ? i : 0;
        }

        public static Int64 ToInt64(Object val, Int64 alternateVal)
        {
            Int64 i;
            return Int64.TryParse(ToStr(val), out i) ? i : alternateVal;
        }

        public static Int64 ToInt64(Object val)
        {
            Int64 i;
            return Int64.TryParse(ToStr(val), out i) ? i : Convert.ToInt64(0);
        }

        public static Double ToDouble(Object val)
        {
            Double dbl;
            return Double.TryParse(ToStr(val), out dbl) ? dbl : 0.0;
        }

        public static float Tofloat(Object val)
        {
            float fl;
            return float.TryParse(ToStr(val), out fl) ? fl : (float)0.0;
        }

        public static Decimal ToDecimal(Object val)
        {
            Decimal dec;
            return Decimal.TryParse(ToStr(val), out dec) ? dec : (Decimal)0.0;
        }

        public static Boolean ToBool(Object val)
        {
            bool b;
            return bool.TryParse(ToStr(val), out b) ? b : false;
        }

        public static Boolean ToBool(Object val, bool alternateVal)
        {
            bool b;
            return bool.TryParse(ToStr(val), out b) ? b : alternateVal;
        }

        public static byte ToByte(Object val, byte alternateVal)
        {
            byte i;
            return byte.TryParse(ToStr(val), out i) ? i : alternateVal;
        }

        public static byte ToByte(Object val)
        {
            byte i;
            return byte.TryParse(ToStr(val), out i) ? i : (byte)(0);
        }

        #endregion

        #region Null Values

        public static int IsNullInt(Object val)
        {
            if (val == null || val.ToString() == DBNull.Value.ToString())
                return 0;
            else
                return Convert.ToInt32(val);
        }

        public static long IsNullLong(Object val)
        {
            if (val == null || val.ToString() == DBNull.Value.ToString())
                return 0;
            else
                return Convert.ToInt64(val);
        }

        public static Object IsNull(Object val, Object defaultVal)
        {
            if (val == null || val.ToString() == DBNull.Value.ToString())
                return defaultVal;
            else
                return val;
        }

        public static string IsNullStr(Object val)
        {
            if (val == null || val.ToString() == DBNull.Value.ToString())
                return string.Empty;
            else
                return val.ToString();
        }

        public static string IsNullStr(Object val, string defaultVal)
        {
            if (val == null || val.ToString() == DBNull.Value.ToString())
                return defaultVal;
            else
                return val.ToString();
        }

        public static bool IsNullBool(Object val, Boolean defaultVal)
        {
            if (val == null || val.ToString() == DBNull.Value.ToString())
                return defaultVal;
            else
                return Convert.ToBoolean(val);
        }

        public static bool IsNullBool(Object val)
        {
            if (val == null || val.ToString() == DBNull.Value.ToString())
                return false;
            else
                return Convert.ToBoolean(val);
        }

        public static DateTime IsNullDate(Object val, DateTime defaultVal)
        {
            if (val == null || val.ToString() == DBNull.Value.ToString())
                return defaultVal;
            else
                return Convert.ToDateTime(val);
        }

        public static DateTime? IsNullDate(Object val)
        {
            if (val == null || val.ToString() == DBNull.Value.ToString())
                return null;
            else
                return Convert.ToDateTime(val);
        }

        public static decimal IsNullDec(Object val)
        {
            if (val == null || val.ToString() == DBNull.Value.ToString())
                return 0;
            else
                return Convert.ToDecimal(val);
        }

        #endregion

        #region Strings

        public static string TrimText(string text, int length)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            if (text.Length < length)
                return text;

            return text.Substring(0, length) + "...";
        }

        
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion

        #region Streams

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        #endregion



    }
}