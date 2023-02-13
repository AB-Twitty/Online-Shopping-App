using System;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public partial class Admin
    {
        public int AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
