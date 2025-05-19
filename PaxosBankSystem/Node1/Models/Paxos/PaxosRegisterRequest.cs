namespace Node1.Models.Paxos;

public class PaxosRegisterRequest
{
	public string ProposalId { get; set; }
	public string Username { get; set; }
	public string Password { get; set; } // already hashed!
}