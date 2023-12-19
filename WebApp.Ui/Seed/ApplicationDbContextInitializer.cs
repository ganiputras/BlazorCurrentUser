using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebApp.Ui.Data;
using WebApp.Ui.Services.ClaimTypes;

namespace WebApp.Ui.Seed;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;

    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;


    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            _context.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }


    private async Task TrySeedAsync()
    {


        var adminRole = new IdentityRole("Admin");
        if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
        {
            await _roleManager.CreateAsync(adminRole);

        }

        var admin = new ApplicationUser
        {
            UserName = "admin@cm.com",
            Email = "admin@cm.com",
            IsActive = true,
            EmailConfirmed = true,
            DisplayName = "Company1 Admin",
            TenantId = "TENANT1",
            CompanyId = "COMPANY1",
        };



        if (_userManager.Users.All(u => u.UserName != admin.UserName))
        {
            await _userManager.CreateAsync(admin, "P@ssw0rd");
            await _userManager.AddToRolesAsync(admin, new[] { adminRole.Name! });
        }


    }
}