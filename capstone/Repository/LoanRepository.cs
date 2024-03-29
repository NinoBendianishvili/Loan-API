using capstone.Data;
using capstone.Dto;
using capstone.Interfaces;
using capstone.Models;

namespace capstone.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly Context _context;
        private readonly IUserRepository _userRepository; // Inject IUserRepository

        public LoanRepository(Context context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public void AddLoan(int userId, LoanDto loan)
        {
            var user = _userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            if (user.IsBlocked)
            {
                return;
            }

            var newLoan = new Loan
            {
                Amount = loan.Amount,
                Currency = loan.Currency,
                LoanType = loan.LoanType,
                Period = loan.Period,
                Status = "In Progress",
                UserId = user.Id // Assuming 'User' is the navigation property in the Loan class
            };

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Loans.Add(newLoan);
                    user.Loans.Add(newLoan);
                    _context.Users.Update(user);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback transaction if an exception occurs
                    // Log or handle the exception
                    throw; // Re-throw the exception for higher-level handling
                }
            }

        }

        public List<Loan> GetLoansById(int Id)
        {
            var user = _userRepository.GetUserById(Id);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            
            return _context.Loans.Where(loan => loan.UserId == Id).ToList();
        }
        
        public bool DeleteLoanById(int LoanId, int UserId)
        {
            var loanToRemove = _context.Loans.Where(loan => loan.LoanId == LoanId).FirstOrDefault();
            var user = _userRepository.GetUserById(UserId);
            if (loanToRemove != null && loanToRemove.UserId == UserId && loanToRemove.Status.Equals("In Progress"))
            {
                _context.Remove(loanToRemove);
                user.Loans.Remove(loanToRemove);
                _context.Users.Update(user);
                _context.SaveChanges();
                return true; // Loan deleted successfully
            }
            return false; // Loan with the given id not found
        }

        public Boolean modifyLoan(int userId, LoanDto newLoan)
        {
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var oldLoan = _context.Loans.Where(x => x.LoanId == newLoan.LoanId).FirstOrDefault();
            if (oldLoan != null && user != null && oldLoan.Status.Equals("In Progress"))
            {
                oldLoan.LoanType = newLoan.LoanType;
                oldLoan.Amount = newLoan.Amount;
                oldLoan.Currency = newLoan.Currency;
                oldLoan.Period = newLoan.Period;
                _context.Loans.Update(oldLoan);
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
        public ICollection<Loan> GetAllLoans()
        {
            // Retrieve all loans from the database
            return _context.Loans.ToList();
        }
    }
    
    
}