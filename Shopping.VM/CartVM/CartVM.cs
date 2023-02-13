using Shopping.VM.CartItemVM;

namespace Shopping.VM.CartVM
{
    public class CartVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public IList<CartItemInfoVM>? Items { get; set; }
    }
}
