using EmploymentSystem.Core.Interfaces;
using MediatR;
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

        public DeleteVacancyCommandHandler(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<bool> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(request.Id);

            if (vacancy == null)
            {
                // Handle not found case (throw exception or return false)
                return false;
            }

            await _vacancyRepository.DeleteAsync(vacancy.Id);
            return true;
        }
    }
}
