using System.Linq;
using System.Windows.Controls;
using System.Globalization;

namespace Encryptor.ViewModel
{
    public class DictionaryKeyValidationRule : ValidationRule
    {
        private DictionaryKeyValidationRuleBindingWrapper _existingValuesWrapper;

        public DictionaryKeyValidationRuleBindingWrapper ExistingValuesWrapper { get => _existingValuesWrapper; set => _existingValuesWrapper = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (ExistingValuesWrapper.ExistingValues.Any(kvp => kvp.RawKey.Trim() == value.ToString().Trim()))
            {
                return new ValidationResult(false, "Key already exists.");
            }

            return ValidationResult.ValidResult;
        }
    }
}