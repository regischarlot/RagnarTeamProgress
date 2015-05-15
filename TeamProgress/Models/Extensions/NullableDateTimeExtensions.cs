using System;

namespace ADM
{
    public static class NullableDateTimeExtensions
    {
        public static DateTime ToDateTime(this DateTime? value)
        {
            return value == null ? DateTime.Today : (DateTime) value;
        }

        public static bool IsNull(this DateTime? value)
        {
            return value == null;
        }

        public static string ToJString(this DateTime? value)
        {
            return value.IsNull() ? string.Empty : string.Format("{0:M/dd/yyyy}", new object[] { value });
        }
    
        public static string ToJSONShortDate(this DateTime? value)
        {
            return value.IsNull() ? string.Empty : string.Format("{0:yyyy-MM-dd}", new object[] { value });
        }
    }
}
