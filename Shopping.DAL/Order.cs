using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CardId { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime ReceiptDate { get; set; }

        public virtual CreditCard Card { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
