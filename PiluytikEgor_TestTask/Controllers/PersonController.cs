using Microsoft.AspNetCore.Mvc;
using Application.Services.Interfaces;
using Application.DTO;

namespace PiluytikEgor_TestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly IPersonService _service;
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger, IPersonService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonDTO>>> GetAllPersons(int from, int to)
        {
            var persons = await _service.GetAllPersons(from, to);

            if (persons != null)
            {
                return Ok(persons);
            }
            return BadRequest(nameof(GetAllPersons));
        }
    }
}