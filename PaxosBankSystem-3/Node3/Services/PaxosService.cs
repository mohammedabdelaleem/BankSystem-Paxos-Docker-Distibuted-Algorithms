using Node3.Models;

namespace Node3.Services;

public class PaxosService
{
	private readonly HttpClient _httpClient;
	private readonly List<string> _paxosNodes;

	public PaxosService(HttpClient httpClient, IConfiguration configuration)
	{
		_httpClient = httpClient;
		_paxosNodes = configuration.GetSection("PaxosNodes").Get<List<string>>();  // Load Paxos nodes from configuration
	}

	public async Task<string> SendTransactionAsync(TransactionRequest request)
	{
		var acceptedNodes = new List<string>();
		var rejectedNodes = new List<string>();
		var failedNodes = new List<string>(); // Track nodes that failed due to an error (network, timeout, etc.)

		// Send request to all Paxos nodes
		foreach (var node in _paxosNodes)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{node}/api/transaction", request);
				if (response.IsSuccessStatusCode)
				{
					acceptedNodes.Add(node); // Node accepted the transaction
				}
				else
				{
					rejectedNodes.Add(node); // Node rejected the transaction
				}
			}
			catch (Exception ex)
			{
				failedNodes.Add(node); // Node failed due to an exception (e.g., unreachable)
				Console.WriteLine($"Error contacting node {node}: {ex.Message}");
			}
		}

		// Log the result for debugging and analysis
		LogTransactionResult(acceptedNodes, rejectedNodes, failedNodes);

		// Determine the majority consensus
		if (acceptedNodes.Count >= 2)  // Majority rule: at least 2 out of 3 nodes need to accept
		{
			return GenerateConsensusMessage(acceptedNodes, rejectedNodes, failedNodes, true);
		}
		else
		{
			return GenerateConsensusMessage(acceptedNodes, rejectedNodes, failedNodes, false);
		}
	}

	private void LogTransactionResult(List<string> acceptedNodes, List<string> rejectedNodes, List<string> failedNodes)
	{
		Console.WriteLine("Transaction Result:");
		Console.WriteLine($"Accepted by: {string.Join(", ", acceptedNodes)}");
		Console.WriteLine($"Rejected by: {string.Join(", ", rejectedNodes)}");
		Console.WriteLine($"Failed to contact: {string.Join(", ", failedNodes)}");
	}

	private string GenerateConsensusMessage(List<string> acceptedNodes, List<string> rejectedNodes, List<string> failedNodes, bool consensusAccepted)
	{
		string message = consensusAccepted
			? "Transaction Accepted by Majority of Nodes"
			: "Transaction Rejected by Majority of Nodes";

		// Append detailed node information
		message += "\n\nDetailed Breakdown:\n";

		foreach (var node in _paxosNodes)
		{
			if (acceptedNodes.Contains(node))
				message += $"{node}: Accepted\n";
			else if (rejectedNodes.Contains(node))
				message += $"{node}: Rejected\n";
			else if (failedNodes.Contains(node))
				message += $"{node}: Failed to Contact\n";
		}

		return message;
	}
}
