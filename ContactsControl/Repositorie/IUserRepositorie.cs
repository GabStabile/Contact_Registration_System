using ContactsControl.Models;
using System.Collections.Generic;

namespace ContactsControl.Repositorie
{
	public interface IUserRepositorie
	{
		UsersModel SearchLogin(string login);
        object TempData { get; }
        UsersModel ListForId(int id);
		List<UsersModel> AllSearch();
		UsersModel ToAdd(UsersModel users);
		UsersModel Edit(UsersModel users);
		bool Delete(int id);
	}
}