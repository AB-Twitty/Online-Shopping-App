using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int CountryId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string NationalId { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AccountType { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;
        public virtual Admin? Admin { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Trader? Trader { get; set; }
    }
}
