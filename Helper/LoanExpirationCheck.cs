using Microsoft.EntityFrameworkCore;
using PinjamanOnline.Data;
using PinjamanOnline.Models;

namespace PinjamanOnline.Helper
{
	public class LoanExpirationCheck : BackgroundService
	{
		private readonly IServiceProvider _provider;

		public LoanExpirationCheck(IServiceProvider provider) => _provider = provider;

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				using (var scope = _provider.CreateScope())
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<LoanDbContext>();

					var expiredLoans = await dbContext.Loans
						.Where(l => l.DueDate < DateTime.Now && l.Status == Status.Active)
						.ToListAsync();

					foreach (var loan in expiredLoans)
					{
						loan.Status = Status.Late;
					}

					await dbContext.SaveChangesAsync();
				}

				await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
			}
		}
	}

}
