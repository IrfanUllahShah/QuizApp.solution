using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class Student
    {
        public Student()
        {
            SetExams = new HashSet<SetExam>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Username { get; set; } = null!;

        public virtual ICollection<SetExam> SetExams { get; set; }
    }
}
