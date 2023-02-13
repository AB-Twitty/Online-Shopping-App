using Shopping.DAL;
using Shopping.BLL.Interfaces;
using Shopping.VM;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Shopping.VM.ImageVM;

namespace Shopping.BLL.Repository_Classes
{
    internal class ImageRepository : RepositoryBase<ProductImage>, IImageRepository
    {
        public ImageRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context,repo,mapper) { }

        private string UploadedFile(IFormFile ImageFile, int productId)
        {
            string uniqueFileName;
            var currentExtension = Path.GetExtension(ImageFile.FileName).ToLower().Trim();
            var extensions = new List<string>() { ".jpeg", ".png", ".jpg" };
            if (!extensions.Contains(currentExtension)) //handle this return 
                return "Invalid File";
            string uploadFolder = $"C:\\Users\\bobof\\Desktop\\ShoppingApp\\Attachments\\Product Images\\Product No.{productId}";
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);
            uniqueFileName = "ProductImages" + "_" + DateTime.Now.ToString("yyyy_MM_dd_hhmmss") + "_" + ImageFile.FileName;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public async Task<int> AddProductImages(IList<IFormFile> images, int productId)
        {
            try
            {
                foreach (var image in images)
                {
                    ProductImage productImage = new ProductImage();
                    productImage.ImageUrl = UploadedFile(image, productId);
                    productImage.ProductId = productId;
                    await _repo.Image.Create(productImage);
                }
                return (int)HttpStatusCode.OK;
            }
            catch
            {
                return (int)HttpStatusCode.InternalServerError;
            }
        }

        public async Task<int> DeleteProductImage(ImageVM imageVM)
        {
            try
            {
                if (await FindByCondition(x => x.Id==imageVM.Id).AnyAsync())
                {
                    Delete(await FindByCondition(x => x.Id == imageVM.Id).FirstAsync());
                    string uploadFolder = $"C:\\Users\\bobof\\Desktop\\ShoppingApp\\Attachments\\Product Images\\Product No.{imageVM.ProductId}";
                    var imagePath = Path.Combine(uploadFolder, imageVM.ImageUrl);
                    System.IO.File.Delete(imagePath);
                    return (int)HttpStatusCode.OK;
                }
                return (int)HttpStatusCode.NotFound;
            }
            catch
            {
                return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
