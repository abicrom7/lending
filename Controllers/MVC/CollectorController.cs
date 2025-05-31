using Lending.Data;
using Lending.Models;
using Lending.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lending.Controllers.MVC;
public class CollectorController : Controller
{
    private readonly ApplicationDBContext dbContext;
    public CollectorController(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CollectorViewModel viewModel)
    {
        var collector = new Collector
        {
            Name = viewModel.Name,
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            DateOfBirth = viewModel.DateOfBirth,
            Address = viewModel.Address
        };

        await dbContext.Collectors.AddAsync(collector);
        await dbContext.SaveChangesAsync();

        return RedirectToAction("List", "Collector");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        var collector = await dbContext.Collectors.FindAsync(Id);
        return View(collector);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Collector model)
    {
        var collector = await dbContext.Collectors.FindAsync(model.Id);

        if (collector != null)
        {
            collector.Name = model.Name;
            collector.Email = model.Email;
            collector.PhoneNumber = model.PhoneNumber;
            collector.DateOfBirth = model.DateOfBirth;
            collector.Address = model.Address;

            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("List", "Collector");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var collectors = await dbContext.Collectors.ToListAsync();
        return View(collectors);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Collector model)
    {
        var collector = await dbContext.Collectors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (collector != null)
        {
            dbContext.Remove(collector);
            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("List", "Collectors");
    }
}
