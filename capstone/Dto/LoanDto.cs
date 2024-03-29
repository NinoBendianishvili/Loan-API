namespace capstone.Dto;

public class LoanDto
{
    public int LoanId { get; set; }
    public string LoanType { get; set; }
    public int Amount { get; set; }
    public string Currency { get; set; }
    public int Period { get; set; }
}