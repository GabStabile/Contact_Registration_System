using ContactsControl.Data;
using ContactsControl.Models;
using Microsoft.EntityFrameworkCore;
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
			_context.DB_Contacts.Add(contact);
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
            ContactsModel DBContact = this.ListForId(contact.Id);

            if (DBContact == null) throw new Exception("Error exception: There was an error updating while editing the data");

			DBContact.Name = contact.Name;
			DBContact.Email = contact.Email;
			DBContact.Phone = contact.Phone;

            _context.DB_Contacts.Update(DBContact);
			_context.SaveChanges();

			return DBContact;
        }

		public bool Delete(int id)
		{
			ContactsModel DBContact = this.ListForId(id);

			if (DBContact == null) throw new Exception($"Error: Contact with ID {id} not found for deletion");

			try
			{
				// Remove o contato encontrado
				_context.DB_Contacts.Remove(DBContact);
				_context.SaveChanges();
				return true; // Indica sucesso na remoção
			}
			catch (Exception ex)
			{
				throw new Exception("Error while deleting the contact: " + ex.Message);
			}
		}
	}
}