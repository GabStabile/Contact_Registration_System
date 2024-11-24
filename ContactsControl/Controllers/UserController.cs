using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.AspNetCore.Mvc;
using System;
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

		public IActionResult Edit(int id) {
			UsersModel user = _userRepositorie.ListForId(id);
			return View(user);
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

		// method 'Create User'
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

        // method 'Edit User'
        [HttpPost]
        public IActionResult Edit(UsersWithoutPasswordModel userWithoutPassword)
        {
            try
            {
                UsersModel user = null;

                if (ModelState.IsValid)
                {
                    user = new UsersModel()
					{
						Id = userWithoutPassword.Id,
						Name = userWithoutPassword.Name,
						Email = userWithoutPassword.Email,
						Login = userWithoutPassword.Login,
						Profile = userWithoutPassword.Profile
					};

                    user = this._userRepositorie.Edit(user);
                    TempData["SuccessMessage"] = "User edited successfully";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception error)
            {
                TempData["MessageError"] = $"An error occurred!\n Details: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}