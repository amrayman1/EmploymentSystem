﻿using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Commands.Vacancies.CreateVacancy
{
    public class CreateVacancyCommand : IRequest<Vacancy>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int MaxApplications { get; set; }
        public bool IsActive { get; set; }
        public string EmployerId { get; set; }
    }

    public class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, Vacancy>
    {
        private readonly IVacancyRepository _vacancyRepository;

        public CreateVacancyCommandHandler(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<Vacancy> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = new Vacancy
            {
                Title = request.Title,
                Description = request.Description,
                ExpiryDate = request.ExpiryDate,
                MaxApplications = request.MaxApplications,
                IsActive = request.IsActive,
                EmployerId = request.EmployerId,
                Applications = new List<ApplicationDetails>()
            };

            await _vacancyRepository.AddAsync(vacancy);
            return vacancy;
        }
    }

}