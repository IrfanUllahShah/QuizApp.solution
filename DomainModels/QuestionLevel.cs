using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class QuestionLevel
    {
        public QuestionLevel()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string? QuestionLevel1 { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
