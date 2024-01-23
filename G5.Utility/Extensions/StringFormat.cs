using System;
using System.Text.RegularExpressions;

namespace G5.Utility.Extensions
{
    public class StringFormat
    {
        public static string ConvertSentenceFormat(string text)
        {
            if (String.IsNullOrEmpty(text))
                return "";

            var lowerCase = text.ToLower();
            var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
            var result = r.Replace(lowerCase, s => s.Value.ToUpper());
            return result;
        }
    }
}
