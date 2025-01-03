﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.ViewModels.Home
{
    public class LoginVM
    {

        [DisplayName("Username: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Username {  get; set; }


        [DisplayName("Password: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Password { get; set;}
    }
}
