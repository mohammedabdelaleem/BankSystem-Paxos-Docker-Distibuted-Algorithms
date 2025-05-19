namespace Node1.Services;


	public interface IJwtTokenService
	{
	string GenerateJwtToken(ApplicationUser user);
}

