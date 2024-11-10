using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Edit()
        {
            return View();
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
    }
}
