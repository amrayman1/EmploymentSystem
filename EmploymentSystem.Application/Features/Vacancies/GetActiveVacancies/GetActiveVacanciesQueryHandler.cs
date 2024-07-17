using EmploymentSystem.Application.Commands.Vacancies.GetAllVacancies;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
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

        public GetActiveVacanciesQueryHandler(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<IEnumerable<Vacancy>> Handle(GetActiveVacanciesQuery request, CancellationToken cancellationToken)
        {
            return await _vacancyRepository.GetActiveVacanciesAsync();
        }
    }

}
