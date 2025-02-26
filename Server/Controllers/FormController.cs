using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DTOs;
using Server.Models;
using FormDto = Server.DTOs.Form;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private readonly AerDbContext AerDbContext;

        public FormController(AerDbContext aerDbContext)
        {
            AerDbContext = aerDbContext;
        }

        [HttpGet("forms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<FormDto>> Get()
        {
            var forms = await AerDbContext.Forms.ToListAsync();

            return forms.Select(form => FormDto.FromModel(form));
        }

        [HttpGet("foruser/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<FormDto?> Get(int userId)
        {
            var form = await AerDbContext.Forms.FirstOrDefaultAsync(f => f.UserId == userId);

            if (form == null)
            {
                return null;
            }

            return FormDto.FromModel(form);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(FormDto form)
        {
            if (form.UserId == 0 || (form.Id.HasValue && form.Id.Value != 0))
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var dbForm = FormDto.ToModel(form);

                await AerDbContext.Forms.AddAsync(dbForm);

                await AerDbContext.SaveChangesAsync();

                return Created(dbForm.Id.ToString(), dbForm.Id);
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

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(FormDto form)
        {
            if (form.UserId == 0 || !form.Id.HasValue)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                AerDbContext.Forms.Update(FormDto.ToModel(form));

                await AerDbContext.SaveChangesAsync();

                return Ok();
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
    }
}
