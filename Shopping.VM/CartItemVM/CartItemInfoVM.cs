using Shopping.VM.CartItemVM.Validations;

namespace Shopping.VM.CartItemVM
{
    public class CartItemInfoVM
    {
        public int id { get; set; }
        public int cartId { get; set; }
        public int productId { get; set; }
        [CheckQuantityExist]
        public int quantity { get; set; }
    }
}
