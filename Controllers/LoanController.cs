using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinjamanOnline.Data;
using PinjamanOnline.Models;
using System.Transactions;

namespace PinjamanOnline.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class LoanController : ControllerBase
	{
        private readonly LoanDbContext _context;
		private readonly UserDbContext _userContext;
		public LoanController(LoanDbContext context, UserDbContext userContext)
		{
			_context = context;
			_userContext = userContext;
		}

		[HttpGet]
		public async Task<IEnumerable<Loan>> Get()
			=> await _context.Loans.ToListAsync();

		[HttpGet("id")]
		[ProducesResponseType(typeof(Loan), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(int id)
		{
			var loan = await _context.Loans.FindAsync(id);
			return loan == null? NotFound() : Ok(loan);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> CreateLoan(Loan loan)
		{
			var userId = await _context.Loans.FindAsync(loan.UserId);
			var user = await _userContext.Users.FindAsync(userId);

			decimal limitAmount = new Dictionary<Level, decimal>
			{ 
				{ Level.Low, 2000000 },
				{ Level.Medium, 5000000 },
				{ Level.High, 10000000 },
				{ Level.Priority, 100000000 }
			}.GetValueOrDefault(user.Level, 0);

			loan.Status = loan.Amount > limitAmount
				? (limitAmount == 0 ? Status.OnHold : Status.Decline)
				: Status.OnHold;

			await _context.Loans.AddAsync(loan);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateLoanStatus(int id, Loan loan)
		{
			if (id != loan.Id) return BadRequest();

			var loanData = await _context.Loans.FindAsync(id);

			if(loanData.Status == Status.Active || loanData.Status == Status.Decline) return BadRequest();

			_context.Entry(loan).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			var loanToDelete = await _context.Loans.FindAsync(id);
			if(loanToDelete == null) return NotFound();
			_context.Loans.Remove(loanToDelete);
			await _context.SaveChangesAsync();

			return NoContent();
		}
    }
}
