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

		public IActionResult Delete(int id) {
			try
			{
				bool deleteUser = _userRepositorie.Delete(id);
				if (deleteUser)
				{
					TempData["SuccessMessage"] = "User deleted sucessfully";
				}
				else
				{
					TempData["ErrorMessage"] = "Unable to delete your user";
				}
				return RedirectToAction("Index");
			}
			catch (System.Exception error)
			{
				TempData["ErrorMessage"] = $"Error ocorred: unable your to delete your user\n Details: {error.Message}";
				return RedirectToAction("Index");
			}
		}

		public IActionResult DeleteConfirmation(int id)
		{
			UsersModel users = _userRepositorie.ListForId(id);
			return View(users);
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