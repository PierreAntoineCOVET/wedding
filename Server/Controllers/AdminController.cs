using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DTOs;
using Server.Models;
using UserDto = Server.DTOs.User;
using AdminDto = Server.DTOs.Admin;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AerDbContext AerDbContext;

        public AdminController(AerDbContext aerDbContext)
        {
            AerDbContext = aerDbContext;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<AdminDto> Get()
        {
            var users = await AerDbContext.Users
                .Include(user => user.Form)
                .ToListAsync();

            var unAnswered = users
                .Where(user => user.Form == null || (user.Form != null && user.Form.ConfirmedDays == Days.None))
                .Select(user => UserDto.FromModel(user, true))
                .ToList();

            var comming = users
                .Where(user => user.Form != null && user.Form.ConfirmedDays != Days.Never && user.Form.ConfirmedDays != Days.None)
                .Select(user => UserDto.FromModel(user, true))
                .ToList();

            var notComming = users
                .Where(user => user.Form != null && user.Form.ConfirmedDays == Days.Never)
                .Select(user => UserDto.FromModel(user, true))
                .ToList();

            return new AdminDto
            {
                UnAnswered = unAnswered,
                Comming = comming,
                NotComming = notComming
            };
        }
    }
}
