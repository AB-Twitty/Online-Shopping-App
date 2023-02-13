using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class CreditCard
    {
        public CreditCard()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CardNumber { get; set; } = null!;
        public string Provider { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
