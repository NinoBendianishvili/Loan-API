using capstone.Dto;
using capstone.Models;

namespace capstone.Interfaces;

public interface ILoanRepository
{
    void AddLoan(int id, LoanDto loan);
    public List<Loan> GetLoansById(int Id);

    public bool DeleteLoanById(int LoanId, int UserId);

    public Boolean modifyLoan(int userId, LoanDto newLoan);

    public ICollection<Loan> GetAllLoans();

}