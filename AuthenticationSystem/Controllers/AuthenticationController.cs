using AuthenticationSystem.PolicyAuthorization;
using AuthSystem.Application.Dtos.Request;
using AuthSystem.Application.Dtos.Response;
using AuthSystem.Application.Helpers;
using AuthSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ForgotPasswordRequest = AuthSystem.Application.Dtos.Request.ForgotPasswordRequest;
using ResetPasswordRequest = AuthSystem.Application.Dtos.Request.ResetPasswordRequest;

namespace AuthenticationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IAuthenticationService _authService;
        public IHttpContextAccessor _httpContextAccessor;

        public AuthenticationController(IAuthenticationService authService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost("Registration", Name = "Registration")]
        [SwaggerOperation(Summary = "register a user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "user", Type = typeof(JwtToken))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "user already exist", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Role does not exist", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationRequest request)
        {
            var response = await _authService.Register(request);
            return Ok(response);
        }

        [Authorize(Policy = AuthPolicies.Admin)]
        [HttpPost("BanUser", Name = "BanUser")]
        [SwaggerOperation(Summary = "Ban a user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "user does not exist", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> BanUser([FromBody] BanUserViewModel request)
        {
            await _authService.BanUser(request);
            return Ok();
        }

        [HttpPut("ChangePassword", Name = "ChangePassword")]
        [SwaggerOperation(Summary = "Change user password")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "user email", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "user does not exist", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal error", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var response = await _authService.ChangePassword(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "login")]
        [SwaggerOperation(Summary = "Authenticates user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns jwt token", Type = typeof(JwtToken))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User is banned", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal error", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestView loginRequest)
        {
            var response = await _authService.Login(loginRequest);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword", Name = "ForgotPassword")]
        [SwaggerOperation(Summary = "forgot-password")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a token", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User is banned", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User does not exist", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var response = await _authService.ForgotPassword(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPut("ResetPassword", Name = "ResetPassword")]
        [SwaggerOperation(Summary = "reset-password")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a token", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User does not exist", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid Token", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid operation", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordRequest request)
        {
            var response = await _authService.ResetPassword(request);
            if (response != null)
                return RedirectToAction("LoginUser");

            return BadRequest(response);
        }

        [Authorize(Policy = AuthPolicies.Admin)]
        [HttpGet("GetUser", Name = "GetUser")]
        [SwaggerOperation(Summary = "Get a user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a user", Type = typeof(UserViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User does not exist", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal error", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetUser(string userId)
        {
            var response = await _authService.GetUser(userId);
            return BadRequest(response);
        }
    }
}
