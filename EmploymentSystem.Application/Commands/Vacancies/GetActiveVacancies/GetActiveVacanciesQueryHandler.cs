using EmploymentSystem.Application.Commands.Vacancies.GetAllVacancies;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.GetActiveVacancies
{
    public class GetActiveVacanciesQueryHandler : IRequestHandler<GetActiveVacanciesQuery, IEnumerable<Vacancy>>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ILogger<GetActiveVacanciesQueryHandler> _logger;

        public GetActiveVacanciesQueryHandler(IVacancyRepository vacancyRepository, ILogger<GetActiveVacanciesQueryHandler> logger)
        {
            _vacancyRepository = vacancyRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Vacancy>> Handle(GetActiveVacanciesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching active vacancies");

            try
            {
                var vacancies = await _vacancyRepository.GetActiveVacanciesAsync();
                return vacancies;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching active vacancies");
                throw;
            }
        }
    }

}
