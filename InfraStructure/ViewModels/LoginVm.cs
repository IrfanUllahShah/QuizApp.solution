using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.ViewModels
{
    public class LoginVm
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage ="Username is required.")]
        public string username { get; set; } = null!;

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string password { get; set; } = null!;
    }
}
