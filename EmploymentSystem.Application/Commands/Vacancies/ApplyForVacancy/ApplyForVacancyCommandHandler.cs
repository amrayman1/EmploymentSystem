using EmploymentSystem.Application.Commands.Vacancies.ApplyForVacancy;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace EmploymentSystem.Application.Features.Vacancies.ApplyForVacancy
{
    public class ApplyForVacancyCommandHandler : IRequestHandler<ApplyForVacancyCommand, ApplicationDetails>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ApplyForVacancyCommandHandler> _logger;

        public ApplyForVacancyCommandHandler(IApplicationRepository applicationRepository, IUserRepository userRepository, IVacancyRepository vacancyRepository, IHttpContextAccessor httpContextAccessor, ILogger<ApplyForVacancyCommandHandler> logger)
        {
            _applicationRepository = applicationRepository;
            _vacancyRepository = vacancyRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<ApplicationDetails> Handle(ApplyForVacancyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling ApplyForVacancyCommand for VacancyId: {VacancyId}", request.VacancyId);

            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                _logger.LogWarning("User is not authenticated.");
                throw new Exception("User is not authenticated.");
            }

            var user = await _userRepository.GetByEmailAsync(userIdClaim);
            var vacancy = await _vacancyRepository.GetByIdAsync(request.VacancyId);

            if (vacancy == null || !vacancy.IsActive || vacancy.ExpiryDate < DateTime.Now)
            {
                _logger.LogWarning("Vacancy is not available or expired.");
                throw new Exception("Vacancy is not available.");
            }

            var applicationCount = await _applicationRepository.GetApplicationCountByVacancyIdAsync(request.VacancyId);
            if (applicationCount >= vacancy.MaxApplications)
            {
                _logger.LogWarning("Maximum number of applications reached for VacancyId: {VacancyId}", request.VacancyId);
                throw new Exception("Maximum number of applications reached for this vacancy.");
            }

            var hasAppliedToday = await _applicationRepository.HasAppliedTodayAsync(user.Id);
            if (hasAppliedToday)
            {
                _logger.LogWarning("User has already applied today.");
                throw new Exception("You can only apply for one vacancy per day.");
            }

            var application = new ApplicationDetails
            {
                VacancyId = request.VacancyId,
                ApplicantId = user.Id,
                ApplicationDate = DateTime.Now,
                Resume = request.ResumeFilePath,
                CoverLetter = request.CoverLetter
            };

            await _applicationRepository.AddAsync(application);

            _logger.LogInformation("Application submitted successfully for VacancyId: {VacancyId}", request.VacancyId);
            return application;
        }
    }
}
