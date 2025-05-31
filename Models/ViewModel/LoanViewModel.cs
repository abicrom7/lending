using System.ComponentModel.DataAnnotations;

namespace Lending.Models.ViewModel;

public class LoanViewModel
{
    public int Id { get; set; }
    public string LoanReferenceNumber { get; set; } = string.Empty;
    // Navigation properties
    public Borrower? Borrower { get; set; }
    //public Collector? Collector { get; set; }
    public Interest? Interest { get; set; }
    public int BorrowerId { get; set; }
    public string? BorrowerName { get; set; }
    public int CollectorId { get; set; }
    public int InterestId { get; set; }
    public int InterestPercentage { get; set; }
    public string? Comaker { get; set; }
    public int LoanPlan { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public decimal MonthlyPay { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? LoanStatus { get; set; }
    public string? Remarks { get; set; }

}
