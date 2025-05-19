using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Node1.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Node1.Services.Auth
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _config;

		public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config)
		{
			_userManager = userManager;
			_config = config;
		}

		public async Task<string> RegisterAsync(RegisterRequestDTO request)
		{
			var existingUser = await _userManager.FindByNameAsync(request.Username);
			if (existingUser != null)
				throw new Exception("Username already exists.");

			var user = new ApplicationUser
			{
				UserName = request.Username
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
				throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

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
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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
}
