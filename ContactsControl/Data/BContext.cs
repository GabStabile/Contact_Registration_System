using ContactsControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsControl.Data
{
	public class BContext : DbContext
	{
		// Connection on DB
		public BContext(DbContextOptions<BContext> options) : base(options) { }
		// Contacts
		public DbSet<ContactsModel> DB_Contacts { get; set; }
		// Users
		public DbSet<UsersModel> DB_Users { get; set; }
	}
}
