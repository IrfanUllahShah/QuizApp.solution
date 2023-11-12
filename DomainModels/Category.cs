using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class Category
    {
        public Category()
        {
            Questions = new HashSet<Question>();
            SetExams = new HashSet<SetExam>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<SetExam> SetExams { get; set; }
    }
}
