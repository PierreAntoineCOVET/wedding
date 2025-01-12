using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Model;

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
        public async Task<IEnumerable<User>> Get()
        {
            return await AerDbContext.Users.ToListAsync();
        }

        [HttpGet("user")]
        public async Task<User?> Get(string userName)
        {
            return await AerDbContext.Users.FirstOrDefaultAsync(u => u.Name == userName);
        }

        [HttpPost("user")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(User user)
        {
            if (!Enum.IsDefined(typeof(AccessGroup), user.AccessGroup))
            {
                return BadRequest("Invalid enum value");
            }

            await AerDbContext.Users.AddAsync(user);

            try
            {
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
