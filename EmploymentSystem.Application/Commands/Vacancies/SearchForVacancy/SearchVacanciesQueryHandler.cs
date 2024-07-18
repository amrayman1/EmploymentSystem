using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.SearchForVacancy
{
    public class SearchVacanciesQueryHandler : IRequestHandler<SearchVacanciesQuery, IEnumerable<Vacancy>>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ILogger<SearchVacanciesQueryHandler> _logger;

        public SearchVacanciesQueryHandler(IVacancyRepository vacancyRepository, ILogger<SearchVacanciesQueryHandler> logger)
        {
            _vacancyRepository = vacancyRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Vacancy>> Handle(SearchVacanciesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling SearchVacanciesQuery with Title: {Title}", request.Title);
            var vacancies = await _vacancyRepository.SearchVacanciesAsync(request.Title);
            _logger.LogInformation("Found {Count} vacancies for Title: {Title}", vacancies.Count(), request.Title);
            return vacancies;
        }
    }
}
