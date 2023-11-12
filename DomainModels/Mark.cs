using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class Mark
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? ExamId { get; set; }
        public int? Marks { get; set; }
        public string? ExamCode { get; set; }

        public virtual SetExam? Exam { get; set; }
        public virtual Student? Student { get; set; }
    }
}
