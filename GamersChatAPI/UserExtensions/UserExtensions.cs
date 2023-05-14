using GamersChatAPI.Areas.Identity.Data;
using GamersChatAPI.Models;

namespace GamersChatAPI.UserExtensions
{
    public static class UserExtensions
    {
        public static User ToUser(this User user, GamersChatAPIUser identityUser)
        {
            return new User
            {
                Id = user.Id,
                Role = user.Role,
                Email = identityUser.Email,
                Name = user.Name
            };
        }
    }
}
