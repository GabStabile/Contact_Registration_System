using ContactsControl.Models;
using System.Collections.Generic;

namespace ContactsControl.Repositorie
{
	public interface IContactRepositorie
	{
		List<ContactsModel> AllSearch();
		ContactsModel ToAdd(ContactsModel contact);
	}
}
