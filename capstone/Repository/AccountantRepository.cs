using capstone.Data;
using capstone.Dto;
using capstone.Interfaces;
using capstone.Models;

namespace capstone.Repository;

public class AccountantRepository : IAccountantRepository
{
    private readonly Context _context;

    public AccountantRepository(Context context)
    {
        _context = context;
    }
    
    public Accountant GetAccountantById(int id)
    {
        if (_context.Accountants != null)
            return _context.Accountants.Where(x => x.Id == id).FirstOrDefault() ?? throw new InvalidOperationException();
        return null;
    }
    
    public ICollection<Loan> GetAllLoans()
    {
        // Retrieve all loans from the database
        return _context.Loans.ToList();
    }

    public Accountant InsertAccountant(AccountantAuthDto auth)
    {
        var acc = new Accountant
        {
            FirstName = auth.FirstName, LastName = auth.LastName, Email = auth.Email, Username = auth.Username,
            Age = auth.Age, Password = auth.Password
            
        };
        _context.Accountants.Add(acc);
        _context.SaveChanges();
        return acc;
    }

}