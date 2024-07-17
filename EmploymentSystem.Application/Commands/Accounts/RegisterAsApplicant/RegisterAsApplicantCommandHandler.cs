using EmploymentSystem.Application.Commands.Accounts.RegisterAsApplicant;
using EmploymentSystem.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Accounts.RegisterAsApplicant
{
    public class RegisterAsApplicantCommandHandler : IRequestHandler<RegisterAsApplicantCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterAsApplicantCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(RegisterAsApplicantCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var roleExists = await _roleManager.RoleExistsAsync("Applicant");
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Applicant"));
                }
                await _userManager.AddToRoleAsync(user, "Applicant");
            }

            return result;
        }
    }
}
