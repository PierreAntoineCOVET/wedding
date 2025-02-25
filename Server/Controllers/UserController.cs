using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AerDbContext = Server.Models.AerDbContext;
using Roles = Server.Models.Roles;
using UserDto = Server.DTOs.User;
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
        public async Task<IEnumerable<UserDto>> Get()
        {
            var users = await AerDbContext.Users.ToListAsync();

            return users.Select(user => UserDto.FromModel(user));
        }

        //[HttpGet("{userName}")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<UserDto?> Get(string userName)
        {
            var user = await AerDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                return null;
            }

            return UserDto.FromModel(user);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<UserDto?> Get(int id)
        {
            var user = await AerDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            return UserDto.FromModel(user);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<AutocompleteItem>> Search(string query)
        {
            var users = await AerDbContext.Users
                .Where(user => EF.Functions.Like(user.LastName, $"%{query}%"))
                .Select(user => new AutocompleteItem { Label = user.LastName + " " + user.FirstName, Value = user.Id.ToString() })
                .ToListAsync();

            return users;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(UserDto user)
        {
            if (!Enum.IsDefined(typeof(Roles), user.Role))
            {
                return BadRequest("Invalid enum value");
            }

            try
            {
                await AerDbContext.Users.AddAsync(UserDto.ToModel(user));

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
