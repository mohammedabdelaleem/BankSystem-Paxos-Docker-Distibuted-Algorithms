namespace Node1.Services;

public interface IUserService
{
	Task<List<UserInfoDTO>> GetAll();
	Task<bool> IsExists(int userId);
	Task<UserInfoDTO> GetUserInfoAsync(int userId);
}
