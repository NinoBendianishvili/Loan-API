using capstone.Helpers;
using capstone.Interfaces;
using capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using capstone.Dto;

namespace capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase 
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly ITokenService _tokenService;
        private readonly AppSettings _appSettings;
        private readonly ILogger<LoanController> _logger;

        public LoanController(IUserRepository userRepository, ILoanRepository loanRepository,
            ITokenService tokenService, AppSettings appSettings, ILogger<LoanController> logger)
        {
            _userRepository = userRepository;
            _loanRepository = loanRepository;
            _tokenService = tokenService;
            _appSettings = appSettings;
            _logger = logger;
        }

        [HttpPost("addLoan")]
        [Authorize]
        public IActionResult AddLoan([FromBody] LoanDto loan)
        {
            try
            {
                _logger.LogInformation("trying to add loan");
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                _logger.LogInformation("token is extracted");
                if (!_tokenService.GetRoleFromToken(token).Equals("User"))
                {
                    _logger.LogInformation("Accountants don't have access, request denied");
                    return Unauthorized(new { message = "Accountants doesn't have access on this information" });
                }

                var userId =
                    _tokenService.GetUserIdFromToken(token); 
                if (!userId.HasValue)
                {
                    _logger.LogError("User Id could not be extracted");
                    return Unauthorized(new { message = "Invalid token" });
                }
                _logger.LogInformation("User Id is extracted");
                _loanRepository.AddLoan(userId.Value, loan);
                _logger.LogInformation("Loan added successfully");

                return Ok(new { message = "Loan added successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding the loan");
                return StatusCode(500, new { message = "An error occurred while adding the loan" });
            }
        }

        [HttpGet("showLoans")]
        [Authorize]
        public IActionResult ShowLoans()
        {
            try
            {
                _logger.LogInformation("trying to show loans");
                
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                _logger.LogInformation("token is extracted");
                if (!_tokenService.GetRoleFromToken(token).Equals("User"))
                {
                    _logger.LogInformation("Accountants don't have access, request denied");
                    return Unauthorized(new { message = "Accountants doesn't have access on this information" });
                }
                _logger.LogInformation("Accountants have access, request continued");
                var userId =
                    _tokenService.GetUserIdFromToken(token); // Assuming _tokenService and _appSettings are injected

                if (!userId.HasValue)
                {
                    _logger.LogError("User Id could not be extracted");
                    return Unauthorized(new { message = "Invalid token" }); // Corrected to return Unauthorized
                }

                _logger.LogInformation("User Id is extracted");
                var loans = _loanRepository
                    .GetLoansById(userId.Value); // Assuming GetLoansByUserId is implemented in your repository
                _logger.LogInformation("Loans retrieved successfully");

                return Ok(loans);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving the loans");
                return StatusCode(500, new { message = "An error occurred while retrieving the loans" });
            }
        }
        
        [HttpDelete("DeleteLoan")]
        [Authorize]
        public IActionResult DeleteLoanById(int loanId)
        {
            //both, user and accountant has access on this function
            _logger.LogInformation("trying to delete loans");
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            _logger.LogInformation("token is extracted");
            var userId =
                _tokenService.GetUserIdFromToken(token); // Assuming _tokenService and _appSettings are injected

            if (!userId.HasValue)
            {
                _logger.LogError("User Id could not be extracted");
                return Unauthorized(new { message = "Invalid token" }); // Corrected to return Unauthorized
            }
            _logger.LogInformation("User Id is extracted");
            
            var result = _loanRepository.DeleteLoanById(loanId, userId.Value);
            if (result)
            {
                _logger.LogInformation("Loan is successfully deleted");
                return Ok("Loan deleted successfully.");
            }
            _logger.LogError("Loan with the given id not found.");
            return NotFound("Loan with the given id not found.");
        }
        
        [HttpPatch("ModifyLoan")]
        [Authorize]
        public IActionResult ModifyLoan([FromBody] LoanDto newLoan)
        {
            try
            {
                //both, user and accountant has access on this function
                _logger.LogInformation("trying to modify loans");
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                _logger.LogInformation("token is extracted");
                var userId =
                    _tokenService.GetUserIdFromToken(token); // Assuming _tokenService and _appSettings are injected

                if (!userId.HasValue)
                {
                    _logger.LogError("User Id could not be extracted");
                    return Unauthorized(new { message = "Invalid token" }); // Corrected to return Unauthorized
                }
                _logger.LogInformation("User Id is extracted");

                var result = _loanRepository.modifyLoan(userId.Value, newLoan);

                if (result)
                {
                    _logger.LogInformation("Loan modified successfully.");
                    return Ok("Loan modified successfully.");
                }
                _logger.LogError("Loan with the given id not found.");
                return NotFound("Loan with the given id not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the loan" });
            }
            
            
        }
    }
}
