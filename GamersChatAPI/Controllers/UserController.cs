using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return this._userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public User GetUserById(Guid id)
        {
            return this._userService.GetUserById(id);
        }

        [HttpPut("{id}")]
        public User Update(Guid id, [FromBody] User user)
        {
            var userToUpdate = this._userService.GetUserById(id);
            userToUpdate.Name = user.Name;
            userToUpdate.Role = user.Role;
            return this._userService.UserUpdate(userToUpdate);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this._userService.RemoveUser(id);
        }

        [HttpPost]
        public User Post(User user)
        {
            var userToAdd = new User
            {
                Name = user.Name,
                Role = user.Role,
                Posts = user.Posts,
            };
            return this._userService.AddUser(userToAdd);
        }
    }
}
