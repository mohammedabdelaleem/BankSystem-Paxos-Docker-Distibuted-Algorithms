using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Node1.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionController : ControllerBase
{
	private readonly ITransactionService _transactionService;
	private readonly IUserService _userService;

	public TransactionController(ITransactionService transactionService, IUserService userService)
	{
		_transactionService = transactionService;
		_userService = userService;
	}



	[HttpPost("transfer")]
	public async Task<IActionResult> Transfer([FromBody] TransferRequestDTO request)
	{
		if (request == null || request.Amount <= 0 || request.ToAccountId <= 0)
		{
			return BadRequest(new { error = "Invalid transfer request data." });
		}


		var fromAccountIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (!int.TryParse(fromAccountIdStr, out var fromAccountId))
		{
			return Unauthorized();
		}

		//if(!await _userService.IsExists(fromAccountId) || !await _userService.IsExists(request.ToAccountId) )
		//	return BadRequest(new { error = "Invalid users Ids" });



		try
		{
			var result = await _transactionService.InitiateTransferAsync(fromAccountId, request.ToAccountId, request.Amount);

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

