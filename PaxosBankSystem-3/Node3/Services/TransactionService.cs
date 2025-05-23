
namespace Node3.Services;
public class TransactionService : ITransactionService
{
	private readonly AppDbContext _context;

	public TransactionService(AppDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(int userId)
	{
		return await _context.Transactions
			.Where(t => t.FromUserId == userId || t.ToUserId == userId)
			.OrderByDescending(t => t.Timestamp)
			.ToListAsync();
	}


	public async Task<TransactionResult> InitiateTransferAsync(int fromUserId, int toUserId, double amount)
	{
		var proposal = new PaxosProposal
		{
			FromUserId = fromUserId,
			ToUserId = toUserId,
			Amount = amount
		};

		var nodes = new[] {
			"http://Node3:5000",
			"http://node2:5001",
			"http://node3:5002"
		};

		int acceptCount = 0;
		foreach (var node in nodes)
		{
			using var http = new HttpClient();
			var response = await http.PostAsJsonAsync($"{node}/api/paxos/prepare", proposal);

			if (response.IsSuccessStatusCode)
			{
				var paxosResponse = await response.Content.ReadFromJsonAsync<PaxosResponse>();
				if (paxosResponse?.Accepted == true)
					acceptCount++;
			}
		}

		if (acceptCount >= 2) // majority accepted
		{
			// Fetch the first account for both users
			var fromAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == fromUserId);
			var toAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == toUserId);

			if (fromAccount == null || toAccount == null)
			{
				return new TransactionResult { Success = false, ErrorMessage = "Account not found." };
			}

			if (fromAccount.Balance < (decimal)amount)
			{
				return new TransactionResult { Success = false, ErrorMessage = "Insufficient balance." };
			}

			fromAccount.Balance -= (decimal)amount;
			toAccount.Balance += (decimal)amount;

			// Optional: Save transaction log
			var transaction = new Transaction
			{
				FromUserId = fromUserId,
				ToUserId = toUserId,
				Amount = (decimal)amount,
				Timestamp = DateTime.UtcNow
			};

			_context.Add(transaction);
			await _context.SaveChangesAsync();

			return new TransactionResult
			{
				Success = true,
				TransactionId = transaction.Id
			};
		}

		return new TransactionResult { Success = false, ErrorMessage = "Consensus not reached." };
	}
}

