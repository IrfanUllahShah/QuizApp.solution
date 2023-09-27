using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class SetExam
    {
        public int Examid { get; set; }
        public DateTime? ExamDate { get; set; }
        public int? ExamFkStd { get; set; }
        public string ExamName { get; set; } = null!;
        public int? StdScore { get; set; }

        public virtual Student? ExamFkStdNavigation { get; set; }
    }
}
