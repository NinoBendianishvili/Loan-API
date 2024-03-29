using capstone.Data;
using capstone.Models;

namespace capstone;

public class Seed
{
    private readonly Context _context;

    public Seed(Context context)
    {
        _context = context;
    }

    public void SeedContext()
    {
        if (!_context.Accountants.Any())
        {
            
            var accountants = new List<Accountant>()
            {
                new Accountant { FirstName = "Dwight", LastName = "Schrute", Email = "Dwight.Schrute@gmail.com", Username = "dwightschrute", Age = 41, Password = "personalitytheft"},
                new Accountant { FirstName = "Pam", LastName = "Beesly", Email = "Pam.Beesly@gmail.com", Username = "pambeesly", Age = 34, Password = "receptionist"},
                new Accountant { FirstName = "Jim", LastName = "Halpert", Email = "Jim.Halpert@gmail.com", Username = "jimhalpert", Age = 36, Password = "paper"},
                new Accountant { FirstName = "Stanley", LastName = "Hudson", Email = "Stanley.Hudson@gmail.com", Username = "stanleyhudson", Age = 58, Password = "crossword"},
                new Accountant { FirstName = "Kevin", LastName = "Malone", Email = "Kevin.Malone@gmail.com", Username = "kevinmalone", Age = 42, Password = "chili"},
                new Accountant { FirstName = "Angela", LastName = "Martin", Email = "Angela.Martin@gmail.com", Username = "angelamartin", Age = 39, Password = "sprinkles"}
            };
            _context.Accountants.AddRange(accountants);
            _context.SaveChanges();
        }

        if (!_context.Users.Any())
        {
        
            var users = new List<User>()
            {
                new User
                {
                    Age = 35, Email = "Emma.Stone@gmail.com", FirstName = "Emma", IsBlocked = false, LastName = "Stone",
                    Password = "qwerty12", Salary = 2000, Username = "emmastone24", Loans =
                    {
                        new Loan{Amount = 2000, Currency = "USD", LoanType = "Standard", Period = 12, Status = "In Progress", UserId = 1},
                        new Loan{Amount = 6000, Currency = "USD", LoanType = "Standard", Period = 18, Status = "In Progress", UserId = 1}
                    }
                },
                new User
                {
                    Age = 30, Email = "Elle.Fanning.com", FirstName = "Elle", IsBlocked = false, LastName = "Fanning",
                    Password = "qwerty34", Salary = 3000, Username = "ElleFan245"
                },
                new User
                {
                    Age = 35, Email = "Kevin.Parker@gmail.com", FirstName = "Kevin", IsBlocked = false,
                    LastName = "Parker", Password = "qwerty56", Salary = 4000, Username = "Kevinparr12", Loans = {new Loan{Amount = 4000, Currency = "EUR", LoanType = "Standard", Period = 15, Status = "Processed", UserId = 3}}
                },
                new User
                {
                    Age = 35, Email = "Mark.Ruffalo@gmail.com", FirstName = "Mark", IsBlocked = false,
                    LastName = "Ruffalo", Password = "qwerty78", Salary = 3000, Username = "Markruff01", Loans = {new Loan{Amount = 3000, Currency = "GEL", LoanType = "Standard", Period = 10, Status = "Processed", UserId = 4}}
                },
                new User
                {
                    Age = 35, Email = "Demi.Lovato@gmail.com", FirstName = "Demi", IsBlocked = false,
                    LastName = "Lovato", Password = "qwerty90", Salary = 6000, Username = "Demilov33"
                },
                new User
                {
                    Age = 35, Email = "Madona.Bulia@gmail.com", FirstName = "Madona", IsBlocked = false,
                    LastName = "Bulia", Password = "qwerty01", Salary = 8000, Username = "madddd24"
                }
            };
            _context.Users.AddRange(users);
            _context.SaveChanges();
        }
        
        if (!_context.Loans.Any())
        {

            var loans = new List<Loan>()
            {
                new Loan{Amount = 2000, Currency = "USD", LoanType = "Standard", Period = 12, Status = "In Progress", UserId = 1},
                new Loan{Amount = 4000, Currency = "EUR", LoanType = "Standard", Period = 15, Status = "Processed", UserId = 3},
                new Loan{Amount = 6000, Currency = "USD", LoanType = "Standard", Period = 18, Status = "In Progress", UserId = 1},
                new Loan{Amount = 3000, Currency = "GEL", LoanType = "Standard", Period = 10, Status = "Processed", UserId = 4}
            };
            
            _context.Loans.AddRange(loans);
            _context.SaveChanges();
        }
        
        
    }
}

