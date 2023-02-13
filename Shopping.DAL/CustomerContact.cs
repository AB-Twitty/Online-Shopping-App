using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class CustomerContact
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ContactTypeId { get; set; }
        public string Contact { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual ContactType ContactType { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
