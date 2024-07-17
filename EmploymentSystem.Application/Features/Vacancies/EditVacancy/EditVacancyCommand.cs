using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.EditVacancy
{
    public class EditVacancyCommand : IRequest<Vacancy>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int MaxApplications { get; set; }
        public bool IsActive { get; set; }
        public string EmployerId { get; set; }
    }

    public class EditVacancyCommandHandler : IRequestHandler<EditVacancyCommand, Vacancy>
    {
        private readonly IVacancyRepository _vacancyRepository;

        public EditVacancyCommandHandler(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<Vacancy> Handle(EditVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(request.Id);

            if (vacancy == null)
            {
                // Handle not found case (throw exception or return null)
                throw new Exception("Vacancy not found");
            }

            vacancy.Title = request.Title;
            vacancy.Description = request.Description;
            vacancy.ExpiryDate = request.ExpiryDate;
            vacancy.MaxApplications = request.MaxApplications;
            vacancy.IsActive = request.IsActive;

            await _vacancyRepository.UpdateAsync(vacancy);

            return vacancy;
        }
    }
}
