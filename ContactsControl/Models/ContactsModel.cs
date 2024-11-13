using System.ComponentModel.DataAnnotations;

namespace ContactsControl.Models
{
	public class ContactsModel
	{
        public int Id { get; set; }

		[Required(ErrorMessage = "Enter name by contact")] // campo obrigatorio
		public string Name { get; set; }

		[Required(ErrorMessage = "Enter e-mail by contact")]
		[EmailAddress(ErrorMessage = "E-mail information is not valid")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Enter contact phone number")]
		[Phone(ErrorMessage = "Phone number infortamion is not valid")]
		public string Phone { get; set; }
    }
}