using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinjamanOnline.Data;
using PinjamanOnline.Models;

namespace PinjamanOnline.Controllers
{
	[Route("Auth")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserDbContext _context;
		public UserController(UserDbContext context) => _context = context;

		[HttpGet]
		public async Task<IEnumerable<User>> Get()
			=> await _context.Users.ToListAsync();

		[HttpGet("id")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(int id)
		{
			var user = await _context.Users.FindAsync(id);
			return user == null ? NotFound() : Ok(user);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> Create(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Update(int id, User user)
		{
			if (id != user.Id) return BadRequest();

			_context.Entry(user).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			var userToDelete = await _context.Users.FindAsync(id);
			if (userToDelete == null) return NotFound();
			_context.Users.Remove(userToDelete);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
