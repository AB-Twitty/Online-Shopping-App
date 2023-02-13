using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class Country
    {
        public Country()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
