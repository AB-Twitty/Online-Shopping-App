using Microsoft.AspNetCore.Http;

namespace Shopping.VM.ImageVM
{
    public class ImageVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
