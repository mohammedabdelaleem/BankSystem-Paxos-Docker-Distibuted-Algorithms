using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Node1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController(AppDbContext context) : ControllerBase
{
	private readonly AppDbContext _context = context;


	//[HttpGet("users")]
	//public ActionResult<IEnumerable<User>> Users()
	//{
	//	var users = _context.Users.ToList();
	//	if(users.Count == 0)
	//		return NotFound(new {message= "No Fucking Users"});

	//	return Ok(users);
	//}



}
