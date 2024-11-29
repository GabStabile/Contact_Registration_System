using ContactsControl.Models;

namespace ContactsControl.Helper
{
    public interface ISession
    {
        void CreateUserSession(UsersModel user);
        void RemoveUserSession(UsersModel user);
        UsersModel SearchUserSession();
    }
}
