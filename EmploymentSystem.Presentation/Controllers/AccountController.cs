using EmploymentSystem.Application.Commands.Accounts.Login;
using EmploymentSystem.Application.Commands.Accounts.RegisterAsApplicant;
using EmploymentSystem.Application.Commands.Accounts.RegisterAsEmployer;
using EmploymentSystem.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmploymentSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IMediator mediator, SignInManager<User> signInManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;

        }

        [HttpPost("RegisterAsEmployer")]
        public async Task<IActionResult> RegisterAsEmployer(RegisterAsEmployerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("User registered successfully");
        }

        [HttpPost("RegisterAsApplicant")]
        public async Task<IActionResult> RegisterAsApplicant(RegisterAsApplicantCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _mediator.Send(command);
            if (token == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("User logged out successfully");
        }
    }

}
