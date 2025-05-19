namespace Node1.Models;

public class TransactionRequest
{
	public string SenderAccount { get; set; }  // The account that is sending money
	public string ReceiverAccount { get; set; }  // The account that will receive money
	public decimal Amount { get; set; }  // The amount to be transferred
	public string TransactionType { get; set; }  // e.g., "Deposit", "Withdrawal", "Transfer"
	public DateTime TransactionDate { get; set; }  // Date and time of the transaction
}
