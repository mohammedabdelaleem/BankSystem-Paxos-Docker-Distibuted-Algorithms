namespace Node3.Services;

public interface ITransactionService
{
	Task<TransactionResult> InitiateTransferAsync(int fromUserId, int toUserId, double amount);
	Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(int userId);
}
