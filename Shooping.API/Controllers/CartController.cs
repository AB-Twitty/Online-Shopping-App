using Shopping.BLL.Interfaces;
using Shopping.VM;
using Shopping.VM.CartItemVM;
using Shopping.VM.CartVM;
using Microsoft.AspNetCore.Mvc;
using Shopping.VM.AccountVM;
using Shopping.API.Authorization;
using Shopping.BLL;
using Microsoft.Extensions.Options;
using Shopping.API.Helpers;

namespace Shopping.API.Controllers
{
    public class CartController : APIBaseController
    {
        public CartController(IRepositoryWrapper repo, IJwtUtils utils, IOptions<AppSettings> app) : base(repo,utils,app) { }

        [HttpPost("AddItemToCart")]
        [Authorize(Role.Customer)]
        public async Task<ResponseModel<bool>> AddItemToCart([FromForm]CartItemInfoVM itemVM)
        {
            return await _repo.Cart.AddItemToCart(itemVM);
        }

        [HttpDelete("DeleteItemFromCart")]
        [Authorize(Role.Customer)]
        public async Task<ResponseModel<bool>> DeleteItemFromCart(CartItemInfoVM itemVM)
        {
            return await _repo.Cart.DeleteItemFromCart(itemVM);
        }

        [HttpGet("ShoppingCart")]
        [Authorize(Role.Admin, Role.Customer)]
        public async Task<ResponseModel<CartVM>> GetShoppingCart(int customerId)
        {
            return await _repo.Cart.GetShoppingCart(customerId);
        }
    }
}
