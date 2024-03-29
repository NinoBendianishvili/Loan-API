using capstone.Dto;
using capstone.Interfaces;
using capstone.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capstone.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountantController : ControllerBase
{
    
    private readonly ILoanRepository _loanRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AccountantController> _logger;

    public AccountantController(ILogger<AccountantController> logger, ILoanRepository loanRepository, IUserRepository userRepository, IAuthService authService, ITokenService tokenService)
    {
        _loanRepository = loanRepository;
        _userRepository = userRepository;
        _authService = authService;
        _tokenService = tokenService;
        _logger = logger;
    }
    

    [HttpGet("GetAllLoans")]
    public IActionResult GetAllLoans()
    {
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        _logger.LogInformation("token is extracted");
        if (!_tokenService.GetRoleFromToken(token).Equals("Accountant"))
        {
            _logger.LogInformation("Users don't have access, request denied");
            return Unauthorized(new { message = "Users doesn't have access on this information" });
        }
        _logger.LogInformation("getting all loans");
        var loans = _loanRepository.GetAllLoans();
        _logger.LogInformation("Loan Repo function succeed, Loans returned");
        return Ok(loans);
    }
    
    [HttpGet("GetAllUsers")]
    public IActionResult GetAllUsers()
    {
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        _logger.LogInformation("token is extracted");
        if (!_tokenService.GetRoleFromToken(token).Equals("Accountant"))
        {
            _logger.LogInformation("Users don't have access, request denied");
            return Unauthorized(new { message = "Users doesn't have access on this information" });
        }
        _logger.LogInformation("Getting all users");
        var users = _userRepository.GetAllUsers();
        _logger.LogInformation("User Repo function succeed, Users returned");
        return Ok(users);
    }

    [HttpPost("BlockUser")]
    public IActionResult BlockUser(int id)
    {
        _logger.LogInformation("trying to block user");
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        _logger.LogInformation("token is extracted");
        if (!_tokenService.GetRoleFromToken(token).Equals("Accountant"))
        {
            _logger.LogInformation("Users don't have access, request denied");
            return Unauthorized(new { message = "Users doesn't have access on this information" });
        }
        var result = _userRepository.BlockUserById(id);
        if (result)
        {
            _logger.LogInformation("User is blocked successfully");
            return Ok("user blocked successfully");
        }

        _logger.LogError("error occured while blocking a user");
        return StatusCode(500, new { message = "An error occurred while blocking a user" });
    }
    
    [HttpPost("UnblockUser")]
    public IActionResult UnblockUser(int id)
    {
        _logger.LogInformation("trying to unblock user");
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        _logger.LogInformation("token is extracted");
        if (!_tokenService.GetRoleFromToken(token).Equals("Accountant"))
        {
            _logger.LogInformation("Users don't have access, request denied");
            return Unauthorized(new { message = "Users doesn't have access on this information" });
        }
        var result = _userRepository.UnblockUserById(id);
        if (result)
        {
            _logger.LogInformation("User is unblocked successfully");
            return Ok("user unblocked successfully");
        }
        _logger.LogError("error occured while unblocking a user");
        return StatusCode(500, new { message = "An error occurred while blocking a user" });
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public IActionResult Authenticate([FromBody] AccountantAuthDto auth)
    {
        var result = _authService.accountantAuth(auth);
        if (result == null)
        {
            _logger.LogError("error occured while Authenticating");
            return BadRequest(new { message = "Incorrect UserName or Password" });
        }
        var tokenString = _tokenService.GenerateToken(result);
        _logger.LogInformation("Successfully authenticated");
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