namespace Node3.Models;

public class UserInfoDTO
{
	public int Id { get; set; }
	public string Name { get; set; }
	public ICollection<int> AccountsId { get; set; }

}
