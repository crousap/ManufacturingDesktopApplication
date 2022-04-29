using System.Globalization;
using System.Windows.Controls;

namespace DesktopApplication.Classes
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((string)value)
                ? new ValidationResult(false, "Обязательное поле.")
                : ValidationResult.ValidResult;
        }
    }
}
