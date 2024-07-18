using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetVacanciesQueryHandler> _logger;

        public GetVacanciesQueryHandler(IVacancyRepository vacancyRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ILogger<GetVacanciesQueryHandler> logger)
        {
            _vacancyRepository = vacancyRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Vacancy>?> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    throw new Exception("User is not authenticated.");
                }

                var user = await _userRepository.GetByEmailAsync(userIdClaim);
                if (user != null)
                {
                    _logger.LogInformation("Fetching vacancies for user: {UserId}", user.Id);
                    return await _vacancyRepository.GetVacanciesAsync(user.Id);
                }

                _logger.LogWarning("User not found while fetching vacancies.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching vacancies.");
                throw;
            }
        }
    }
}
