using capstone.Dto;
using capstone.Models;

namespace capstone.Interfaces;

public interface IAuthService
{
    public Accountant accountantAuth(AccountantAuthDto auth);
    public User userAuth(UserAuthDto auth);
}