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
    public class GetVacanciesQueryHandler : IRequestHandler<GetVacanciesQuery, IEnumerable<Vacancy>>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public GetVacanciesQueryHandler(IVacancyRepository vacancyRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _vacancyRepository = vacancyRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Vacancy>?> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("User is not authenticated.");
            }

            var user = await _userRepository.GetByEmailAsync(userIdClaim);
            if (user != null)
            {
                return await _vacancyRepository.GetVacanciesAsync(user.Id);
            }
            return null;
        }
    }
}
