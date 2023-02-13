using Microsoft.AspNetCore.Http;
using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.ImageVM;

namespace Shopping.BLL.Interfaces
{
    public interface IImageRepository : IRepositoryBase<ProductImage>
    {
        public Task<int> AddProductImages(IList<IFormFile> images, int productId);
        public Task<int> DeleteProductImage(ImageVM imageVM);
    }
}
