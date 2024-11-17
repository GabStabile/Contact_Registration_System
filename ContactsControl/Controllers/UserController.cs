using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContactsControl.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserRepositorie _userRepositorie;

		public UserController(IUserRepositorie userRepositorie)
		{
			_userRepositorie = userRepositorie;
		}


		// gets
		public IActionResult Index()
		{
			List<UsersModel> users = _userRepositorie.AllSearch();
			return View(users);
		}

		public IActionResult Create() {
			return View();
		}

		public IActionResult Edit() {
			return View();
		}

		public IActionResult Delete() {
			return View();
		}

		[HttpPost]
		public IActionResult Create(UsersModel user)
		{
			try
			{
				if (ModelState.IsValid)
				{
					user = _userRepositorie.ToAdd(user);

					TempData["SuccessMessage"] = "User created sucessfully";
					return RedirectToAction("Index");
				}
				return View(user);
			}
			catch (System.Exception error)
			{
				TempData["MessageError"] = $"An error ocurred:\n Details: {error.Message}";
				return RedirectToAction("Idex");
			}
		}
	}
}
