using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class Category
    {
        public Category()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? AdminId { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
