using GamersChatAPI.Areas.Identity.Data;
using GamersChatAPI.Models;
using GamersChatAPI.Services;
using GamersChatAPI.UserExtensions;
using Microsoft.AspNetCore.Identity;

namespace GamersChatAPI.AggregationServices
{
    public class UserAggregationService
    {
        private readonly UserService userService;
        private readonly UserManager<GamersChatAPIUser> userManager;
        private readonly RoleManager<IdentityRole> rolesManager;
        private readonly ILogger<UserAggregationService> logger;

        public UserAggregationService(UserService userService, UserManager<GamersChatAPIUser> userManager, RoleManager<IdentityRole> rolesManager, ILogger<UserAggregationService> logger)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.rolesManager = rolesManager;
            this.logger = logger;
        }

        private async Task<IEnumerable<User>> GetUsersWithIdentityData(IEnumerable<User> users)
        {
            List<User> userList = new();
            foreach (var user in users)
            {
                var identityUser = await userManager.FindByIdAsync(user.Id.ToString());
                if(identityUser != null)
                {
                    userList.Add(user.ToUser(identityUser));
                }
            }
            return userList;
        }

        public async Task<User> CreateUser(User newUser, string password)
        {
            var roleExists = await rolesManager.RoleExistsAsync(newUser.Role);
            if (!roleExists)
                throw new ArgumentException("The role specified for the user is invalid", "newUser");

            var identityUser = new GamersChatAPIUser(newUser.Email);
            identityUser.Email = newUser.Email;
            identityUser.EmailConfirmed = true;
            var createUserResult = await userManager.CreateAsync(identityUser, password);
            if(createUserResult != IdentityResult.Success)
            {
                logger.LogError("Failed to create identity user. {@Errors} ", createUserResult.Errors);
                throw new Exception("Failed to create new Identity user");
            }
            await
                userManager.AddToRoleAsync(identityUser, newUser.Role);

            User user = new()
            {
                Id = Guid.Parse(identityUser.Id),
                Name = newUser.Name,
                Role = newUser.Role
            };
            var savedUser = userService.AddUser(user);
            return savedUser.ToUser(identityUser);
        }

        public async Task DeleteUser(User newUser)
        {
            var userToDelete = new GamersChatAPIUser(newUser.Email);
            userToDelete.Id = newUser.Id.ToString();
            await userManager.DeleteAsync(userToDelete);
            userService.RemoveUser(newUser.Id);
        }

        public async Task DeleteUserById(Guid id)
        {
            var userToDelete = await userManager.FindByIdAsync(id.ToString());
            if (userToDelete != null)
            {
                await userManager.DeleteAsync(userToDelete);
            }
            userService.RemoveUser(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = userService.GetAllUsers();
            return await GetUsersWithIdentityData(users);
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = userService.GetUserById(id);
            var identityUser = await userManager.FindByIdAsync(id.ToString());
            if(identityUser == null)
            {
                throw new KeyNotFoundException($"User cannot be found {id.ToString()}");
            }

            return user.ToUser(identityUser);
        }

        public async Task<User> UpdateUser(User newUserData)
        {
            var user = userService.GetUserById(newUserData.Id);
            var identityUser = await userManager.FindByIdAsync(newUserData.Id.ToString());
            if (identityUser == null)
                throw new KeyNotFoundException($"User cannot be updated since it cannot be found in the list of active users.");

            user.Name = newUserData.Name;
            if(!string.IsNullOrEmpty(newUserData.Role) && user.Role != newUserData.Role)
            {
                var result = await userManager.AddToRoleAsync(identityUser, newUserData.Role);
                if (result != IdentityResult.Success)
                    throw new Exception($"Unable to add user to the new role {newUserData.Role}");
                await userManager.RemoveFromRoleAsync(identityUser, user.Role);
                user.Role = newUserData.Role;
            }
            user = userService.UserUpdate(user);
            return user.ToUser(identityUser);
        }
    }
}
