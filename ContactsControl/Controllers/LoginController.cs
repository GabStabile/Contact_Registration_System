using ContactsControl.Helper;
using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContactsControl.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUserRepositorie _userRepositorie;
        private readonly ISession _session;

        public LoginController(IUserRepositorie userRepositorie, ISession session)
        {
            _userRepositorie = userRepositorie;
            
            _session = session;
        }

        public IActionResult Index()
		{
            // se o usuario estiver logado, redirecionar para a home. Significa que tem usuario com sessao aberta
            if (_session.SearchUserSession() != null) return RedirectToAction("Index", "Home");

            return View();
		}

        public IActionResult Logout(UsersModel user)
        {
            _session.RemoveUserSession(user);

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
		public IActionResult InSystem(LoginModel loginModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
                    // 'Buscar Login do usuario' existente no DB
                    UsersModel user = _userRepositorie.SearchLogin(loginModel.Login);

					if (user != null)
                    {
                        // condition for valid password
                        if (user.ValidPassword(loginModel.Password))
                        {
                            // method for create session
                            _session.CreateUserSession(user);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MessageError"] = "Incorrect user password. Please try again";
                    }
                    else
                    {
                        TempData["MessageError"] = "Incorrect username and/or password. Please try again";
                    }
                }
                return View("Index");
            }
			catch (Exception error)
			{
				TempData["MessageError"] = $"An error ocorrude!\n Details: {error.Message}";
				return View("Index");
			}
		}
    }
}