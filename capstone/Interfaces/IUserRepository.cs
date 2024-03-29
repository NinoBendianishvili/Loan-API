using capstone.Dto;
using capstone.Models;

namespace capstone.Interfaces;

public interface IUserRepository
{
    User GetUserById(int id);
    Boolean UserExists(int id);
    public ICollection<User> GetAllUsers();
    public Boolean BlockUserById(int id);
    public Boolean UnblockUserById(int id);
    public User insertUser(UserAuthDto auth);

}