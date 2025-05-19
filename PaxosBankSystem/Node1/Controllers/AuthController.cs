using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register(RegisterRequestDTO request)
	{
		try
		{
			var token = await _authService.RegisterAsync(request);
			return Ok(new { token });
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginRequestDTO request)
	{
		try
		{
			var token = await _authService.LoginAsync(request);
			return Ok(new { token });
		}
		catch (Exception ex)
		{
			return Unauthorized(new { error = ex.Message });
		}
	}
}
