using Lending.Data;
using Lending.Migrations;
using Lending.Models;
using Lending.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lending.Controllers.MVC;

public class LoanController : Controller
{
    private readonly ApplicationDBContext dbContext;
    public LoanController(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    public async Task<IActionResult> Edit(int id)
    {
        var loan = await dbContext.Loans.FindAsync(id);

        var viewModel = new LoanViewModel
        {
            Id = loan.Id,
            LoanReferenceNumber = loan.LoanReferenceNumber,
            BorrowerId = loan.BorrowerId,
            CollectorId = loan.CollectorId,
            InterestId = loan.InterestId,
            Comaker = loan.Comaker,
            LoanPlan = loan.LoanPlan,
            EffectiveDate = loan.EffectiveDate,
            MonthlyPay = loan.MonthlyPay,
            PrincipalAmount = loan.PrincipalAmount,
            TotalAmount = loan.TotalAmount,
            LoanStatus = loan.LoanStatus,
            Remarks = loan.Remarks
        };

        return View(viewModel); 
    }

    [HttpGet]
    public IActionResult List()
    {
        var loans = dbContext.Loans
            .Include(l => l.Borrower) 
            .Include(l => l.Interest)
            .Select(l => new LoanViewModel
            {
                Id = l.Id,
                LoanReferenceNumber = l.LoanReferenceNumber,
                BorrowerId = l.BorrowerId,
                BorrowerName = l.Borrower.Name,
                CollectorId = l.CollectorId,
                InterestId = l.InterestId,
                InterestPercentage = l.Interest.InterestPercentage,
                Comaker = l.Comaker,
                LoanPlan = l.LoanPlan,
                EffectiveDate = l.EffectiveDate,
                MonthlyPay = l.MonthlyPay,
                PrincipalAmount = l.PrincipalAmount,
                TotalAmount = l.TotalAmount,
                LoanStatus = l.LoanStatus,
                Remarks = l.Remarks
            })
            .ToList();

        return View(loans);
    }
}
