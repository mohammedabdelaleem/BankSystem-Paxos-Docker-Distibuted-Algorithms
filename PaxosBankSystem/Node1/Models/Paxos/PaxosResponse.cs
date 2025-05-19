namespace Node1.Models.Paxos;

public class PaxosResponse
{
	public bool Accepted { get; set; }
	public string ProposalId { get; set; }
	public string Message { get; set; } // Optional for additional info
}

