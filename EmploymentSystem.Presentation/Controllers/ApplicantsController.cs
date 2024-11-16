using EmploymentSystem.Application.Commands.Vacancies.ApplyForVacancy;
using EmploymentSystem.Application.Commands.Vacancies.GetAllVacancies;
using EmploymentSystem.Application.Features.Vacancies.SearchForVacancy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Applicant")]
    public class ApplicantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ApplicantsController> _logger;

        public ApplicantsController(IMediator mediator, ILogger<ApplicantsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("ApplyForVacancy")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> ApplyForVacancy([FromForm] ApplyForVacancyCommand command, IFormFile resume)
        {
            _logger.LogInformation("ApplyForVacancy action started.");

            if (resume == null || resume.Length == 0)
            {
                _logger.LogWarning("No resume uploaded.");
                return BadRequest("No resume uploaded.");
            }

            try
            {
                var uploadsFolder = Path.Combine("uploads", "resumes");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, Guid.NewGuid() + Path.GetExtension(resume.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await resume.CopyToAsync(stream);
                }

                command.ResumeFilePath = filePath;
                var application = await _mediator.Send(command);
                _logger.LogInformation("Application submitted successfully.");
                return Ok("Application submitted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while applying for a vacancy.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchVacancies")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> SearchVacancies([FromBody] SearchVacanciesQuery query)
        {
            _logger.LogInformation("SearchVacancies action started.");

            try
            {
                var result = await _mediator.Send(query);
                _logger.LogInformation("Vacancies retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for vacancies.");
                return BadRequest(ex.Message);
            }
        }

        
        [Authorize(Roles = "Applicant")]
        [HttpGet("GetActiveVacancies")]
        public async Task<IActionResult> GetActiveVacancies()
        {
            _logger.LogInformation("Request received to fetch active vacancies");

            try
            {
                var vacancies = await _mediator.Send(new GetActiveVacanciesQuery());
                return Ok(vacancies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching active vacancies");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
