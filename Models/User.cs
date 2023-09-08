using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace PinjamanOnline.Models
{
	public class User
	{
		public int Id { get; set; }
		[Required]
        public String Email { get; set; }
		[Required]
		public String Password { get; set; }
		[Required]
		public String PhoneNumber { get; set; }
		[Required]
		public String Salary { get; set; }
		[Required]
		public Level Level { get; set; }

    }
	public enum Level
	{
		None, Low, Medium, High, Priority
	}
}
