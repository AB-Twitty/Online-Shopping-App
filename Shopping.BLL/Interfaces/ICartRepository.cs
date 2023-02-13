using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.CartVM;
using Shopping.VM.CartItemVM;

namespace Shopping.BLL.Interfaces
{
    public interface ICartRepository : IRepositoryBase<ShoppingCart>
    {
        public Task<ResponseModel<CartVM>> GetShoppingCart(int customerId);
        public Task<ResponseModel<bool>> AddItemToCart(CartItemInfoVM itemVM);
        public Task<ResponseModel<bool>> DeleteItemFromCart(CartItemInfoVM itemVM);
    }
}
