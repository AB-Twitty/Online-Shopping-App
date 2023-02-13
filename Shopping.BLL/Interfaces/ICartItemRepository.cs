using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.CartItemVM;

namespace Shopping.BLL.Interfaces
{
    public interface ICartItemRepository : IRepositoryBase<CartItem>
    {
        public Task<double> GetTotalPrice(CartItemInfoVM itemVM);
        public Task<double> DeleteItem(CartItemInfoVM itemVM);
    }
}
