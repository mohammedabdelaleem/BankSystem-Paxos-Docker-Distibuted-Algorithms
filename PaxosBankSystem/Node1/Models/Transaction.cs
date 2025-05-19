namespace Node1.Models;

public class Transaction
{
	public int Id { get; set; }
	public int FromAccountId { get; set; }
	public int ToAccountId { get; set; }
	public decimal Amount { get; set; }
	public DateTime Timestamp { get; set; } = DateTime.Now;

	public Account FromAccount { get; set; }
	public Account ToAccount { get; set; }
}

