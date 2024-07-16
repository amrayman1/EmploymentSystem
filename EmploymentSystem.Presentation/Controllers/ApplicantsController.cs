using EmploymentSystem.Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly ApplyForVacancyHandler _applyForVacancyHandler;

        public ApplicantsController(ApplyForVacancyHandler applyForVacancyHandler)
        {
            _applyForVacancyHandler = applyForVacancyHandler;
        }

        [HttpPost("vacancies/apply")]
        public async Task<IActionResult> ApplyForVacancy([FromBody] ApplyForVacancyCommand command)
        {
            await _applyForVacancyHandler.Handle(command);
            return Ok();
        }
    }
}
