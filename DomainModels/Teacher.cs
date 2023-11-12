using System;
using System.Collections.Generic;

namespace DomainModels
{
    public partial class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Image { get; set; }
        public string? Username { get; set; }
    }
}
