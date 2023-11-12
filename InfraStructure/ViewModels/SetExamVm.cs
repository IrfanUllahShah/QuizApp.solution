using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.ViewModels
{
    public class SetExamVm
    {
        [DisplayName("Quiz Id")]
        public int Examid { get; set; }
        [DisplayName("Created Date")]
        public DateTime? ExamDate { get; set; }

        [DisplayName("Quiz Name")]
        public string ExamName { get; set; } = null!;

        [DisplayName("Room code")]
        public string? RoomCode { get; set; }
        [DisplayName("Number of questions")]
        public int NoOfQuestions { get; set; }
        //[DisplayName("Marks")]
        //public int? StdScore { get; set; }
        [DisplayName("Category")]
        public string? Category { get; set; }
    }
        public class SetExamCreateVm
    {
       
        [DisplayName("Quiz Name")]
        [Required(ErrorMessage = "Exam Name is required")]
        public string ExamName { get; set; } = null!;
        [DisplayName("Number of questions")]
        [Required(ErrorMessage = "field is required.")]
        public int NoOfQuestions { get; set; }

        [DisplayName("Room code")]
        public string? RoomCode { get; set; }

        [DisplayName("Created Date")]
        public DateTime? ExamDate { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "field is required.")]
        public int? CategoryId { get; set; }
        public IEnumerable<CategoryVm> Categories { get; set; }
    }
}
