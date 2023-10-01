namespace Core.Lib.Extension
{
    public static class NumericEx
    {
        public static string Dec2ccy3(this decimal? price)
        {
            return price is null ? "" : price.Value.ToString("C3"); // C2, drop zero fraction
        }

        public static string DecTrim(this decimal? price)
        {
            return price is null ? string.Empty : $"{price.Value:G29}";
        }

        public static string Dec2ccy(this decimal? price)
        {
            return price is null ? string.Empty : $"{price.Value:C2}";
        }

        public static string Dec2string(this decimal? dec, string format = "N4")
        {
            if (dec is null)
            {
                return string.Empty;
            }
            var number = dec.Value;

            if (number % 1 == 0)
            {
                return number.ToString("N0");                                    // drop fraction   
            }
            return number.ToString(format ?? "N4").TrimEnd(new char[] { '0' });  // trim fraction
        }

        public static string Rate2string(this decimal? rate, string onNull = "", string percent = "%")
        {
            return rate is null ? "" : $"{rate.Dec2string(onNull)}{percent}"; // N4 + %
        }
    }
}
