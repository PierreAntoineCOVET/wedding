using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AerDbContext = Server.Models.AerDbContext;
using AccessGroup = Server.Models.AccessGroup;
using Server.Models;
using Server.DTOs;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<UserLight>> Get()
        {
            var users = await AerDbContext.Users.ToListAsync();

            return users.Select(u => new UserLight
            {
                UserName = u.UserName,
                AccessGroup = u.AccessGroup,
                Id = u.Id
            });
        }

        [HttpGet("user/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<UserLight?> Get(string userName)
        {
            var user = await AerDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                return null;
            }

            return new UserLight
            {
                UserName = user.UserName,
                AccessGroup = user.AccessGroup,
                Id = user.Id
            };
        }

        [HttpPost("user")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(UserFull user)
        {
            if (!Enum.IsDefined(typeof(AccessGroup), user.AccessGroup))
            {
                return BadRequest("Invalid enum value");
            }

            try
            {
                await AerDbContext.Users.AddAsync(new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    AccessGroup = user.AccessGroup
                });

                await AerDbContext.SaveChangesAsync();

                return Created();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Db update exception");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("user/{userId}")]
        public async Task Delete(int userId)
        {
            try
            {
                var dbUSer = await AerDbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if(dbUSer == null)
                {
                    return;
                }

                AerDbContext.Users.Remove(dbUSer);

                await AerDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
