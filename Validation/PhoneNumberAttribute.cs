using System.ComponentModel.DataAnnotations;
using PhoneNumbers;

namespace AutodijeloviDemic.Validation
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        private readonly string _defaultRegion;

        public PhoneNumberAttribute(string defaultRegion = "BA")
        {
            _defaultRegion = defaultRegion;
            ErrorMessage = "The phone number is invalid.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success; // Dozvoli prazne brojeve ako nije Required
            }

            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var phoneNumber = phoneNumberUtil.Parse(value.ToString(), _defaultRegion);
                if (phoneNumberUtil.IsValidNumber(phoneNumber))
                {
                    return ValidationResult.Success; // Broj je validan
                }
            }
            catch (NumberParseException)
            {
                // Invalid format
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
