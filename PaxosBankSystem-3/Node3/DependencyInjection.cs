using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Node3
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers();


			// 1. Add DbContext
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			

			// 2. Add Identity and configure default token providers (optional)
			services.AddIdentity<ApplicationUser, IdentityRole<int>>()
			.AddEntityFrameworkStores<AppDbContext>()
			.AddDefaultTokenProviders();

			// 3.
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = "Bearer";
				options.DefaultChallengeScheme = "Bearer";
			})
		.AddJwtBearer("Bearer", options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = configuration["Jwt:Issuer"],

				ValidateAudience = true,
				ValidAudience = configuration["Jwt:Audience"],

				ValidateLifetime = true,

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),

				NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
				RoleClaimType = "role"
			};
		});


			// 4. Add Authorization
			services.AddAuthorization();

			// 5. Register Scoped Services
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<ITransactionService, TransactionService>();
			services.AddScoped<IUserService, UserService>();
			services.AddSingleton<PaxosService>();  // Register PaxosService
			services.AddHttpClient<PaxosService>(); //
													// 6. Add Controllers and Swagger for API documentation

			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			// 7. Register IHttpClientFactory
			services.AddHttpClient();

			return services;
		}
	}
}
