using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.EditVacancy
{
    public class EditVacancyCommandHandler : IRequestHandler<EditVacancyCommand, Vacancy>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ILogger<EditVacancyCommandHandler> _logger;

        public EditVacancyCommandHandler(IVacancyRepository vacancyRepository, ILogger<EditVacancyCommandHandler> logger)
        {
            _vacancyRepository = vacancyRepository;
            _logger = logger;
        }

        public async Task<Vacancy> Handle(EditVacancyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling EditVacancyCommand for Vacancy Id: {Id}", request.Id);

            var vacancy = await _vacancyRepository.GetByIdAsync(request.Id);

            if (vacancy == null)
            {
                _logger.LogWarning("Vacancy with Id: {Id} not found", request.Id);
                throw new Exception("Vacancy not found");
            }

            vacancy.Title = request.Title;
            vacancy.Description = request.Description;
            vacancy.ExpiryDate = request.ExpiryDate;
            vacancy.MaxApplications = request.MaxApplications;
            vacancy.IsActive = request.IsActive;

            await _vacancyRepository.UpdateAsync(vacancy);

            _logger.LogInformation("Vacancy with Id: {Id} updated successfully", request.Id);

            return vacancy;
        }
    }
}
