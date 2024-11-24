using ContactsControl.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsControl.Models
{
	public class LoginModel
	{
        [Required(ErrorMessage = "Enter user login")]
        public string Login {  get; set; }

        [Required(ErrorMessage = "Enter user password")]
        public string Password { get; set; }
    }
}