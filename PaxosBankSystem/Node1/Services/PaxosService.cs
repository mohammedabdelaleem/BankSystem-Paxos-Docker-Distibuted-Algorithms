namespace Node1.Services;

public class PaxosService : IPaxosService
{
	private readonly HttpClient _httpClient;
	private readonly IConfiguration _config;

	public PaxosService(HttpClient httpClient, IConfiguration config)
	{
		_httpClient = httpClient;
		_config = config;
	}

	public async Task<PaxosResponse> PrepareLoginAsync(PaxosLoginRequest request)
	{
		var nodes = _config.GetSection("PaxosNodes").Get<string[]>();
		int prepareCount = 0;

		foreach (var node in nodes)
		{
			var response = await _httpClient.PostAsJsonAsync($"{node}/api/paxos/prepareLogin", request);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<PaxosResponse>();
				if (result.Accepted)
				{
					prepareCount++;
				}
			}
		}

		if (prepareCount < 2) throw new Exception("Prepare phase failed. Not enough nodes accepted.");
		return new PaxosResponse { Accepted = true, ProposalId = request.ProposalId };
	}

	public async Task<PaxosResponse> AcceptLoginAsync(PaxosLoginRequest request)
	{
		var nodes = _config.GetSection("PaxosNodes").Get<string[]>();
		int acceptCount = 0;

		foreach (var node in nodes)
		{
			var response = await _httpClient.PostAsJsonAsync($"{node}/api/paxos/acceptLogin", request);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<PaxosResponse>();
				if (result.Accepted)
				{
					acceptCount++;
				}
			}
		}

		if (acceptCount < 2) throw new Exception("Accept phase failed. Not enough nodes accepted.");
		return new PaxosResponse { Accepted = true, ProposalId = request.ProposalId };
	}

	public async Task<PaxosResponse> CommitLoginAsync(PaxosLoginRequest request)
	{
		var nodes = _config.GetSection("PaxosNodes").Get<string[]>();

		foreach (var node in nodes)
		{
			var response = await _httpClient.PostAsJsonAsync($"{node}/api/paxos/commitLogin", request);
			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("Commit phase failed. One or more nodes rejected.");
			}
		}

		return new PaxosResponse { Accepted = true, ProposalId = request.ProposalId };
	}




	public async Task<PaxosResponse> PrepareRegisterAsync(PaxosRegisterRequest request)
	{
		var nodes = _config.GetSection("PaxosNodes").Get<string[]>();
		int prepareCount = 0;

		foreach (var node in nodes)
		{
			var response = await _httpClient.PostAsJsonAsync($"{node}/api/paxos/prepareRegister", request);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<PaxosResponse>();
				if (result.Accepted)
				{
					prepareCount++;
				}
			}
		}

		if (prepareCount < 2) throw new Exception("Prepare phase failed. Not enough nodes accepted.");
		return new PaxosResponse { Accepted = true, ProposalId = request.ProposalId };
	}

	public async Task<PaxosResponse> AcceptRegisterAsync(PaxosRegisterRequest request)
	{
		var nodes = _config.GetSection("PaxosNodes").Get<string[]>();
		int acceptCount = 0;

		foreach (var node in nodes)
		{
			var response = await _httpClient.PostAsJsonAsync($"{node}/api/paxos/acceptRegister", request);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<PaxosResponse>();
				if (result.Accepted)
				{
					acceptCount++;
				}
			}
		}

		if (acceptCount < 2) throw new Exception("Accept phase failed. Not enough nodes accepted.");
		return new PaxosResponse { Accepted = true, ProposalId = request.ProposalId };
	}

	public async Task<PaxosResponse> CommitRegisterAsync(PaxosRegisterRequest request)
	{
		var nodes = _config.GetSection("PaxosNodes").Get<string[]>();

		foreach (var node in nodes)
		{
			var response = await _httpClient.PostAsJsonAsync($"{node}/api/paxos/commitRegister", request);
			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("Commit phase failed. One or more nodes rejected.");
			}
		}

		return new PaxosResponse { Accepted = true, ProposalId = request.ProposalId };
	}
}
