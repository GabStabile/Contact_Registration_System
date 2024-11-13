using ContactsControl.Migrations;
using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace ContactsControl.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepositorie _contactRepositorie;
        public ContactController(IContactRepositorie contact)
        {
            _contactRepositorie = contact;
        }

        // methods gets
        public IActionResult Index()
        {
            List<ContactsModel> contacts = _contactRepositorie.AllSearch();
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            ContactsModel contact = _contactRepositorie.ListForId(id);
            return View(contact);
        }
        public IActionResult DeleteConfirmation(int id)
        {
            ContactsModel contact = _contactRepositorie.ListForId(id);
            return View(contact);
        }
        
        public IActionResult Delete(int id)
        {
            try
            {
				bool deleted = _contactRepositorie.Delete(id);
				if (deleted)
                {
					TempData["SuccessMessage"] = "Contact deleted successfully";
				}
                else
                {
                    TempData["MessageError"] = "Unable to delete your contact";
                }
                return RedirectToAction("Index");
			}
            catch (Exception error)
            {
                TempData["MessageError"] = $"Error ocorred: unable to delete your contact\n Details: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Create(ContactsModel contact)
        {
            try
            {
				if (ModelState.IsValid)
				{
					_contactRepositorie.ToAdd(contact);
					TempData["SuccessMessage"] = "Contact created sucessfully";
					return RedirectToAction("Index");
				}
				return View(contact);
			}
            catch (Exception error)
            {
                TempData["MessageError"] = $"An error ocurred!\n Details: {error.Message}";
                return RedirectToAction("Index");
			}
		}

        [HttpPost]
        public IActionResult Edit(ContactsModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._contactRepositorie.Edit(contact);
                    TempData["SuccessMessage"] = "Contact edited successfully";
                    return RedirectToAction("Index");
                }
                return View("Edit", contact);
            }
            catch (Exception error)
            {
                TempData["MessageError"] = $"An error occurred!\n Details: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}