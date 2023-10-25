using LPA.Data;
using LPA.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

var roles = new[] { "Admin", "User" };

foreach (var role in roles)
{
    if (!await roleManager.RoleExistsAsync(role))
    {
        await roleManager.CreateAsync(new IdentityRole(role));
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

await InitializeAsync(app);

app.Run();




static async Task InitializeAsync(IApplicationBuilder app)
{
	var services = app.ApplicationServices.CreateScope().ServiceProvider;

	var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

	string defaultRole = "Admin";
	string defaultUserEmail = "admin@lpa.com";
	string defaultPassword = "P@ssw0rd";

	if (await userManager.FindByEmailAsync(defaultUserEmail) is null)
	{
		var user = new ApplicationUser
		{
			UserName = defaultUserEmail,
			Email = defaultUserEmail,
			EmailConfirmed = true,
		};
		await userManager.CreateAsync(user, defaultPassword);
		await userManager.AddToRoleAsync(user, defaultRole);
	}
}
