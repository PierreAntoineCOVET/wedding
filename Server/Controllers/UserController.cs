using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AerDbContext = Server.Models.AerDbContext;
using AccessGroup = Server.Models.AccessGroup;
using UserModel = Server.Models.User;
using UserDto = Server.DTOs.User;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AerDbContext AerDbContext;

        public UserController(AerDbContext aerDbContext)
        {
            AerDbContext = aerDbContext;
        }

        [HttpGet("users")]
        public async Task<IEnumerable<UserDto>> Get()
        {
            var users = await AerDbContext.Users.ToListAsync();

            return users.Select(u => new UserDto
            {
                Name = $"{u.FirstName}.{u.LastName}",
                AccessGroup = u.AccessGroup,
                Id = u.Id
            });
        }

        [HttpGet("user")]
        public async Task<UserDto?> Get(string userName)
        {
            var user = await AerDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Name = $"{user.FirstName}.{user.LastName}",
                AccessGroup = user.AccessGroup,
                Id = user.Id
            };
        }

        [HttpPost("user")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(UserDto user)
        {
            if (!Enum.IsDefined(typeof(AccessGroup), user.AccessGroup))
            {
                return BadRequest("Invalid enum value");
            }

            try
            {
                await AerDbContext.Users.AddAsync(new UserModel
                {
                    FirstName = "first",
                    LastName = user.Name,
                    AccessGroup = user.AccessGroup
                });

                await AerDbContext.SaveChangesAsync();

                return Created();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
