using System;

namespace ADM
{
    public static class ObjectExtension
    {
        /// <summary>
        ///     ObjectExtension
        ///        
        ///     Author              Company                             Date            Description
        ///     ------------------- ----------------------------------- --------------- -----------------------------------------
        ///     Regis Charlot       Intelligent Medical Objects, Inc.   April 11, 2013  Inital Creation
        ///     
        /// </summary>

        /// <summary>
        ///     ToString
        ///     
        /// </summary>
        public static string ToString(this object value)
        {
            string result = string.Empty;
            if (value.GetType() != typeof (DBNull))
                result = Convert.ToString(value);
            return result;
        }

        /// <summary>
        ///     ToInt32
        ///     
        /// </summary>
        public static int ToInt32(this object value)
        {
            int result = 0;
            if (value.GetType() != typeof(DBNull))
                result = Convert.ToInt32(value);
            return result;
        }

        /// <summary>
        ///     ToInt32
        ///     
        /// </summary>
        public static int? ToNullableInt32(this object value)
        {
            int? result = null;
            if (value.GetType() != typeof(DBNull) && !string.IsNullOrEmpty(value.ToString()))
                result = Convert.ToInt32(value);
            return result;
        }

        /// <summary>
        ///     ToDateTime
        ///     
        /// </summary>
        public static DateTime ToDateTime(this object value)
        {
            DateTime result;
            if (DateTime.TryParse((value ?? "").ToString(), out result))
                return result;
            return new DateTime(0);
        }

        /// <summary>
        ///     ToDateTime
        ///     
        /// </summary>
        public static DateTime? ToNullableDateTime(this object value)
        {
            DateTime result;
            if (DateTime.TryParse((value ?? "").ToString(), out result))
                return result;
            return null;
        }

        /// <summary>
        ///     ToDouble()
        ///     
        /// </summary>
        public static double ToDouble(this object value)
        {
            double result = 0.0;
            if (value.GetType() == typeof(DBNull) || value == null)
                result = 0.0;
            else if (value is bool)
                result = value.ToBool() ? 1 : 0;
            else if (value.IsReal())
                result = Convert.ToDouble(value);
            else if (value.IsNumber())
                result = value.ToInt32();
            else if (!string.IsNullOrEmpty(value.ToString()))
                result = Convert.ToDouble(value.ToString());
            return result;
        }

        /// <summary>
        ///     ToDouble()
        ///     
        /// </summary>
        public static double? ToNullableDouble(this object value)
        {
            double? result;
            if (value.GetType() == typeof(DBNull))
                return null;
            else if (value is bool)
                result = value.ToBool() ? 1 : 0;
            else if (value.IsReal())
                // ReSharper disable CompareOfFloatsByEqualityOperator
                result = Convert.ToDouble(value);
            // ReSharper restore CompareOfFloatsByEqualityOperator
            else if (value.IsNumber())
                result = value.ToInt32();
            else
                result = double.Parse(value.ToString());
            return result;
        }

        /// <summary>
        ///     ToBool()
        ///     
        /// </summary>
        public static bool ToBool(this object value)
        {
            bool result;
            if (value is bool)
                result = (bool)value;
            else if (value.IsNumber())
                result = value.ToInt32() != 0;
            else if (value.IsReal())
                // ReSharper disable CompareOfFloatsByEqualityOperator
                result = value.ToDouble() != 0;
            // ReSharper restore CompareOfFloatsByEqualityOperator
            else
            {
                string s = value.ToString();
                result = (s != string.Empty) && (!s.Equals("no", StringComparison.OrdinalIgnoreCase)) && (!s.Equals("false", StringComparison.OrdinalIgnoreCase));
            }
            return result;
        }

        /// <summary>
        ///     ToBool()
        ///     
        /// </summary>
        public static bool? ToNullableBool(this object value)
        {
            bool? result;
            if (value is bool)
                result = (bool)value;
            else if (value.IsNumber())
                result = value.ToInt32() != 0;
            else if (value.IsReal())
                // ReSharper disable CompareOfFloatsByEqualityOperator
                result = value.ToDouble() != 0;
            // ReSharper restore CompareOfFloatsByEqualityOperator
            else
            {
                string s = value.ToString();
                result = (s != string.Empty) && (!s.Equals("no", StringComparison.OrdinalIgnoreCase)) && (!s.Equals("false", StringComparison.OrdinalIgnoreCase));
            }
            return result;
        }

        /// <summary>
        ///    IsNumber() 
        ///
        /// </summary>
        public static bool IsNumber(this object value)
        {
            if (value is String)
            {
                int num;
                return int.TryParse(value.ToString(), out num);
            }
            else
                return (value is Int16 || value is Int32 || value is Int64 || value is Decimal || value is Single || value is Double || value is Boolean);
        }

        /// <summary>
        ///   IsReal()
        ///     
        /// </summary>
        public static bool IsReal(this object value)
        {
            double num;
            return double.TryParse(value.ToString(), out num);
        }
    }
}
