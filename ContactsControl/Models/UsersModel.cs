using ContactsControl.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsControl.Models
{
	public class UsersModel
	{
        public int Id { get; set; }

		[Required(ErrorMessage = "Enter user name")]
        public string Name { get; set; }

		[Required(ErrorMessage = "Enter user login")]
		public string Login { get; set; } // email, CPF ou codigo pra login

		[Required(ErrorMessage = "Enter user password")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Enter user e-mail")]
		[EmailAddress(ErrorMessage = "E-mail information is not valid")]
		public string Email { get; set; }
        public EnumProfile Profile { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ProfileUpdateDate { get; set; }
    }
}