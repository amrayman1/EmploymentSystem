using EmploymentSystem.Application.Commands.Vacancies.CreateVacancy;
using EmploymentSystem.Application.Commands.Vacancies.GetAllApplicants;
using EmploymentSystem.Application.Commands.Vacancies.GetAllVacancies;
using EmploymentSystem.Application.Features.Vacancies.DeleteVacancy;
using EmploymentSystem.Application.Features.Vacancies.EditVacancy;
using EmploymentSystem.Application.Features.Vacancies.GatAllVacancies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employer")]
    public class EmployersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateVacancy")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> CreateVacancy([FromBody] CreateVacancyCommand command)
        {
            var vacancy = await _mediator.Send(command);
            return Ok(vacancy);
        }

        [HttpPut("EditVacancy/{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> EditVacancy(int id, [FromBody] EditVacancyCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Vacancy ID mismatch");
            }

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("DeleteVacancy/{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var command = new DeleteVacancyCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("GetActiveVacancies")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetActiveVacancies()
        {
            var vacancies = await _mediator.Send(new GetActiveVacanciesQuery());
            return Ok(vacancies);
        }

        [HttpGet("GetAllVacancies")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetAllVacancies()
        {
            var vacancies = await _mediator.Send(new GetVacanciesQuery());
            return Ok(vacancies);
        }

        [HttpGet("GetAllApplicants/{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetAllApplicants(int id)
        {
            var applications = await _mediator.Send(new GetApplicantsForVacancyQuery { VacancyId = id });
            return Ok(applications);
        }
    }
}
