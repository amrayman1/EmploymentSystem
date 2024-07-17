using EmploymentSystem.Application.Commands.Vacancies.CreateVacancy;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.CreateVacancy
{
    public class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, Vacancy>
    {
        private readonly IVacancyRepository _vacancyRepository;

        public CreateVacancyCommandHandler(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
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
                EmployerId = request.EmployerId,
                Applications = new List<ApplicationDetails>()
            };

            await _vacancyRepository.AddAsync(vacancy);
            return vacancy;
        }
    }

}
