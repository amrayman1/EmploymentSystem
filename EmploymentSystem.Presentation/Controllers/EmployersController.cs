using EmploymentSystem.Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly CreateVacancyHandler _createVacancyHandler;

        public EmployersController(CreateVacancyHandler createVacancyHandler)
        {
            _createVacancyHandler = createVacancyHandler;
        }

        [HttpPost("vacancies")]
        public async Task<IActionResult> CreateVacancy([FromBody] CreateVacancyCommand command)
        {
            await _createVacancyHandler.Handle(command);
            return Ok();
        }
    }
}
