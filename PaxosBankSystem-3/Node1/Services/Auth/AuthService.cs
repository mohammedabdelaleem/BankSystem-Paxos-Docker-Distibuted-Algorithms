
using Microsoft.AspNetCore.Identity;

namespace Node1.Services.Auth;

public class AuthService : IAuthService
{
	private readonly IConfiguration _config;
	private readonly UserManager<ApplicationUser> _userManager;

	public AuthService( IConfiguration config, UserManager<ApplicationUser> userManager)
	{
		_config = config;
		_userManager = userManager;
	}
	public async Task<string> RegisterAsync(RegisterRequestDTO request)
	{
		var existingUser = await _userManager.FindByNameAsync(request.Username);
		if (existingUser != null)
			throw new Exception("Username already exists.");

		var user = new ApplicationUser
		{
			UserName = request.Username,
			Email = request.Email ?? $"{request.Username}@example.com" // Fallback if Email is null
		};

		var result = await _userManager.CreateAsync(user, request.Password);
		if (!result.Succeeded)
		{
			var errors = string.Join("; ", result.Errors.Select(e => e.Description));
			Console.WriteLine($"Registration failed: {errors}");
			throw new Exception(errors);
		}

		Console.WriteLine($"User created: ID={user.Id}, Username={user.UserName}, Email={user.Email}");
		return GenerateJwtToken(user);
	}




	public async Task<string> LoginAsync(LoginRequestDTO request)
	{
		var user = await _userManager.FindByNameAsync(request.Username);
		if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
			throw new Exception("Invalid username or password.");

		return GenerateJwtToken(user);
	}
	
	private string GenerateJwtToken(ApplicationUser user)
	{
		var claims = new[] {
			new Claim("sub", user.Id.ToString()), // Use 'sub' instead of ClaimTypes.NameIdentifier
            new Claim(ClaimTypes.Name, user.UserName)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresInMinutes"]));

		var token = new JwtSecurityToken(
			issuer: _config["Jwt:Issuer"],
			audience: _config["Jwt:Audience"],
			claims: claims,
			expires: expires,
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
