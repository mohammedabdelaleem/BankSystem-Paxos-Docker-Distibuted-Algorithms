using Microsoft.AspNetCore.Mvc;
using Node1.Models.Paxos;
using Microsoft.Extensions.Logging;
using System;

namespace Node1.Controllers
{
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

			// Randomly accept/reject for simulation. You can replace this logic with your Paxos algorithm.
			var accepted = new Random().Next(0, 2) == 1;

			// You can fetch the NodeId from an environment variable, configuration, or set it manually
			var nodeId = Environment.GetEnvironmentVariable("NODE_ID") ?? "Unknown";

			// Log the decision for the Paxos protocol
			_logger.LogInformation($"Proposal {proposal.ProposalId} accepted by Node {nodeId}: {accepted}");

			// Return the response with proposal status
			return Ok(new PaxosResponse
			{
				ProposalId = proposal.ProposalId,
				Accepted = accepted,
				NodeId = nodeId
			});
		}
	}
}
