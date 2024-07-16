using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.UseCases
{
    public class CreateVacancyCommand
    {
        public Guid EmployerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxApplications { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class CreateVacancyHandler
    {
        private readonly IVacancyRepository _vacancyRepository;

        public CreateVacancyHandler(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task Handle(CreateVacancyCommand command)
        {
            var vacancy = new Vacancy
            {
                VacancyId = Guid.NewGuid(),
                EmployerId = command.EmployerId,
                Title = command.Title,
                Description = command.Description,
                MaxApplications = command.MaxApplications,
                ExpiryDate = command.ExpiryDate,
                IsActive = true
            };

            await _vacancyRepository.AddAsync(vacancy);
        }
    }
}
