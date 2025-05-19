using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Node1.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
	private readonly IUserService _userService = userService;

	[HttpGet("all")]
	public async Task<ActionResult> GetAll()
	{
		var fromUserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (!int.TryParse(fromUserIdStr, out var fromUserId))
		{
			return Unauthorized();
		}

		var users = await _userService.GetAll();
		return users!= null? Ok(users) : NotFound(new {message = "No Users Found"});
	}


}
