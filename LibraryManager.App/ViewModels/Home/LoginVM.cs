using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.App.ViewModels.Home
{
    public class LoginVM
    {

        [DisplayName("Username: ")]
        [Required(ErrorMessage = "Username is Required!")]
        public string Username { get; set; }


        [DisplayName("Password: ")]
        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }
    }
}
