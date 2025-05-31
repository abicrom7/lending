using Lending.Data;
using Lending.Models;
using Lending.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Lending.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public LoanController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/LoanApi/borrowers
        [HttpGet("borrowers")]
        public IActionResult GetBorrowers()
        {
            var borrowers = _context.Borrowers
                .Select(b => new
                {
                    id = b.Id,
                    name = b.Name
                })
                .ToList();

            return Ok(borrowers);
        }

        // GET: api/LoanApi/collectors
        [HttpGet("collectors")]
        public IActionResult GetCollectors()
        {
            var collectors = _context.Collectors
                .Select(c => new
                {
                    id = c.Id,
                    name = c.Name
                })
                .ToList();

            return Ok(collectors);
        }

        // GET: api/LoanApi/interests
        [HttpGet("interests")]
        public IActionResult GetInterests()
        {
            var interests = _context.Interests
                .Select(i => new
                {
                    id = i.Id,
                    interest = i.InterestPercentage
                })
                .ToList();

            return Ok(interests);
        }

        // GET: api/LoanApi/terms
        [HttpGet("terms")]
        public IActionResult GetTerms()
        {
            var terms = _context.Terms
                .Select(t => new
                {
                    id = t.Id,
                    months = t.Months
                })
                .ToList();

            return Ok(terms);
        }

        [HttpPost("loans")]
        public async Task<IActionResult> AddLoan([FromBody] LoanViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loan = new Loan
            {
                Id = viewModel.Id,
                LoanReferenceNumber = viewModel.LoanReferenceNumber,
                BorrowerId = viewModel.BorrowerId,
                CollectorId = viewModel.CollectorId,
                InterestId = viewModel.InterestId,
                Comaker = viewModel.Comaker,
                LoanPlan = viewModel.LoanPlan,
                EffectiveDate = viewModel.EffectiveDate,
                MonthlyPay = viewModel.MonthlyPay,
                PrincipalAmount = viewModel.PrincipalAmount,
                TotalAmount = viewModel.TotalAmount,
                Remarks = viewModel.Remarks,
                LoanStatus = "Pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            if (loan.Id > 0)
            {
                // Edit mode
                var existing = await _context.Loans.FindAsync(loan.Id);
                if (existing == null)
                    return NotFound();

                _context.Entry(existing).CurrentValues.SetValues(loan);
            }
            else
            {
                // Add mode
                _context.Loans.Add(loan);
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Loan saved successfully", loanId = loan.Id });
        }
    }
}





