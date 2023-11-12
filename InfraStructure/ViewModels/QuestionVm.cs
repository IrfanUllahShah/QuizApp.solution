using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.ViewModels
{

    //question view model
    public class QuestionVm
    {
        [DisplayName("ID")]
        [Required(ErrorMessage = "Required field!")]
        public int QId { get; set; }
        [DisplayName("Question text")]
        [Required(ErrorMessage = "Required field!")]
        public string QText { get; set; } = null!;
        [DisplayName("Option 1")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans1 { get; set; } = null!;
        [DisplayName("Option 2")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans2 { get; set; } = null!;
        [DisplayName("Option 3")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans3 { get; set; } = null!;
        [DisplayName("Option 4")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans4 { get; set; } = null!;
        [DisplayName("Correct option")]
        [Required(ErrorMessage = "Required field!")]
        public string CorrectAns { get; set; } = null!;
        [DisplayName("Category")]
        [Required(ErrorMessage = "Required field!")]
        public string? Category { get; set; }
    }


    //question create view model
    public class QuestionCreateVm
    {
        public QuestionCreateVm()
        {
            Categories = new LinkedList<CategoryVm>();
        }
        [DisplayName("Question text")]
        [Required(ErrorMessage = "Required field!")]
        public string QText { get; set; } = null!;
        [DisplayName("Option 1")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans1 { get; set; } = null!;
        [DisplayName("Option 2")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans2 { get; set; } = null!;
        [DisplayName("Option 3")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans3 { get; set; } = null!;
        [DisplayName("Option 4")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans4 { get; set; } = null!;
        [DisplayName("Correct option")]
        [Required(ErrorMessage = "Required field!")]
        public string CorrectAns { get; set; } = null!;
        [DisplayName("Category")]
        [Required(ErrorMessage = "Required field!")]
        public int? CategoryId { get; set; }
        public IEnumerable<CategoryVm> Categories { get; set; }
    }


    //question update model
    public class QuestionUpdateVm
    {
        public QuestionUpdateVm()
        {
            Categories = new LinkedList<CategoryVm>();
        }

        [DisplayName("ID")]
        public int QId { get; set; }
        [DisplayName("Question text")]
        [Required(ErrorMessage = "Required field!")]
        public string QText { get; set; } = null!;
        [DisplayName("Option 1")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans1 { get; set; } = null!;
        [DisplayName("Option 2")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans2 { get; set; } = null!;
        [DisplayName("Option 3")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans3 { get; set; } = null!;
        [DisplayName("Option 4")]
        [Required(ErrorMessage = "Required field!")]
        public string Ans4 { get; set; } = null!;
        [DisplayName("Correct option")]
        [Required(ErrorMessage = "Required field!")]
        public string CorrectAns { get; set; } = null!;
        [DisplayName("Category")]
        [Required(ErrorMessage = "Required field!")]
        public int? CategoryId { get; set; }
        public IEnumerable<CategoryVm> Categories { get; set; }
    }


}
