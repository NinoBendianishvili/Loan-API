namespace capstone.Models;

public class Loan
{

    public int LoanId { get; set; }
    public string LoanType { get; set; }
    public int Amount { get; set; }
    public string Currency { get; set; }
    public int Period { get; set; }
    public String Status { get; set; }
    public int UserId { get; set; }
}