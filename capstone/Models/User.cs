using capstone.Interfaces;

namespace capstone.Models;

public class User : Iunifier
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public int Salary { get; set; }
    public bool IsBlocked { get; set; } = false;
    public string Password { get; set; }
    public ICollection<Loan> Loans { get; set; }
}