namespace Common
{
    public static class StringExtension
    {
        static StringExtension()
        {
        }

        public static string Fix(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            text = text.Trim();

            if (text == string.Empty)
            {
                return string.Empty;
            }

            while (text.Contains("  "))
            {
                text =
                    text.Replace("  ", " ");
            }

            return text;
        }
    }
}
