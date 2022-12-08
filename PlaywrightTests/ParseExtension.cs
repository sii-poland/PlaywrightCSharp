using System.Globalization;

namespace PlaywrightTests
{
    public static class ParseExtension
    {
        public static double ParseEuroToDouble(this string stringValue)
        {
            stringValue = stringValue.Split(' ')[0];
            var parsedValue = stringValue.Replace("€", "").Replace(".", ",");
            return double.Parse(parsedValue,
                NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol);
        }
    }
}