using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Enter the username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter the password")]
        [MinLength(6, ErrorMessage = "Should more than 5 character")]
        public string Password { get; set; }
    }
}
