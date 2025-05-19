
using Microsoft.AspNetCore.Identity;

namespace Node1.Services;

public class UserService(AppDbContext context, UserManager<ApplicationUser> userManager) : IUserService
{
	private readonly AppDbContext _context = context;
	private readonly UserManager<ApplicationUser> _userManager = userManager;


	public async Task<bool> IsExists(int userId)
	{
		var user = await _userManager.Users
			.FirstOrDefaultAsync(u => u.Id == userId);

		return user != null;
	}
	public async Task<UserInfoDTO> GetUserInfoAsync(int userId)
	{
		var user = await _userManager.Users
			.Include(u => u.Accounts)  // Assuming you have a navigation property for Accounts in User entity
			.FirstOrDefaultAsync(u => u.Id == userId);



		if (user == null)
		{
			throw new Exception("User not found.");
		}

		var userInfoDTO = new UserInfoDTO
		{
			Id = user.Id.ToString(),
			Name = user.UserName,
			AccountsId = user.Accounts.Select(a => a.Id).ToList() // Map to Account IDs
		};

		return userInfoDTO;
	}

	public async Task<List<UserInfoDTO>> GetAll()
	{
		return await _userManager.Users.Select(u=> new UserInfoDTO
		{
			Id = u.Id.ToString(),
			Name = u.UserName,
			AccountsId = u.Accounts.Select(a=>a.Id).ToList()
		}).ToListAsync();
	}
}
