using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class SetExam
    {
        public SetExam()
        {
            Marks = new HashSet<Mark>();
        }

        public int Examid { get; set; }
        public string ExamName { get; set; } = null!;
        public string? RoomCode { get; set; }
        public int NoOfQuestions { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? ExamDate { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
    }
}
