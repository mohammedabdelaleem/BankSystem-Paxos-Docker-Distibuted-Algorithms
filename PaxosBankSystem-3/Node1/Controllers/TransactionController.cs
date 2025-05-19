using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Node1.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
	private readonly ITransactionService _transactionService;

	public TransactionController(ITransactionService transactionService)
	{
		_transactionService = transactionService;
	}



	[HttpPost("transfer")]
	public async Task<IActionResult> Transfer([FromBody] TransferRequestDTO request)
	{
		if (request == null || request.Amount <= 0 || request.ToUserId <= 0)
		{
			return BadRequest(new { error = "Invalid transfer request data." });
		}

		var fromUserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (!int.TryParse(fromUserIdStr, out var fromUserId))
		{
			return Unauthorized();
		}

		try
		{
			var result = await _transactionService.InitiateTransferAsync(fromUserId, request.ToUserId, request.Amount);

			return result.Success
				? Ok(new { message = "Transfer initiated", transactionId = result.TransactionId })
				: BadRequest(new { error = result.ErrorMessage });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
		}
	}



	[HttpGet("history")]
	public async Task<IActionResult> GetHistory()
	{
		var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (!int.TryParse(userIdStr, out var userId))
		{
			return Unauthorized();
		}

		var history = await _transactionService.GetTransactionHistoryAsync(userId);
		return Ok(history);
	}
}
