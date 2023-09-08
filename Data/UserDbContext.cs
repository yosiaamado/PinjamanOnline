using Microsoft.EntityFrameworkCore;
using PinjamanOnline.Models;

namespace PinjamanOnline.Data
{
	public class UserDbContext : DbContext
	{
		public UserDbContext(DbContextOptions<UserDbContext> options) 
			: base (options)
		{ 
		
		}
		public DbSet<User> Users { get; set; }
	}
}
