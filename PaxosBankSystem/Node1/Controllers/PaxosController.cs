using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace Node1.Controllers;

[ApiController]
[Route("api/paxos")]
public class PaxosController : ControllerBase
{
	private readonly AppDbContext _context;
	private readonly IPaxosService _paxosService;

	// In-memory tracking of proposals
	private static readonly ConcurrentDictionary<string, PaxosRegisterRequest> _acceptedProposals = new();

	public PaxosController(AppDbContext context, IPaxosService paxosService
		)
	{
		_context = context;
		_paxosService = paxosService;
	}

	// Prepare phase for register
	[HttpPost("prepareRegister")]
	public async Task<IActionResult> PrepareRegister([FromBody] PaxosRegisterRequest request)
	{
		try
		{
			var result = await _paxosService.PrepareRegisterAsync(request);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	// Accept phase for register
	[HttpPost("acceptRegister")]
	public async Task<IActionResult> AcceptRegister([FromBody] PaxosRegisterRequest request)
	{
		try
		{
			var result = await _paxosService.AcceptRegisterAsync(request);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	// Commit phase for register
	[HttpPost("commitRegister")]
	public async Task<IActionResult> CommitRegister([FromBody] PaxosRegisterRequest request)
	{
		try
		{
			var result = await _paxosService.CommitRegisterAsync(request);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	
	// Prepare phase for login
	[HttpPost("prepareLogin")]
	public async Task<IActionResult> PrepareLogin([FromBody] PaxosLoginRequest request)
	{
		try
		{
			var result = await _paxosService.PrepareLoginAsync(request);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	// Accept phase for login
	[HttpPost("acceptLogin")]
	public async Task<IActionResult> AcceptLogin([FromBody] PaxosLoginRequest request)
	{
		try
		{
			var result = await _paxosService.AcceptLoginAsync(request);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	// Commit phase for login
	[HttpPost("commitLogin")]
	public async Task<IActionResult> CommitLogin([FromBody] PaxosLoginRequest request)
	{
		try
		{
			var result = await _paxosService.CommitLoginAsync(request);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}
}
