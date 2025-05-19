

using System.Linq.Expressions;

namespace Node1.Services;

public class AccountService : IAccountService
{
	private readonly AppDbContext _context;

	public AccountService(AppDbContext context)
	{
		_context = context;
	}



	public async Task<List<AccountInfoResponseDTO>> GetAllAsync(Expression<Func<Account, bool>> filter = null)
	{
	

		var accounts = await _context.Accounts
			.Include(x=>x.User)
			.Where(filter)
			.Select(a=>  new AccountInfoResponseDTO{
			Id = a.Id,
			UserName = a.User.UserName,
			UserId = a.UserId,
			Balance = a.Balance,
			}).ToListAsync();
			;

		return accounts! ;

	}
	public async Task<Account> GetAsync(int userId)
	{
		var account = await _context.Accounts
			.Include(a => a.User)
			.FirstOrDefaultAsync(a => a.UserId == userId);

		return account == null ? null : account;


	}

	public async Task<Account> AddAsync(int userId)
	{
		var account = new Account
		{
			Balance = 5000m,
			UserId = userId
		};

		_context.Accounts.Add(account);
		await _context.SaveChangesAsync();

		return account;
	}


	public async Task<bool> TransferAsync(int fromUserId, int toUserId, decimal amount)
	{
		if (fromUserId == toUserId)
		{
			throw new Exception("Cannot transfer to yourself.");
		}

		var fromAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == fromUserId);
		var toAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == toUserId);

		if (fromAccount == null || toAccount == null)
		{
			throw new Exception("Account not found.");
		}

		if (fromAccount.Balance < amount)
		{
			throw new Exception("Insufficient balance.");
		}
		
		// If Paxos agreed, perform the transfer in the database
		fromAccount.Balance -= amount;
		toAccount.Balance += amount;

		var transaction = new Transaction
		{
			FromAccountId = fromUserId,
			ToAccountId = toUserId,
			Amount = amount,
			Timestamp = DateTime.UtcNow
		};

		_context.Transactions.Add(transaction);
		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(int userId)
	{
		return await _context.Transactions
			.Where(t => t.FromAccountId == userId || t.ToAccountId == userId)
			.OrderByDescending(t => t.Timestamp)
			.ToListAsync();
	}

}