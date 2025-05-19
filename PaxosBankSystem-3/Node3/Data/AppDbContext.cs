

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Node3.Data;
public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}
	public DbSet<Account> Accounts { get; set; }
	public DbSet<Transaction> Transactions { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);


		builder.Entity<Account>()
	.Property(a => a.Balance)
	.HasPrecision(18, 4); // or whatever fits your data

		builder.Entity<Transaction>()
			.Property(t => t.Amount)
			.HasPrecision(18, 4);

	}
}