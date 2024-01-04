using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Clio.Demo.Extension
{
    public static class StringEx
	{

		public static bool EqualsNoCase(this string self, string other)
		{
			return string.Equals(self, other, StringComparison.CurrentCultureIgnoreCase);
		}
		//public static bool ContainsNoCase(this string self, string other)
		//{
		//	return self != null && self.Contains(other, StringComparison.CurrentCultureIgnoreCase);
		//}
		public static T ToEnum<T>(this string value, T defaultEnum) where T : struct
		{
			T parsedEnum;
			return Enum.TryParse<T>(value, true, out parsedEnum) ? parsedEnum : defaultEnum;
		}

        public static bool IsValidEmail(this string email)
        {
            var regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|edu)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static string NoLongerThan(this string str, int max)
        {
            return str is null ? string.Empty : str.Substring(0, Math.Min(str.Length - 1, max));
        }

        public static bool ContainsAny(this string haystack, params string[] needles)
        {
            foreach (var needle in needles)
            {
                if (haystack.Contains(needle))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsAny(this IEnumerable<string> existing, IEnumerable<string> desired)
        {
            if (existing.IsEmpty())
            {
                return false;
            }
            return existing.Any(x => desired.Any(y => y == x));
        }

        public static List<int> AllIndexesOf(this string str, string data)
        {
            List<int> all = new List<int>();

            if (str.IsNotEmpty())
            {
                for (int index = 0; ; index += data.Length)
                {
                    if (-1 == (index = str.IndexOf(data, index)))
                    {
                        break;
                    }
                    all.Add(index);
                }
            }
            return all;
        }

        public static string AllAfter(this string data, char delimiter)
        {
            if (data is null)
            {
                return data;
            }
            int start = data.LastIndexOf(delimiter);
            if (start < 0 || start > data.Length - 1)
            {
                return data;
            }
            return $"{data.Substring(start + 1, data.Length - start - 1)}";
        }

        public static string BeforeLast(this string data, char delimiter)
        {
            if (data is null)
            {
                return data;
            }
            int end = data.LastIndexOf(delimiter);

            if (end < 0)
            {
                return data;
            }
            return $"{data.Substring(0, end)}";
        }

        public static string Encode(this string data)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        }

        public static string Decode(this string data)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(data));
        }

    }
}
