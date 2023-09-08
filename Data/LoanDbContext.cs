using Microsoft.EntityFrameworkCore;
using PinjamanOnline.Models;

namespace PinjamanOnline.Data
{
	public class LoanDbContext : DbContext
	{
		public LoanDbContext(DbContextOptions<LoanDbContext> options)
			: base(options)
		{

		}
		public DbSet<Loan> Loans { get; set; }
	}
}
