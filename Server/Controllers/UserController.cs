using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AerDbContext = Server.Models.AerDbContext;
using AccessGroup = Server.Models.AccessGroup;
using Server.Models;
using Server.DTOs;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            return users.Select(user => new UserLight(user));
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<UserLight?> Get(string userName)
        {
            var user = await AerDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                return null;
            }

            return new UserLight(user);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<AutocompleteItem>> Search(string query)
        {
            var users = await AerDbContext.Users
                .Where(user => EF.Functions.Like(user.LastName, $"%{query}%"))
                .Select(user => new AutocompleteItem { Label = user.LastName, Value = user.Id.ToString() })
                .ToListAsync();

            return users;
        }

        [HttpPost()]
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

        [HttpDelete("{userId}")]
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
