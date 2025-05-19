

namespace Node3.Services.Auth;

public interface IAuthService
{
	Task<string> RegisterAsync(RegisterRequestDTO request);
	Task<string> LoginAsync(LoginRequestDTO request);
}
