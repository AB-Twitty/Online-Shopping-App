using System.ComponentModel.DataAnnotations;

namespace Shopping.VM.AccountVM.Validations
{
    internal class HasSixteenYearsOrMoreAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int age = (int)((DateTime.Today - (DateTime)value).TotalDays / 365);
            if (age >= 16)
                return ValidationResult.Success;
            return new ValidationResult("Must be at least 16 years!");
        }
    }
}
