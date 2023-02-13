using Microsoft.AspNetCore.Http;

namespace Shopping.VM.ProductVM
{
    public class ProductAddVM : ProductInfoVM
    {
        public IList<IFormFile> imageFiles { get; set; } = null!;
    }
}
