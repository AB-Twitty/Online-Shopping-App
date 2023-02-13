using Microsoft.AspNetCore.Http;

namespace Shopping.VM.ImageVM
{
    public class ImageAddVM
    {
        public IList<IFormFile> imageFiles { get; set; }
        public int productId { get; set; }

        public ImageAddVM (IList<IFormFile> imageFiles, int productId)
        {
            this.imageFiles = imageFiles;
            this.productId = productId;
        }
    }
}
