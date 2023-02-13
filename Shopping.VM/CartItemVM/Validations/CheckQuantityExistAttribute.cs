using System.ComponentModel.DataAnnotations;
using Shopping.DAL;

namespace Shopping.VM.CartItemVM.Validations
{
    internal class CheckQuantityExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object properityValue, ValidationContext validationContext)
        {
            Object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            Object checkForValue = type.GetProperty("productId").GetValue(instance, null).ToString();
            ShoppingDBContext _context = new ShoppingDBContext();
            if (_context.Products.Any(x => x.Id.ToString()==checkForValue))
            {
                int quantity = _context.Products.Where(x => x.Id.ToString()==checkForValue).First().Quantity;
                return new ValidationResult($"Only {quantity} left in stock");
            }
            return ValidationResult.Success;
        }
    }
}
