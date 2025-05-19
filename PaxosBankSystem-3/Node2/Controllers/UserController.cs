using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Node2.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController(IUserService userService) : ControllerBase
{
	private readonly IUserService _userService = userService;

	//[Authorize]
	[HttpGet("all")]
	public async Task<ActionResult> GetAll()
	{
		var users = await _userService.GetAll();
		return users!= null? Ok(users) : NotFound(new {message = "No Users Found"});
	}


}
