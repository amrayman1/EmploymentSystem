using EmploymentSystem.Application.Commands.Vacancies.ApplyForVacancy;
using EmploymentSystem.Application.Features.Vacancies.SearchForVacancy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("ApplyForVacancy")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> ApplyForVacancy(ApplyForVacancyCommand command)
        {
            var application = await _mediator.Send(command);
            return Ok(application);
        }

        [HttpPost("SearchVacancies")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> SearchVacancies([FromBody] SearchVacanciesQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
