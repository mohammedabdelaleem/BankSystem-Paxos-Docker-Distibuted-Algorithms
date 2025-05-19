namespace Node1.Services;

public interface IUserService
{
	Task<List<UserInfoDTO>> GetAll();
}
