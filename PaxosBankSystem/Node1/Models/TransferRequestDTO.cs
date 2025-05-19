using System.ComponentModel.DataAnnotations;

namespace Node1.Models;

public class TransferRequestDTO
{

	[Required]
	[Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
	public double Amount { get; set; }

	[Required]
	[Range(1, int.MaxValue, ErrorMessage = "Invalid recipient user ID.")]
	public int ToAccountId { get; set; }
}