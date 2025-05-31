using Lending.Data;
using Lending.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lending.Controllers.MVC
{
    public class TermController : Controller
    {
        private readonly ApplicationDBContext dbContext;
        public TermController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var terms = await dbContext.Terms.ToListAsync();
            return View(terms);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Term model)
        {
            var term = new Term
            {
                Months = model.Months
            };

            await dbContext.Terms.AddAsync(term);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Term");
        }

    }
}
