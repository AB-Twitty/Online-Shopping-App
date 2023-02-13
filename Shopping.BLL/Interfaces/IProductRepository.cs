using Microsoft.AspNetCore.Http;
using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.ImageVM;
using Shopping.VM.ProductVM;

namespace Shopping.BLL.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        public Task<ResponseModel<ProductInfoVM>> AddProductImages(RequestModel<ImageAddVM> request);
        public Task<ResponseModel<ProductInfoVM>> AddProduct(RequestModel<ProductAddVM> request);
        public Task<ResponseModel<ProductInfoVM>> ProductInfo(RequestModel<int> request);
        public Task<ResponseModel<ProductInfoVM>> ProductsList();
        public Task<ResponseModel<bool>> DeleteProduct(RequestModel<int> request);
        public Task<ResponseModel<bool>> RestoreProduct(RequestModel<int> request);
        public Task<ResponseModel<bool>> DeleteProductImage(RequestModel<ImageVM> request);
        public Task<ResponseModel<ProductInfoVM>> UpdateProduct(RequestModel<ProductInfoVM> request);
        public Task<double> GetPrice(int productId);
    }
}
