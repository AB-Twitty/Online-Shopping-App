using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            CartItems = new HashSet<CartItem>();
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public decimal Total { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
