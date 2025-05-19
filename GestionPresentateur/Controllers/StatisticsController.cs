using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionPresentateur.Data;

namespace GestionPresentateur.Controllers
{
  

    [Authorize(Roles = "Admin")] 
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stats = new
            {
                TotalPresentateurs = await _context.Presentateurs.CountAsync(),
                TotalNumeros = await _context.Numeros.CountAsync(),
                TotalPerformances = await _context.Performances.CountAsync(),
                AverageDuration = await _context.Numeros.AnyAsync() ? await _context.Numeros.AverageAsync(n => n.Duree) : 0,
                TotalRevenue = await _context.Performances
                    .Join(_context.Numeros,
                        p => p.CodeN,
                        n => n.CodeN,
                        (p, n) => n.Presentateur.Role.Prix)
                    .DefaultIfEmpty()
                    .SumAsync()
            };
            return View(stats);
        }
    }
}