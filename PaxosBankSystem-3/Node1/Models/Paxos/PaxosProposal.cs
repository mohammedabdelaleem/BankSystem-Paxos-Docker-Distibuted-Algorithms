namespace Node1.Models.Paxos;

public class PaxosProposal
{
	public Guid ProposalId { get; set; } = Guid.NewGuid();
	public int FromUserId { get; set; }
	public int ToUserId { get; set; }
	public double Amount { get; set; }
}
