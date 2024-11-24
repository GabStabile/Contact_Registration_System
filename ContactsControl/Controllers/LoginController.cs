using ContactsControl.Data;
using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContactsControl.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUserRepositorie _userRepositorie;

        public LoginController(IUserRepositorie userRepositorie)
        {
            _userRepositorie = userRepositorie ?? throw new ArgumentNullException(nameof(userRepositorie));
        }

        public IActionResult Index()
		{
			return View();
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