namespace Node1.Models.Paxos;

public class PaxosLoginRequest
{
	public string ProposalId { get; set; }
	public string Username { get; set; }
	public string Password { get; set; } // you may hash it here if necessary
}
