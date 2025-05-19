namespace Node2.Models;

public class Transaction
{
	public int Id { get; set; }
	public int FromUserId { get; set; }
	public int ToUserId { get; set; }
	public decimal Amount { get; set; }
	public DateTime Timestamp { get; set; } = DateTime.Now;

	public ApplicationUser FromUser { get; set; }
	public ApplicationUser ToUser { get; set; }
}

