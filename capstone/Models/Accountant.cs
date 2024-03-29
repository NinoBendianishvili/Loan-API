using capstone.Interfaces;

namespace capstone.Models;
using Microsoft.EntityFrameworkCore;
public class Accountant : Iunifier
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Password { get; set; }
    public string Username { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}