using EmploymentSystem.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.DeleteVacancy
{
    public class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand, bool>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ILogger<DeleteVacancyCommandHandler> _logger;

        public DeleteVacancyCommandHandler(IVacancyRepository vacancyRepository, ILogger<DeleteVacancyCommandHandler> logger)
        {
            _vacancyRepository = vacancyRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteVacancyCommand for Vacancy Id: {Id}", request.Id);

            var vacancy = await _vacancyRepository.GetByIdAsync(request.Id);

            if (vacancy == null)
            {
                _logger.LogWarning("Vacancy with Id: {Id} not found", request.Id);
                return false;
            }

            await _vacancyRepository.DeleteAsync(vacancy.Id);
            _logger.LogInformation("Vacancy with Id: {Id} deleted successfully", request.Id);

            return true;
        }
    }
}
