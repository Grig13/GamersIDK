using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using GamersChatAPI.Areas.Identity.Data;
using GamersChatAPI.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GamersChatAPI.AggregationServices
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<GamersChatAPIUser> userClaimsPrincipalFactory;
        private readonly UserManager<GamersChatAPIUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserService userService;
        public ProfileService(IUserClaimsPrincipalFactory<GamersChatAPIUser> userClaimsPrincipalFactory, UserManager<GamersChatAPIUser> userManager, RoleManager<IdentityRole> roleManager, UserService userService)
        {
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userService = userService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            GamersChatAPIUser user = await userManager.FindByIdAsync(sub);
            ClaimsPrincipal userClaims = await userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            var appUser = userService.GetUserById(Guid.Parse(sub));

            if (userManager.SupportsUserRole)
            {
                IList<string> roles = await userManager.GetRolesAsync(user);
                foreach(var roleName in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                    if (roleManager.SupportsRoleClaims)
                    {
                        IdentityRole role = await roleManager.FindByNameAsync(roleName);
                        if(role != null)
                        {
                            claims.AddRange(await roleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            GamersChatAPIUser user = await userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
