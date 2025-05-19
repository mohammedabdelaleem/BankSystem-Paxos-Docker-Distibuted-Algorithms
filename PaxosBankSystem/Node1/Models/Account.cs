namespace Node1.Models;

public class Account
{
	public int Id { get; set; }
	public decimal Balance { get; set; }

	public int UserId { get; set; }
	public ApplicationUser User { get; set; }
}
