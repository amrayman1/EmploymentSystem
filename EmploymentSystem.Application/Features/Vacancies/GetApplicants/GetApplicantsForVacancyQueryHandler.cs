using EmploymentSystem.Application.Commands.Vacancies.GetAllApplicants;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
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

        public GetApplicantsForVacancyQueryHandler(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<IEnumerable<ApplicationDetails>> Handle(GetApplicantsForVacancyQuery request, CancellationToken cancellationToken)
        {
            return await _vacancyRepository.GetApplicationsByVacancyIdAsync(request.VacancyId);
        }
    }

}
