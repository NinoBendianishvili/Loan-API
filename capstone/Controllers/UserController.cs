using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using capstone.Dto;
using capstone.Helpers;
using capstone.Interfaces;
using capstone.Models;
using capstone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace capstone.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly UserService _userService;
        private readonly AppSettings _appSettings;
        private readonly TokenService _tokenService;
        private readonly IAuthService _authService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, ILoanRepository loanRepository, UserService userService, IOptions<AppSettings> appSettings, TokenService tokenService, IAuthService authService)
        {
            _userRepository = userRepository;
            _loanRepository = loanRepository;
            _userService = userService;
            _appSettings = appSettings.Value;
            _tokenService = tokenService;
            _authService = authService;
            _logger = logger;
        }
        
        [HttpGet("Id")]
        public IActionResult GetUser(int Id)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            _logger.LogInformation("token is extracted");
            if (!_tokenService.GetRoleFromToken(token).Equals("Accountant"))
            {
                _logger.LogInformation("Users don't have access, request denied");
                return Unauthorized(new { message = "Users doesn't have access on this information" });
            }
            _logger.LogInformation("trying to get user by id");
            if (!_userRepository.UserExists(Id))
            {
                _logger.LogError("User Does not exist");
                return NotFound();                
            }


            var user = _userRepository.GetUserById(Id);

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("model state is not valid");
                return BadRequest(ModelState);                
            }

            _logger.LogInformation("user successfully retrieved");
            return Ok(user);
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginInput loginUser)
        {
            var user = _userService.Login(loginUser);
            if (user == null)
            {
                _logger.LogError("could not log in, incorrect username or password");
                return BadRequest(new { message = "Incorrect UserName or Password" });
            }
            _logger.LogInformation("user logged in successfully");
            var tokenString = _tokenService.GenerateToken(user);
            _logger.LogInformation("token generated successfully");
            return Ok(
                new
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    token = tokenString
                });
        }
        
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserAuthDto auth)
        {
            _logger.LogInformation("trying to authenticate user");
            var result = _authService.userAuth(auth);
            if (result == null)
            {
                _logger.LogError("could not authenticate, Email already registered");
                return BadRequest(new { message = "This Email is already registered" });
            }
            _logger.LogInformation("user authenticated successfully");
            var tokenString = _tokenService.GenerateToken(result);
            _logger.LogInformation("token generated successfully");
            return Ok(
                new
                {
                    result.Username,
                    result.FirstName,
                    result.LastName,
                    result.Email,
                    result.Id,
                    token = tokenString
                });
        }
        
    }


    
}