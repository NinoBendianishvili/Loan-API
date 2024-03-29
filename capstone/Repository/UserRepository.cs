using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using capstone.Data;
using capstone.Dto;
using capstone.Interfaces;
using capstone.Models;
using Microsoft.IdentityModel.Tokens;

namespace capstone.Repository;

public class UserRepository : IUserRepository
{
    private readonly Context _context;

    public UserRepository(Context context)
    {
        _context = context;
    }


    public User GetUserById(int id)
    {
        if (_context.Users != null)
            return _context.Users.Where(x => x.Id == id).FirstOrDefault() ?? throw new InvalidOperationException();
        return null;
    }

    public bool UserExists(int id)
    {
        return _context.Users != null && _context.Users.Any(p => p.Id == id);
    }
    
    public ICollection<User> GetAllUsers()
    {
        // Assuming ApplicationDbContext has a Users DbSet
        return _context.Users.Select(u => new User
        {
            Id = u.Id,
            Username = u.Username,
            FirstName = u.FirstName,
            LastName = u.LastName,
            IsBlocked = u.IsBlocked,
            Email = u.Email,
            Loans = u.Loans
            // Map other properties as needed
        }).ToList();
    }

    public Boolean BlockUserById(int id)
    {
        var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
        if (user == null)
        {
            return false;
        }
        user.IsBlocked = true;
        _context.Users.Update(user);
        _context.SaveChanges();
        return true;
    }
    
    public Boolean UnblockUserById(int id)
    {
        var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
        if (user == null)
        {
            return false;
        }
        user.IsBlocked = false;
        _context.Users.Update(user);
        _context.SaveChanges();
        return true;
    }
    
    public User insertUser(UserAuthDto auth)
    {
        var acc = new User
        {
            FirstName = auth.FirstName, LastName = auth.LastName, Email = auth.Email, Username = auth.Username,
            Age = auth.Age, Password = auth.Password, IsBlocked = false, Loans = {}, Salary = auth.Salary
            
        };
        _context.Users.Add(acc);
        _context.SaveChanges();
        return acc;
    }
  
}