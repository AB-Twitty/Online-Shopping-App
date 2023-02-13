using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual ShoppingCart Cart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
