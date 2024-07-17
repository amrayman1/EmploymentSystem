using EmploymentSystem.Application.Commands.Vacancies.CreateVacancy;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public CreateVacancyCommandHandler(IVacancyRepository vacancyRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _vacancyRepository = vacancyRepository; 
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<Vacancy> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("User is not authenticated.");
            }

            var user = await _userRepository.GetByEmailAsync(userIdClaim);
            if (user == null)
            {
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
            return vacancy;
        }
    }

}
