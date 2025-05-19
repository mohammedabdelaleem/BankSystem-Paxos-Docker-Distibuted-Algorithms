namespace Node1.Models.Request;

public class PrepareRequest
{
	public ProposalId ProposalId { get; set; }
}

public class ProposalId
{
	public int ProposalNumber { get; set; }
	public int ProposerId { get; set; }
}