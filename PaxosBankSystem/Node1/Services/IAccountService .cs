
using System.Linq.Expressions;

namespace Node1.Services;

public interface IAccountService
{
	Task<List<AccountInfoResponseDTO>> GetAllAsync(Expression<Func<Account,bool>> filter=null);

	Task<Account> GetAsync(int userId);
	Task<Account> AddAsync(int userId);
	Task<bool> TransferAsync(int fromUserId, int toUserId, decimal amount);
	Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(int userId);


}
