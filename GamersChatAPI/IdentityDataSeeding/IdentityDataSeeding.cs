using GamersChatAPI.AggregationServices;
using GamersChatAPI.Areas.Identity.Data;
using GamersChatAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GamersChatAPI.IdentityDataSeeding
{
    public class IdentityDataSeeding
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserAggregationService userAggregationService;
        UserManager<GamersChatAPIUser> userManager;
        private readonly ILogger<IdentityDataSeeding> logger;
        private const string defaultPassword = "P@ssw0rd";
        private const string defaultAdminName = "System Admin";
        private readonly List<string> defaultRoles = new()
        {
            DefaultRoles.User,
            DefaultRoles.Admin
        };
        private readonly string defaultAdmin = "admin@admin.com";

        public IdentityDataSeeding(RoleManager<IdentityRole> roleManager, UserAggregationService userAggregationService, UserManager<GamersChatAPIUser> userManager, ILogger<IdentityDataSeeding> logger)
        {
            this.roleManager = roleManager;
            this.userAggregationService = userAggregationService;
            this.userManager = userManager;
            this.logger = logger;
        }

        private async Task SeedRoles()
        {
            foreach(var role in defaultRoles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    logger.LogDebug($"Seeding role: {role}");
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        logger.LogError("Failed to seed role {role}. Errors: {@Errors}", role, result.Errors);
                    }
                }
            }
        }

        private async Task SeedDefaultUser()
        {
            var user = await userManager.FindByNameAsync(defaultAdmin);
            if(user != null)
            {
                logger.LogInformation("Default User already exists and it will not be created.");
                return;
            }
            if(user == null)
            {
                await userAggregationService.CreateUser(new User()
                {
                    Role = DefaultRoles.Admin,
                    Email = defaultAdmin,
                    Name = defaultAdminName
                }, defaultPassword);
            }
        }

        public async Task SeedData()
        {
            await SeedRoles();
            await SeedDefaultUser();
        }
    }
}
