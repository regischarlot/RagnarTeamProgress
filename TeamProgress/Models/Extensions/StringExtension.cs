using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ADM
{
    public static class StringExtensions
    {
        /// <summary>
        ///     ObjectExtension
        ///        
        ///     Author              Company                             Date            Description
        ///     ------------------- ----------------------------------- --------------- -----------------------------------------
        ///     Regis Charlot       Intelligent Medical Objects, Inc.   March 13, 2012  Inital Creation
        ///     
        /// </summary>


        /// <summary>
        ///     Repeat()
        ///     
        /// </summary>
        public static string Repeat(this char chatToRepeat, int repeat)
        {
            return new string(chatToRepeat, repeat);
        }

        /// <summary>
        ///     Repeat()
        ///     
        /// </summary>
        public static string Repeat(this string stringToRepeat, int repeat)
        {
            var builder = new StringBuilder(repeat*stringToRepeat.Length);
            for (int i = 0; i < repeat; i++)
            {
                builder.Append(stringToRepeat);
            }
            return builder.ToString();
        }

        /// <summary>
        ///     ToSQL()
        ///     
        /// </summary>
        public static string ToSQL(this string value)
        {
            return value.Replace("'", "''");
        }

        /// <summary>
        ///     ReadFile()
        ///     
        /// </summary>
        public static string ReadFile(this string filename)
        {
            StreamReader streamReader = new StreamReader(filename);
            string s = streamReader.ReadToEnd();
            streamReader.Close();
            return s;
        }

        /// <summary>
        ///     WriteFile()
        ///     
        /// </summary>
        public static bool WriteFile(this string value, string filename)
        {
            using (StreamWriter outfile = new StreamWriter(filename))
            {
                outfile.Write(value);
                outfile.Close();
            }
            return true;
        }

        /// <summary>
        ///     EncodeXMLSpecialCharacters
        ///     
        /// </summary>
        public static string EncodeXMLSpecialCharacters(this string value)
        {
            string s = value;
            if (!string.IsNullOrEmpty(s))
            {
                // First convert the special Microsoft Word smart quotes
                s = s.Replace('\u2018', '\'');
                s = s.Replace('\u2019', '\'');
                s = s.Replace('\u201b', '\'');
                s = s.Replace('\u2032', '\'');
                s = s.Replace('\u201c', '"');
                s = s.Replace('\u201d', '"');
                s = s.Replace('\u201e', '"');
                s = s.Replace('\u201f', '"');
                s = s.Replace('\u2033', '"');

                s = s.Replace("&", "&amp;");
                s = s.Replace(">", "&gt;");
                s = s.Replace("<", "&lt;");
                s = s.Replace("\"", "&quot;");
            }
            return s;
        }

        /// <summary>
        ///     DecodeXMLSpecialCharacters
        ///     
        /// </summary>
        public static string DecodeXMLSpecialCharacters(this string value)
        {
            string s = value;
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace("&gt;", ">");
                s = s.Replace("&lt;", "<");
                s = s.Replace("&quot;", "\"");
                s = s.Replace("&amp;", "&");
            }
            return s;
        }

        /// <summary>
        ///     GetItem()
        ///     
        /// </summary>
        public static string GetItem(this string value, char cSeparator, int index)
        {
            string result = string.Empty;
            List<String> lst = new List<String>(value.Split(cSeparator));
            if (index < lst.Count)
                result = lst[index];
            return result.Trim();
        }

        /// <summary>
        ///     GetPart()
        ///     
        /// </summary>
        public static string GetPart(this string value, string part)
        {
            string s = String.Format("{0}=\"", new object[] { part });
            string result = value;
            int n = result.IndexOf(s, StringComparison.Ordinal);
            if (n != -1)
            {
                result = result.Remove(0, n + s.Length);
                n = result.IndexOf("\"", StringComparison.Ordinal);
                if (n != -1)
                    result = result.Remove(n);
            }
            else
                result = string.Empty;
            return result;
        }

        /// <summary>
        ///     JSONToDateTime()
        ///     
        /// </summary>
        public static DateTime? ToJDate(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return null;
                value = value.Replace('"', ' ').Trim();
                String[] parts = value.Split(new char[] { 'T' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 1)
                    return DateTime.ParseExact(parts[0], "yyyy-M-dd", CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        /// <summary>
        ///     StartOfLineAt()
        ///     
        /// </summary>
        public static int StartOfLineAt(this string value, int currentPos)
        {
            int L = currentPos;
            while (L > 0 && value[L] != '\n' && value[L] != '\r')
                L--;
            return L;
        }

        /// <summary>
        ///     SubString()
        ///     
        /// </summary>
        public static string SubString(this string value, int start, int length)
        {
            return value.Substring(start, length);
        }

        /// <summary>
        ///     ToInt32()
        ///     
        /// </summary>
        public static int ToInt32(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch 
            {
                return 0;
            }
        }


        /// <summary>
        ///     ToDouble()
        ///     
        /// </summary>
        public static double ToDouble(this string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0.0;
            }
        }

        
    }
}
