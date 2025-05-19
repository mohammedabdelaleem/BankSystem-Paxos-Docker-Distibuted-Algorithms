namespace Node1.Services;

public interface ITransactionService
{
	Task<TransactionResult> InitiateTransferAsync(int fromAccountId, int toAccountId, double amount);
	Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(int userId);
}












//namespace Node1.Services;

//public interface ITransactionService
//{
//	Task<TransactionResult> InitiateTransferAsync(int fromUserId, int toUserId, double amount);
//	Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(int userId);
//}