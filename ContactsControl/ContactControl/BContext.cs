using ContactsControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsControl.DB
{
	public class BContext : DbContext
	{
		// Connection on DB
		public BContext(DbContextOptions<BContext> options) : base(options) { }

		// Table contact
		public DbSet<ContactsModel> DB_Contacts { get; set; }
	}
}
