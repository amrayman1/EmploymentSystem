﻿using EmploymentSystem.Application.Commands.Vacancies.ApplyForVacancy;
using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Vacancies.ApplyForVacancy
{
    public class ApplyForVacancyCommandHandler : IRequestHandler<ApplyForVacancyCommand, ApplicationDetails>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IVacancyRepository _vacancyRepository;

        public ApplyForVacancyCommandHandler(IApplicationRepository applicationRepository, IVacancyRepository vacancyRepository)
        {
            _applicationRepository = applicationRepository;
            _vacancyRepository = vacancyRepository;
        }

        public async Task<ApplicationDetails> Handle(ApplyForVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(request.VacancyId);
            if (vacancy == null || !vacancy.IsActive || vacancy.ExpiryDate < DateTime.Now)
            {
                throw new Exception("Vacancy is not available.");
            }

            var applicationCount = await _applicationRepository.GetApplicationCountByVacancyIdAsync(request.VacancyId);
            if (applicationCount >= vacancy.MaxApplications)
            {
                throw new Exception("Maximum number of applications reached for this vacancy.");
            }

            var hasAppliedToday = await _applicationRepository.HasAppliedTodayAsync(request.ApplicantId);
            if (hasAppliedToday)
            {
                throw new Exception("You can only apply for one vacancy per day.");
            }

            var application = new ApplicationDetails
            {
                VacancyId = request.VacancyId,
                ApplicantId = request.ApplicantId,
                ApplicationDate = DateTime.Now
            };

            await _applicationRepository.AddAsync(application);
            return application;
        }
    }
}