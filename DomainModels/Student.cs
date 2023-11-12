using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class Student
    {
        public Student()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Image { get; set; }
        public string Username { get; set; } = null!;

        public virtual ICollection<Mark> Marks { get; set; }
    }
}
