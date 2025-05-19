using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
		Console.WriteLine("User Claims: " + string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}")));
		var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		Console.WriteLine($"User ID from token: {userIdStr}");

		if (!int.TryParse(userIdStr, out var userId))
		{
			return Unauthorized("Missing or invalid sub claim.");
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
		Console.WriteLine("User.Identity.IsAuthenticated: " + User.Identity.IsAuthenticated);
		Console.WriteLine("Claims count: " + User.Claims.Count());


		Console.WriteLine("User Claims: " + string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}")));
		var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		Console.WriteLine($"User ID from token: {userIdStr}");

		if (!int.TryParse(userIdStr, out var userId))
		{
			return Unauthorized("Missing or invalid sub claim.");
		}

		var account = await _accountService.GetAsync(userId);
		Console.WriteLine($"Account retrieved: {account?.Id}");

		return account != null
			? Ok(new { account.Id, account.Balance, Username = account.User.UserName })
			: NotFound(new { message = "No Accounts For You , Go To Add One" });
	}

	[HttpGet("all")]
	public async Task<IActionResult> GetAll()
	{
		var accounts = await _accountService.GetAllAsync();
		return accounts != null && accounts.Any()
			? Ok(accounts)
			: NotFound(new { message = "No Accounts Here For This User." });
	}


	[HttpGet("whoami")]
	public IActionResult WhoAmI()
	{
		return Ok(new
		{
			IsAuthenticated = User.Identity?.IsAuthenticated,
			Claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
		});
	}

}