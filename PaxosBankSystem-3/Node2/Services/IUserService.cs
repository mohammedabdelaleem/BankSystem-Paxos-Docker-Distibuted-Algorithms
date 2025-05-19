namespace Node2.Services;

public interface IUserService
{
	Task<List<UserInfoDTO>> GetAll();
}
