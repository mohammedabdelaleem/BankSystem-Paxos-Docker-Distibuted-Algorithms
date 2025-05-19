namespace Node1.Services;

public interface IPaxosService
{

	Task<PaxosResponse> PrepareRegisterAsync(PaxosRegisterRequest request);
	Task<PaxosResponse> AcceptRegisterAsync(PaxosRegisterRequest request);
	Task<PaxosResponse> CommitRegisterAsync(PaxosRegisterRequest request);


	Task<PaxosResponse> PrepareLoginAsync(PaxosLoginRequest request);
	Task<PaxosResponse> AcceptLoginAsync(PaxosLoginRequest request);
	Task<PaxosResponse> CommitLoginAsync(PaxosLoginRequest request);
}
