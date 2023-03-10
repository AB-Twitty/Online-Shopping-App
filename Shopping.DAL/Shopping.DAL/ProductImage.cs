using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
