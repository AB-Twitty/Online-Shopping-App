using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class Trader
    {
        public Trader()
        {
            Products = new HashSet<Product>();
        }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
    }
}
