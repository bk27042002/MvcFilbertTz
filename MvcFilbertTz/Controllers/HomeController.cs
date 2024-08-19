using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFilbertTz.Models;
using System.Diagnostics;

namespace MvcFilbertTz.Controllers
{
    public class HomeController : Controller
    {
        private readonly CollectContext _context;

        public HomeController(CollectContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.People
                .Select(person => new
                {
                    F = person.F,
                    I = person.I,
                    O = person.O,
                    BirthDate = person.BirthDate,
                    Passports = _context.Passports
                        .Where(p => p.RPersonId == person.Id)
                        .Select(p => new { p.Series, p.Number })
                        .ToList(),
                    Debts = _context.Debts
                        .Where(d => d.RPersonId == person.Id)
                        .Select(d => new
                        {
                            d.DebtSum,
                            ContractDate = d.ContractDt
                        })
                        .ToList()
                })
                .ToListAsync();

            var result = data.SelectMany(person =>
                person.Passports.SelectMany(passport =>
                    person.Debts.Select(debt => new
                    {
                        person.F,
                        person.I,
                        person.O,
                        person.BirthDate,
                        passport.Series,
                        passport.Number,
                        debt.ContractDate,
                        debt.DebtSum
                    })))
                .ToList();

            return View(result);
        }
    }
}
