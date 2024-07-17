using EmploymentSystem.Application.Commands.Vacancies.CreateVacancy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmploymentSystem.Application.Commands.Vacancies.GetAllVacancies;
using EmploymentSystem.Application.Commands.Vacancies.GetAllApplicants;

namespace EmploymentSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VacanciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveVacancies()
        {
            var vacancies = await _mediator.Send(new GetActiveVacanciesQuery());
            return Ok(vacancies);
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Create(CreateVacancyCommand command)
        {
            var vacancy = await _mediator.Send(command);
            return Ok(vacancy);
        }

        [HttpGet("{id}/applications")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetApplicants(int id)
        {
            var applications = await _mediator.Send(new GetApplicantsForVacancyQuery { VacancyId = id });
            return Ok(applications);
        }

    }
}
