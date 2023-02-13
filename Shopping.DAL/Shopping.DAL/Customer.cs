using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class Customer
    {
        public Customer()
        {
            CreditCards = new HashSet<CreditCard>();
            CustomerContacts = new HashSet<CustomerContact>();
            Orders = new HashSet<Order>();
        }

        public int AccountId { get; set; }
        public int CartId { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ShoppingCart Cart { get; set; } = null!;
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
