namespace Node3.Services;

public interface IUserService
{
	Task<List<UserInfoDTO>> GetAll();
}
