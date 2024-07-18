using EmploymentSystem.Application.Commands.Vacancies.GetAllApplicants;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.GetApplicants
{
    public class GetApplicantsForVacancyQueryHandler : IRequestHandler<GetApplicantsForVacancyQuery, IEnumerable<ApplicationDetails>>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ILogger<GetApplicantsForVacancyQueryHandler> _logger;

        public GetApplicantsForVacancyQueryHandler(IVacancyRepository vacancyRepository, ILogger<GetApplicantsForVacancyQueryHandler> logger)
        {
            _vacancyRepository = vacancyRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ApplicationDetails>> Handle(GetApplicantsForVacancyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching applicants for vacancy with ID: {VacancyId}", request.VacancyId);
                return await _vacancyRepository.GetApplicationsByVacancyIdAsync(request.VacancyId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching applicants for vacancy with ID: {VacancyId}", request.VacancyId);
                throw;
            }
        }
    }

}
