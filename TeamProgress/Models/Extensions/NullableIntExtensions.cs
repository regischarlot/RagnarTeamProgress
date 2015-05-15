namespace ADM
{
    public static class NullableIntExtensions
    {
        public static int ToInt32(this int? value)
        {
            return value == null ? -1 : (int) value;
        }

        public static string ToString(this int? value)
        {
            return value == null ? string.Empty : ((int)value).ToString();
        }

        public static bool IsNull(this int? value)
        {
            return value == null;
        }
    }
}
