using DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InfraStructure.ViewModels
{
    public class CategoryVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class CategoryCreateVm
    {
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;
        [Display(Name = "Category Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;
    }

    public class CategoryUpdateVm
    {
        [Display(Name = "Category Id")]
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;
        [Display(Name = "Category Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;
    }

    public class CategoryQuestionsVm
    {
        public IEnumerable<Question> Questions { get; set; }
    }
}
