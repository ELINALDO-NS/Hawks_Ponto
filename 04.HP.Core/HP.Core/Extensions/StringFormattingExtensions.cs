using System.Globalization;


namespace HP.Core.Extentions
{
    public static class StringFormattingExtensions
    {
        public static string ToTitleCase(this string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        public static string ToUpperSafe(this string? input)
        {
            return input?.ToUpper() ?? string.Empty;
        }
    }
}
