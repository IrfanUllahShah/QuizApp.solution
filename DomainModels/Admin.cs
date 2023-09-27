using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class Admin
    {
        public Admin()
        {
            Categories = new HashSet<Category>();
        }

        public int Id { get; set; }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Category> Categories { get; set; }
    }
}
