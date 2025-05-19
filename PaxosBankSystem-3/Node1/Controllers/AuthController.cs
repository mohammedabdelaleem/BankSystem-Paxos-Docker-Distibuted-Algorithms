using Microsoft.AspNetCore.Mvc;

namespace Node1.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;
	private readonly ILogger<AuthController> _logger;

	public AuthController(IAuthService authService, ILogger<AuthController> logger)
	{
		_authService = authService;
		_logger = logger;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register(RegisterRequestDTO request)
	{
		try
		{
			_logger.LogInformation("\n\nRegister Try\n\n");
			var token = await _authService.RegisterAsync(request);
			return Ok(new { token });
		}
		catch (Exception ex)
		{
			_logger.LogInformation("\n\nRegister Catch\n\n");
			return BadRequest(new { error = ex.Message });
		}
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginRequestDTO request)
	{
		try
		{
			_logger.LogInformation("\n\nLooging \n\n");
			var token = await _authService.LoginAsync(request);
			return Ok(new { token });
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}
}
