using EmploymentSystem.Application.Commands.Vacancies.CreateVacancy;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.CreateVacancy
{
    public class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, Vacancy>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CreateVacancyCommandHandler> _logger;

        public CreateVacancyCommandHandler(IVacancyRepository vacancyRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ILogger<CreateVacancyCommandHandler> logger)
        {
            _vacancyRepository = vacancyRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Vacancy> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateVacancyCommand for Title: {Title}", request.Title);

            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                _logger.LogWarning("User is not authenticated.");
                throw new Exception("User is not authenticated.");
            }

            var user = await _userRepository.GetByEmailAsync(userIdClaim);
            if (user == null)
            {
                _logger.LogWarning("User not found.");
                throw new Exception("User not found.");
            }

            var vacancy = new Vacancy
            {
                Title = request.Title,
                Description = request.Description,
                ExpiryDate = request.ExpiryDate,
                MaxApplications = request.MaxApplications,
                IsActive = request.IsActive,
                EmployerId = user.Id,
                Applications = new List<ApplicationDetails>()
            };

            await _vacancyRepository.AddAsync(vacancy);

            _logger.LogInformation("Vacancy created successfully for Title: {Title}", request.Title);
            return vacancy;
        }
    }

}
