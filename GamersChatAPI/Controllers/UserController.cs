using GamersChatAPI.AggregationServices;
using GamersChatAPI.Areas.Identity.Data;
using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.Net;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserAggregationService aggregationService;

        public UserController(UserAggregationService aggregationService)
        {
            this.aggregationService = aggregationService;
        }

        private GamersChatAPIUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<GamersChatAPIUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(GamersChatAPIUser)}'." + 
                    $"Ensure that '{nameof(GamersChatAPIUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Action was successful")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server error occured and is logged")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await aggregationService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception)
            {
                return Problem("Unable to retrieve users associated data.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var users = await aggregationService.GetUserById(id);
                return Ok(users);
            }
            catch (Exception)
            {
                return Problem("Could not load the user from the database.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User newUser)
        {
            try
            {
                var user = await aggregationService.CreateUser(new User()
                {
                    Name = newUser.Name,
                    Email = newUser.Email,
                    Role = newUser.Role,
                    Posts = null
                }, "P@ssw0rd");
                return Ok(user);
            }
            catch (Exception)
            {
                return Problem("Could not create the user.");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await aggregationService.DeleteUserById(id);
                return Ok();
            }
            catch (Exception)
            {
                return Problem("Cannot remove the specified user.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(Guid id, [FromBody]User userData)
        {
            try
            {
                var user = await aggregationService.UpdateUser(userData);
                return Ok(user);
            }
            catch (Exception)
            {
                return Problem("Could not create the user");
            }
        }
    }
}
