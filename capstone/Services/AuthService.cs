using capstone.Data;
using capstone.Dto;
using capstone.Interfaces;
using capstone.Models;

namespace capstone.Services;

public class AuthService : IAuthService
{
    private readonly Context _context;
    private readonly IAccountantRepository _accountantRepository;
    private readonly IUserRepository _userRepository;
    

    public AuthService(Context context, IAccountantRepository accountantRepository, IUserRepository userRepository)
    {
        _context = context;
        _accountantRepository = accountantRepository;
        _userRepository = userRepository;
    }
    public Accountant accountantAuth(AccountantAuthDto auth)
    {
        if (_context.Accountants.Select(x => x.Email).Contains(auth.Email))
        {
            return null;
        }
        var acc = _accountantRepository.InsertAccountant(auth);
        return acc;
    }
    
    public User userAuth(UserAuthDto auth)
    {
        if (_context.Users.Select(x => x.Email).Contains(auth.Email))
        {
            return null;
        }
        var acc = _userRepository.insertUser(auth);
        return acc;
    }
}