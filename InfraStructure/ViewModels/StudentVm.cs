using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InfraStructure.ViewModels
{
    public class StudentVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Image { get; set; } 
        public IFormFile ImageFile { get; set; }

    }

    public class StudentCreateVm
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "FullName is required.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = null!;

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;

        public string Image { get; set; }

      //  [Required(ErrorMessage = "Image is required.")]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }
       
    }

    public class StudentUpdateVm
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "FullName is required.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = null!;

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Image is required.")]
        [DisplayName("Upload File")]
        public string Image { get; set; } 
       // public IFormFile ImageFile { get; set; }
    }

}
