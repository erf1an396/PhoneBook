using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class RegisterViewModel
    {

        [Display(Name = "Username")]
        [Required(ErrorMessage = "{0} Enter the  ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} Enter the ")]
        [Display(Name = "FullName")]
        public string Password { get; set; }


        [Required(ErrorMessage = "{0} Enter the ")]
        [Display(Name = "Password ")]
        [MinLength(6, ErrorMessage = "{0} should more than 5 character")]
        public string FullName { get; set; }

    }
}
