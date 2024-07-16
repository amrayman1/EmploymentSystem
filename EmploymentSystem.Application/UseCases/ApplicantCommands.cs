using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.UseCases
{
    public class ApplyForVacancyCommand
    {
        public Guid ApplicantId { get; set; }
        public Guid VacancyId { get; set; }
    }

    public class ApplyForVacancyHandler
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IVacancyRepository _vacancyRepository;

        public ApplyForVacancyHandler(IApplicationRepository applicationRepository, IVacancyRepository vacancyRepository)
        {
            _applicationRepository = applicationRepository;
            _vacancyRepository = vacancyRepository;
        }

        public async Task Handle(ApplyForVacancyCommand command)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(command.VacancyId);
            if (vacancy == null || !vacancy.IsActive || vacancy.ExpiryDate < DateTime.UtcNow)
            {
                throw new Exception("Vacancy is not available.");
            }

            var applicationCount = (await _applicationRepository.GetAllAsync())
                .Count(a => a.VacancyId == command.VacancyId);

            if (applicationCount >= vacancy.MaxApplications)
            {
                throw new Exception("Maximum number of applications reached.");
            }

            var application = new ApplicationDetails
            {
                ApplicationId = Guid.NewGuid(),
                ApplicantId = command.ApplicantId,
                VacancyId = command.VacancyId,
                ApplicationDate = DateTime.UtcNow
            };

            await _applicationRepository.AddAsync(application);
        }
    }
}
