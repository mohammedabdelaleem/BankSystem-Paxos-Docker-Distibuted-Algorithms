using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Node1.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountController : ControllerBase
{
	private readonly IAccountService _accountService;

	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[HttpPost("add")]
	public async Task<IActionResult> Add()
	{
		var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (!int.TryParse(userIdStr, out var userId))
		{
			return Unauthorized();
		}

		try
		{
			var account = await _accountService.AddAsync(userId);
			return Ok(account);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[HttpGet("info")]
	public async Task<IActionResult> GetAccountInfo()
	{
		var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (!int.TryParse(userIdStr, out var userId))
		{
			return Unauthorized();
		}

		var account = await _accountService.GetAsync(userId);

		return account != null
			? Ok(new { account.Id, account.Balance, Username = account.User.UserName })
			: NotFound(new { message = $"Account Not Found For This User {userIdStr}." });
	}

	[HttpGet("all")]
	public async Task<IActionResult> GetAll()
	{
		var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (!int.TryParse(userIdStr, out var userId))
		{
			return Unauthorized();
		}

		var accounts = await _accountService.GetAllAsync(a=>a.UserId == userId);
		return accounts != null && accounts.Any()
			? Ok(accounts)
			: NotFound(new { message = "No Accounts Here." });
	}
}
