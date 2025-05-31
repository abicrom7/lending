using Lending.Data;
using Lending.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lending.Controllers.MVC;

public class InterestController : Controller
{
    private readonly ApplicationDBContext dbContext;
    public InterestController(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Interest model)
    {
        var interest = new Interest
        {
            InterestPercentage = model.InterestPercentage,
        };

        await dbContext.Interests.AddAsync(interest);
        await dbContext.SaveChangesAsync();

        return RedirectToAction("List", "Interest");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        var interest = await dbContext.Interests.FindAsync(Id);
        return View(interest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Interest model)
    {
        var interest = await dbContext.Interests.FindAsync(model.Id);

        if (interest != null)
        {
            interest.InterestPercentage = model.InterestPercentage;
            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("List", "Interest");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var interests = await dbContext.Interests.ToListAsync();
        return View(interests);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Interest model)
    {
        var interest = await dbContext.Interests
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (interest != null)
        {
            dbContext.Remove(interest);
            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("List", "Interest");
    }
}
