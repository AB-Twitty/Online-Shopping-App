namespace Shopping.VM.ProductVM
{
    public class ProductInfoVM
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public string desc { get; set; } = null!;
        public decimal price { get; set; }
        public int quantity { get; set; }
        public int? categoryId { get; set; }
        public int traderId { get; set; }
        public IList<ImageVM.ImageVM>? images { get; set; }
    }
}
