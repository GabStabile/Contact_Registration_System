using ContactsControl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ContactsControl.ViewsComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userSession = HttpContext.Session.GetString("userSessionLoggedIn");

            if (string.IsNullOrEmpty(userSession)) return null;

            UsersModel user = JsonConvert.DeserializeObject<UsersModel>(userSession);

            return View(user);
        }
    }
}