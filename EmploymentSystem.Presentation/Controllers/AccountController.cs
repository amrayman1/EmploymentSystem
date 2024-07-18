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
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost("RegisterAsEmployer")]
        public async Task<IActionResult> RegisterAsEmployer(RegisterAsEmployerCommand command)
        {
            _logger.LogInformation("RegisterAsEmployer action started.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for RegisterAsEmployer.");
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                _logger.LogWarning("RegisterAsEmployer failed: {Errors}", string.Join(", ", result.Errors));
                return BadRequest(result.Errors);
            }

            _logger.LogInformation("User registered as Employer successfully.");
            return Ok("User registered successfully");
        }

        [HttpPost("RegisterAsApplicant")]
        public async Task<IActionResult> RegisterAsApplicant(RegisterAsApplicantCommand command)
        {
            _logger.LogInformation("RegisterAsApplicant action started.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for RegisterAsApplicant.");
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                _logger.LogWarning("RegisterAsApplicant failed: {Errors}", string.Join(", ", result.Errors));
                return BadRequest(result.Errors);
            }

            _logger.LogInformation("User registered as Applicant successfully.");
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            _logger.LogInformation("Login action started for user: {Email}", command.Email);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for Login.");
                return BadRequest(ModelState);
            }

            var token = await _mediator.Send(command);

            if (token == null)
            {
                _logger.LogWarning("Login failed for user: {Email}", command.Email);
                return Unauthorized("Invalid credentials.");
            }

            _logger.LogInformation("User logged in successfully: {Email}", command.Email);
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("Logout action started.");

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out successfully.");
            return Ok("User logged out successfully");
        }
    }

}
