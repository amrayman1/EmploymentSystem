using EmploymentSystem.Application.Commands.Accounts.RegisterAsEmployer;
using EmploymentSystem.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Application.Features.Accounts.RegisterAsEmployer
{
    public class RegisterAsEmployerCommandHandler : IRequestHandler<RegisterAsEmployerCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterAsEmployerCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(RegisterAsEmployerCommand request, CancellationToken cancellationToken)
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
                var roleExists = await _roleManager.RoleExistsAsync("Employer");
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Employer"));
                }
                await _userManager.AddToRoleAsync(user, "Employer");
            }

            return result;
        }
    }
}
