using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
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

        public SearchVacanciesQueryHandler(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<IEnumerable<Vacancy>> Handle(SearchVacanciesQuery request, CancellationToken cancellationToken)
        {
            return await _vacancyRepository.SearchVacanciesAsync(request.Title);
        }
    }
}
