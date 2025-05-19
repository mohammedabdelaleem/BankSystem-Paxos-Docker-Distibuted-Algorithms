

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Node1.Data;
public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Account> Accounts { get; set; }
	public DbSet<Transaction> Transactions { get; set; }
}