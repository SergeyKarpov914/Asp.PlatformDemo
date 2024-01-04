using System;

namespace Clio.Demo.Extension
{
    public static class DateTimeEx
    {
        public static string ToShortDateString(this DateTime? date)
        {
            if (date.HasValue)
            { 
                return date.Value.ToShortDateString();
            }
            return string.Empty;
        }
    }

    public static class DT
    {
        public static string Date     => DateTime.Now.ToString("yyyy-MMM-dd");
        public static string Time     => DateTime.Now.ToString("HHmmss");
        public static string TimeLong => DateTime.Now.ToString("HH:mm:ss.fff");
    }
}
