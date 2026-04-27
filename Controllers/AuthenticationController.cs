using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DevRequestPortal.Models;
using DevRequestPortal.Helpers;

namespace DevRequestPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly WindowsAuthHelper _windowsAuth;
        private readonly JwtHelper _jwt;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            WindowsAuthHelper windowsAuth,
            JwtHelper jwt, 
            ILogger<AuthenticationController> logger)
        {
            _windowsAuth = windowsAuth;
            _jwt = jwt;
            _logger = logger;
        }

        [HttpGet("sso")]
        [Authorize(AuthenticationSchemes = "Negotiate")]
        public IActionResult Sso()
        {
            var rawUsername = User.Identity?.Name;
            if (string.IsNullOrEmpty(rawUsername))
                return Unauthorized(new
                {
                    error = "Windows identity not found"
                });

            var username = rawUsername.Contains('\\')
                ? rawUsername.Split('\\', 2)[1]
                : rawUsername;

            _logger.LogInformation("SSO success for: {User}", username);
            return Ok(new AuthResponse
            {
                Token = _jwt.GenerateToken(username), 
                Username = username
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] CredentialRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new
                {
                    error = "Username and password are required"
                });

            if (!_windowsAuth.ValidateCredentials(request.Username, request.Password))
            {
                _logger.LogWarning("Login failed for: {User}", request.Username);
                return Unauthorized(new
                {
                    error = "Invalid username or password"
                });
            }

            _logger.LogInformation("Login success for: {User}", request.Username);
            return Ok(new AuthResponse
            {
                Token = _jwt.GenerateToken(request.Username), 
                Username = request.Username
            });
        }
    }
}