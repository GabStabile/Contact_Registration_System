using ContactsControl.DB;
using ContactsControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ContactsControl.Repositorie
{
	public class ContactRepositorie : IContactRepositorie
	{
		private readonly BContext _context;

        public object TempData => throw new System.NotImplementedException();

        public ContactRepositorie(BContext bContext) 
		{
			_context = bContext;
		}
		// method: add contact on table
		public ContactsModel ToAdd(ContactsModel contact)
		{
			// saved in DB
			_context.Add(contact);
			_context.SaveChanges();
			return contact;
		}

        public ContactsModel ListForId(int id)
        {
            return _context.DB_Contacts.FirstOrDefault(c => c.Id == id);
        }

        public List<ContactsModel> AllSearch()
		{
			return _context.DB_Contacts.ToList();
		}

        public ContactsModel Edit(ContactsModel contact)
        {
            ContactsModel contactDB = this.ListForId(contact.Id);

            if (contactDB == null) throw new Exception("Error exception: There was an error updating while editing the data");

			contactDB.Name = contact.Name;
            contactDB.Email = contact.Email;
            contactDB.Phone = contact.Phone;

            _context.DB_Contacts.Update(contactDB);
			_context.SaveChanges();

			return contactDB;
        }
    }
}