//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace Node1.Controllers;


////[Authorize]
//[Route("api/[controller]")]
//[ApiController]
//public class TestController(AppDbContext context, UserManager<ApplicationUser> userManager) : ControllerBase
//{
//	private readonly AppDbContext _context = context;
//	private readonly UserManager<ApplicationUser> _userManager = userManager;

//	//[Authorize]

//	[HttpGet("users")]
//	//[ProducesResponseType(StatusCodes.Status401Unauthorized)]
//	[ProducesResponseType(StatusCodes.Status200OK)]
//	[ProducesResponseType(StatusCodes.Status404NotFound)]

//	public async Task<ActionResult<IEnumerable<UserInfoDTO>>> Users()
//	{

//		//if (!User.Identity.IsAuthenticated)
//		//{
//		//	return Unauthorized(new { message = "User is not authenticated" });
//		//}

//		var users = await _userManager.Users
//			.Select(u=> new UserInfoDTO
//			{
//				Id = u.Id.ToString(),
//				Name = u.UserName,
//				AccountsId =  u.Accounts.Select(a=>a.Id).ToList() 
//			})
//			.ToListAsync();

//		if (users.Count == 0)
//			return NotFound(new { message = "No Fucking Users" });

//		return Ok(users);
//	}



//}
