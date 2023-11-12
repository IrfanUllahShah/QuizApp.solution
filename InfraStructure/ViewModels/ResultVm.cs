using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InfraStructure.ViewModels
{
    public class ResultVm
    {
        public string Name { get; set; } = null!;
        public int? Marks { get; set; }

        [Display(Name = "Total Marks")]
        public int? TotalMarks { get; set; }
        [Display(Name = "Percentage % ")]
        public float? percentage { get; set; }


    }
}
