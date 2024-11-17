using ContactsControl.Data;
using ContactsControl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ContactsControl.Repositorie
{
	public class UserRepositorie : IUserRepositorie
	{
		private readonly BContext _context;

        public object TempData => throw new System.NotImplementedException();

        public UserRepositorie(BContext bContext) 
		{
			_context = bContext;
		}
		// method: add user on table
		public UsersModel ToAdd(UsersModel user)
		{
			user.RegistrationDate = DateTime.Now;
			// saved in DB
			_context.Add(user);
			_context.SaveChanges();
			return user;
		}
        
		public UsersModel ListForId(int id)
        {
            return _context.DB_Users.FirstOrDefault(c => c.Id == id);
        }

        public List<UsersModel> AllSearch()
		{
			return _context.DB_Users.ToList();
		}

        public UsersModel Edit(UsersModel user)
        {
            UsersModel DBUser = this.ListForId(user.Id);

            if (DBUser == null) throw new Exception("Error exception: There was an error updating while editing the data");

			DBUser.Name = user.Name;
			DBUser.Email = user.Email;
			DBUser.Login = user.Login;
			DBUser.Profile = user.Profile;
			DBUser.ProfileUpdateDate = DateTime.Now;

            _context.DB_Users.Update(DBUser);
			_context.SaveChanges();

			return DBUser;
        }

		public bool Delete(int id)
		{
			UsersModel DBUser = this.ListForId(id);

			if (DBUser == null) throw new Exception($"Error: User with ID {id} not found for deletion");

			try
			{
				// Remove o contato encontrado
				_context.DB_Users.Remove(DBUser);
				_context.SaveChanges();
				return true; // Indica sucesso na remoção
			}
			catch (Exception ex)
			{
				throw new Exception("Error while deleting the user: " + ex.Message);
			}
		}
	}
}