namespace Node3.Models.Paxos;

public class PaxosResponse
{
	public Guid ProposalId { get; set; }
	public bool Accepted { get; set; }
	public string NodeId { get; set; }
}
