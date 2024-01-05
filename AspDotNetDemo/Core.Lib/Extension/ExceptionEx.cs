using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Clio.Demo.Core.Lib.Extension
{
    public static class ExceptionEx
    {
        public static string Short(this Exception ex, int stackDepth = 5)
        {
            string exShort = string.Empty;
            try
            {
                if (null != ex)
                {
                    StringBuilder sb = new StringBuilder();
                    string[] stack = ex.StackTrace?.Split(new string[] { "at " }, StringSplitOptions.None);

                    if (null != stack)
                    {
                        int count = 0;
                        foreach (string segment in stack)
                        {
                            if (++count > stackDepth)
                            {
                                break;
                            }
                            sb.Append($"{(count > 1 ? "-> " : "")} {Regex.Replace(segment.Substring(segment.LastIndexOfAny(new char[] { '\\' }) + 1), @"(\f\n?|\r?\n)+", "")}");
                        }
                    }
                    exShort = $"{ex.GetType().Name} '{ex.Message}' {sb}";
                }
            }
            catch
            {
            }
            return exShort;
        }
    }
}
