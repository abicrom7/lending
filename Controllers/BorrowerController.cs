using Lending.Data;
using Lending.Models;
using Lending.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lending.Controllers;

public class BorrowerController : Controller
{
    private readonly ApplicationDBContext dbContext;

    public BorrowerController(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(BorrowerViewModel viewModel)
    {
        var borrower = new Borrower
        {
            Name = viewModel.Name,
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            DateOfBirth = viewModel.DateOfBirth,
            Address = viewModel.Address
        };

        await dbContext.Borrowers.AddAsync(borrower);
        await dbContext.SaveChangesAsync();

        return RedirectToAction("List", "Borrower");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
       var borrower = await dbContext.Borrowers.FindAsync(Id);
        return View(borrower);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var borrowers = await dbContext.Borrowers.ToListAsync();
        return View(borrowers);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Borrower model)
    {
        var borrower = await dbContext.Borrowers.FindAsync(model.Id);

        if(borrower != null)
        {
            borrower.Name = model.Name;
            borrower.Email = model.Email;
            borrower.PhoneNumber = model.PhoneNumber;
            borrower.DateOfBirth = model.DateOfBirth;
            borrower.Address = model.Address;

            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("List","Borrower");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Borrower model)
    {
        var borrower = await dbContext.Borrowers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (borrower != null)
        {
            dbContext.Remove(borrower);
            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("List", "Borrower");
    }
}
