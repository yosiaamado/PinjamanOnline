using System.ComponentModel.DataAnnotations;

namespace PinjamanOnline.Models
{
	public class Loan
	{
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
		public Decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DueDate { get; set; }
        public Status Status { get; set; }
    }
    public enum Status
    {
        OnHold, Active, Decline, Late
    }
}
