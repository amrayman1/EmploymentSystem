using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.GatAllVacancies
{
    public class GetVacanciesQuery : IRequest<IEnumerable<Vacancy>>
    {
    }

    public class GetVacanciesQueryHandler : IRequestHandler<GetVacanciesQuery, IEnumerable<Vacancy>>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetVacanciesQueryHandler(IVacancyRepository vacancyRepository, IHttpContextAccessor httpContextAccessor)
        {
            _vacancyRepository = vacancyRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Vacancy>?> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId != null)
            {
                return await _vacancyRepository.GetVacanciesAsync(userId);
            }
            return null;
        }
    }
}
