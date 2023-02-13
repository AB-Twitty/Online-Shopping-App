using System.ComponentModel.DataAnnotations;
using Shopping.DAL;

namespace Shopping.VM.ContactVM.Validations
{
    internal class CheckExistanceAttribute : ValidationAttribute
    {
        private string CheckForProperity { get; set; }

        public CheckExistanceAttribute(string checkForProperity)
        {
            CheckForProperity = checkForProperity;
        }

        protected override ValidationResult IsValid(object properityValue, ValidationContext validationContext)
        {
            Object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            Object checkForValue = type.GetProperty(CheckForProperity).GetValue(instance, null);
            ShoppingDBContext _context = new ShoppingDBContext();
            if (_context.CustomerContacts.Any(x => x.Contact==properityValue && (x.Id.ToString()==checkForValue.ToString() || x.Id == 0)))
            {
                return new ValidationResult($"This Contact '{properityValue}' already exists.");
            }
            return ValidationResult.Success;
        }
    }
}
