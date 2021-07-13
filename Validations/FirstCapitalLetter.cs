using System.ComponentModel.DataAnnotations;

namespace back_end.Validations
{
    public class FirstCapitalLetter: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty((value.ToString())))
            {
                return ValidationResult.Success;
            }

            var firstLetter = value.ToString()?[0].ToString();

            if (firstLetter != firstLetter?.ToUpper())
            {
                return new ValidationResult("First letter must be capital");
            }
            
            return ValidationResult.Success;
        }
    }
}