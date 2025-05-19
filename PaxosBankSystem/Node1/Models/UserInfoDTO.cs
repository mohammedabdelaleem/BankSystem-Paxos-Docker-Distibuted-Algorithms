namespace Node1.Models;

public class UserInfoDTO
{
	public string Id { get; set; }
	public string Name { get; set; }
	public ICollection<int> AccountsId { get; set; }

}
