using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LibraryManager.ViewModels.Home
{
    public class SignUpVM
    {

     
        [DisplayName("Username: ")]
        [Required(ErrorMessage = "Username is Required!")]
        public string Username { get; set; }


        [DisplayName("Password: ")]
        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }


        [DisplayName("First Name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string FirstName { get; set; }


        [DisplayName("Last Name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string LastName { get; set; }
    }
}
