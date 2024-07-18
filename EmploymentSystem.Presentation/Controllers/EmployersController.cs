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
        private readonly ILogger<EmployersController> _logger;

        public EmployersController(IMediator mediator, ILogger<EmployersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("CreateVacancy")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> CreateVacancy([FromBody] CreateVacancyCommand command)
        {
            _logger.LogInformation("CreateVacancy action started.");

            try
            {
                var vacancy = await _mediator.Send(command);
                _logger.LogInformation("Vacancy created successfully.");
                return Ok(vacancy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a vacancy.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("EditVacancy/{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> EditVacancy(int id, [FromBody] EditVacancyCommand command)
        {
            _logger.LogInformation("EditVacancy action started for Vacancy Id: {Id}", id);

            if (id != command.Id)
            {
                _logger.LogWarning("Vacancy ID mismatch: route Id {RouteId} does not match command Id {CommandId}", id, command.Id);
                return BadRequest("Vacancy ID mismatch");
            }

            try
            {
                var result = await _mediator.Send(command);

                if (result == null)
                {
                    _logger.LogWarning("Vacancy with Id: {Id} not found", id);
                    return NotFound();
                }

                _logger.LogInformation("Vacancy with Id: {Id} updated successfully", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating Vacancy with Id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteVacancy/{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            _logger.LogInformation("DeleteVacancy action started for Vacancy Id: {Id}", id);

            var command = new DeleteVacancyCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                _logger.LogWarning("Vacancy with Id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Vacancy with Id: {Id} deleted successfully", id);
            return NoContent();
        }


        [HttpGet("GetAllVacancies")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetAllVacancies()
        {
            _logger.LogInformation("Request received to fetch all vacancies");

            try
            {
                var vacancies = await _mediator.Send(new GetVacanciesQuery());
                return Ok(vacancies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all vacancies");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("GetAllApplicants/{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetAllApplicants(int id)
        {
            _logger.LogInformation("Request received to fetch applicants for vacancy with ID: {VacancyId}", id);

            try
            {
                var applications = await _mediator.Send(new GetApplicantsForVacancyQuery { VacancyId = id });
                return Ok(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching applicants for vacancy with ID: {VacancyId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
