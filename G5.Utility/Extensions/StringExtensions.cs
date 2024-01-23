namespace G5.Utility.Extensions
{
    public static class StringExtensions
    {
        public static string ToUpperIgnoreNull(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            else
                return text.ToUpper();
        }
    }
}
