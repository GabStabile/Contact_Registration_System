using ContactsControl.DB;
using ContactsControl.Models;

namespace ContactsControl.Repositorie
{
	public class ContactRepositorie : IContactRepositorie
	{
		private readonly BContext b_context;
		public ContactRepositorie(BContext bContext) 
		{
			b_context = bContext;
		}
		// method: add contact on table
		public ContactsModel ToAdd(ContactsModel contact)
		{
			// saved in DB
			b_context.Add(contact);
			b_context.SaveChanges();
			return contact;
		}
	}
}
