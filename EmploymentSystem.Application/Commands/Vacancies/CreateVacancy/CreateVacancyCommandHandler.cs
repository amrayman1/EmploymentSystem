using EmploymentSystem.Application.Commands.Vacancies.CreateVacancy;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateVacancyCommandHandler(IVacancyRepository vacancyRepository, IHttpContextAccessor httpContextAccessor)
        {
            _vacancyRepository = vacancyRepository; 
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Vacancy> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = new Vacancy
            {
                Title = request.Title,
                Description = request.Description,
                ExpiryDate = request.ExpiryDate,
                MaxApplications = request.MaxApplications,
                IsActive = request.IsActive,
                EmployerId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                Applications = new List<ApplicationDetails>()
            };

            await _vacancyRepository.AddAsync(vacancy);
            return vacancy;
        }
    }

}
