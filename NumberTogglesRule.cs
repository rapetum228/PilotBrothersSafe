using System;
using System.Globalization;
using System.Windows.Controls;

namespace PilotBrothersSafe
{
    public class NumberTogglesRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public NumberTogglesRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int numberToggles = 0;

            try
            {
                if (((string)value).Length > 0)
                    numberToggles = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((numberToggles < Min) || (numberToggles > Max))
            {
                return new ValidationResult(false,
                  $"Введённое число допускает диапазон: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
