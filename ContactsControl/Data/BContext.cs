using ContactsControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsControl.Data
{
	public class BContext : DbContext
	{
		// constructor for tests
        public BContext() {}

        // Connection on DB
        public BContext(DbContextOptions<BContext> options) : base(options) { }

        // Contacts
        public virtual DbSet<ContactsModel> DB_Contacts { get; set; }

        // Users
        public virtual DbSet<UsersModel> DB_Users { get; set; }
	}
}