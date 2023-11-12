using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class Question
    {
        public int QId { get; set; }
        public string QText { get; set; } = null!;
        public string Ans1 { get; set; } = null!;
        public string Ans2 { get; set; } = null!;
        public string Ans3 { get; set; } = null!;
        public string Ans4 { get; set; } = null!;
        public string CorrectAns { get; set; } = null!;
        public int? CategoryId { get; set; }
        public int? QuestionLevelId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual QuestionLevel? QuestionLevel { get; set; }
    }
}
