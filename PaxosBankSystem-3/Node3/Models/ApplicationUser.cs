using Microsoft.AspNetCore.Identity;

namespace Node3.Models;

public class ApplicationUser : IdentityUser<int>
{
	public ICollection<Account> Accounts { get; set; }
}
