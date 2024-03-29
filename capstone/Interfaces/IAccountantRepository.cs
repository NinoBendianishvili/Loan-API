using capstone.Dto;
using capstone.Models;

namespace capstone.Interfaces;

public interface IAccountantRepository
{
    public Accountant InsertAccountant(AccountantAuthDto auth);
    public Accountant GetAccountantById(int id);
    public ICollection<Loan> GetAllLoans();

}