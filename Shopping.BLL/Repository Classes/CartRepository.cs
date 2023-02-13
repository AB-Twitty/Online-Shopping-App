using Shopping.DAL;
using Shopping.BLL.Interfaces;
using Shopping.VM;
using Shopping.VM.CartItemVM;
using Shopping.VM.CartVM;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Shopping.BLL.Repository_Classes
{
    internal class CartRepository : RepositoryBase<ShoppingCart>, ICartRepository
    {
        public CartRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context,repo,mapper) { }

        public async Task<ResponseModel<CartVM>> GetShoppingCart(int cartId)
        {
            try
            {
                if (await FindByCondition(x => x.Id==cartId).AnyAsync())
                {
                    return new ResponseModel<CartVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Data = _mapper.Map<CartVM>(await FindByCondition(x => x.Id == cartId).Include(x => x.CartItems).FirstAsync()),
                        Message = "The request has seccedded"
                    };
                }
                return new ResponseModel<CartVM>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Not Found",
                };
            }
            catch
            {
                return new ResponseModel<CartVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<bool>> AddItemToCart(CartItemInfoVM itemVM)
        {
            try
            {
                if (await FindByCondition(x => x.Id == itemVM.cartId).AnyAsync())
                {
                    ShoppingCart cart = await FindByCondition(x => x.Id==itemVM.cartId).FirstAsync();
                    cart.Total += (decimal)await _repo.CartItem.GetTotalPrice(itemVM);
                    Update(cart);
                    await _repo.Save();
                    return new ResponseModel<bool>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Data = true,
                        Message = "Item added successfully to cart"
                    };
                }
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Data = false,
                    Message = "Bad Request"
                };
            }
            catch
            {
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<bool>> DeleteItemFromCart(CartItemInfoVM itemVM)
        {
            try
            {
                if (await FindByCondition(x => x.Id==itemVM.id).AnyAsync())
                {
                    ShoppingCart cart = await FindByCondition(x => x.Id == itemVM.cartId).FirstAsync();
                    cart.Total -= (decimal)await _repo.CartItem.DeleteItem(itemVM);
                    Update(cart);
                    await _repo.Save();
                    return new ResponseModel<bool>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Data = true,
                        Message = "Item deleted successfully from cart"
                    };
                }
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Data = false,
                    Message = "Bad Request"
                };
            }
            catch
            {
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "Internal Server Error"
                };
            }
        }
    }
}
