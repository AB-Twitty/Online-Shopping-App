using Microsoft.AspNetCore.Mvc;
using Shopping.BLL.Interfaces;
using Shopping.VM;
using Shopping.VM.ImageVM;
using Shopping.VM.ProductVM;
using Shopping.VM.AccountVM;
using Shopping.API.Authorization;
using Shopping.API.Helpers;
using Shopping.BLL;
using Microsoft.Extensions.Options;

namespace Shopping.API.Controllers
{
    public class ProductController : APIBaseController
    {
        [IsDeleted]
        public ProductController(IRepositoryWrapper repo, IJwtUtils utils, IOptions<AppSettings> app) : base(repo,utils,app) { }

        [HttpPost("AddImages")]
        [Authorize(Role.Trader)]
        public async Task<ResponseModel<ProductInfoVM>> AddProductImages(ImageAddVM imageVM)
        {
            return await _repo.Product.AddProductImages(new RequestModel<ImageAddVM>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = imageVM
            });
        }

        [HttpPost("AddProduct")]
        [Authorize(Role.Trader)]
        public async Task<ResponseModel<ProductInfoVM>> AddProduct([FromForm]ProductAddVM productVM)
        {
            return await _repo.Product.AddProduct(new RequestModel<ProductAddVM>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = productVM
            });
        }

        [HttpGet("ProductInfo")]
        [Authorize(Role.Admin, Role.Trader, Role.Customer)]
        public async Task<ResponseModel<ProductInfoVM>> ProductInfo(int productId)
        {
            return await _repo.Product.ProductInfo(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = productId
            });
        }

        [HttpGet("ProductsList")]
        [Authorize(Role.Admin, Role.Customer)]
        public async Task<ResponseModel<ProductInfoVM>> ProductsList()
        {
            return await _repo.Product.ProductsList();
        }

        [HttpGet("DeleteProduct")]
        [Authorize(Role.Admin, Role.Trader)]
        public async Task<ResponseModel<bool>> DeleteProduct(int productId)
        {
            return await _repo.Product.DeleteProduct(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = productId
            });
        }

        [HttpGet("RestoreProduct")]
        [Authorize(Role.Trader)]
        public async Task<ResponseModel<bool>> RestoreProduct(int productId)
        {
            return await _repo.Product.RestoreProduct(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = productId
            });
        }

        [HttpDelete("DeleteImage")]
        [Authorize(Role.Trader)]
        public async Task<ResponseModel<bool>> DeleteProductImage(ImageVM imageVM)
        {
            return await _repo.Product.DeleteProductImage(new RequestModel<ImageVM>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = imageVM
            });
        }

        [HttpPut("UpdateProduct")]
        [Authorize(Role.Trader)]
        public async Task<ResponseModel<ProductInfoVM>> UpdateProduct([FromForm]ProductInfoVM productVM)
        {
            return await _repo.Product.UpdateProduct(new RequestModel<ProductInfoVM>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = productVM
            });
        }
    }
}
