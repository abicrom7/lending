using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public class Loan
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string LoanReferenceNumber { get; set; } = string.Empty;

        // Foreign keys
        public int BorrowerId { get; set; }
        public int CollectorId { get; set; }
        public int InterestId { get; set; }

        // Navigation properties
        public Borrower? Borrower { get; set; }
        public Collector? Collector { get; set; }
        public Interest? Interest { get; set; }

        public string? Comaker { get; set; }
        public int LoanPlan { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public decimal MonthlyPay { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string? LoanStatus { get; set; }
        public string? Remarks { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
