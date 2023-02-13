using Shopping.DAL;
using Shopping.BLL.Interfaces;
using Shopping.VM;
using Shopping.VM.CartItemVM;
using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Shopping.BLL.Repository_Classes
{
    internal class CartItemRepository : RepositoryBase<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context,repo,mapper) { }

        public async Task<double> GetTotalPrice(CartItemInfoVM itemVM)
        {
            if (await _repo.Product.FindByCondition(x => x.Id == itemVM.productId).AnyAsync())
            {
                CartItem item = new CartItem();
                item = _mapper.Map<CartItem>(itemVM);
                item.CreationDate = item.LastModifiedDate = DateTime.Now;
                await Create(item);
                double price = await _repo.Product.GetPrice(itemVM.productId);
                return price * itemVM.quantity;
            }
            throw new Exception();
        }

        public async Task<double> DeleteItem(CartItemInfoVM itemVM)
        {
            if (await _repo.CartItem.FindByCondition(x => x.Id == itemVM.id && x.CartId == itemVM.cartId).AnyAsync())
            {
                Delete(_mapper.Map<CartItem>(itemVM));
                return await GetTotalPrice(itemVM);
            }
            throw new Exception();
        }
    }
}
