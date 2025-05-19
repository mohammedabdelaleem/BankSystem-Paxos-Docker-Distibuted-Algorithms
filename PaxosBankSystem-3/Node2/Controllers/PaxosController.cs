using Microsoft.AspNetCore.Mvc;
using Node2.Models.Paxos;

namespace Node2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaxosController : ControllerBase
{
	private readonly ILogger<PaxosController> _logger;

	public PaxosController(ILogger<PaxosController> logger)
	{
		_logger = logger;
	}

	[HttpPost("prepare")]
	public IActionResult Prepare([FromBody] PaxosProposal proposal)
	{
		_logger.LogInformation($"Node received proposal: {proposal.ProposalId}");

		// Randomly accept/reject for simulation. You can replace this logic.
		var accepted = new Random().Next(0, 2) == 1;

		return Ok(new PaxosResponse
		{
			ProposalId = proposal.ProposalId,
			Accepted = accepted,
			NodeId = Environment.GetEnvironmentVariable("NODE_ID") ?? "Unknown"
		});
	}
}
