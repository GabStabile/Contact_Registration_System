using ContactsControl.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ContactsControl.Helper
{
    public class Session : ISession
    {
        private readonly IHttpContextAccessor _httpContext;

        public Session(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void CreateUserSession(UsersModel user)
        {
            string value = JsonConvert.SerializeObject(user);

            _httpContext.HttpContext.Session.SetString("userSessionLoggedIn" , value);
        }

        public void RemoveUserSession(UsersModel user)
        {
            _httpContext.HttpContext.Session.Remove("userSessionLoggedIn");
        }

        public UsersModel SearchUserSession()
        {
            // Garante que HttpContext não seja nulo
            if (_httpContext.HttpContext == null) return null;

            string userSession = _httpContext.HttpContext.Session.GetString("userSessionLoggedIn");

            if (string.IsNullOrEmpty(userSession)) return null;

            return JsonConvert.DeserializeObject<UsersModel>(userSession);
        }
    }
}