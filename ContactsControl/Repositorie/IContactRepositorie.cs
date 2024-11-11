using ContactsControl.Models;
using System.Collections.Generic;

namespace ContactsControl.Repositorie
{
	public interface IContactRepositorie
	{
        object TempData { get; }

        ContactsModel ListForId(int id);
		List<ContactsModel> AllSearch();
		ContactsModel ToAdd(ContactsModel contact);
		ContactsModel Edit(ContactsModel contacts);
	}
}