using ContactsControl.Migrations;
using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactsModel contact)
        {
			_contactRepositorie.ToAdd(contact);
            return RedirectToAction("Index");
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
                TempData["MensagemErro"] = $"An error occurred! Details: {error.Message}";
                return RedirectToAction("Index");

            }
        }
    }
}