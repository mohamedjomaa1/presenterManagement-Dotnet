using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionPresentateur.Data;
using GestionPresentateur.Models;

namespace GestionPresentateur.Controllers
{
   
    [Authorize(Roles = "Admin")]
    public class PerformanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerformanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Performance
        public async Task<IActionResult> Index()
        {
            var performances = await _context.Performances
                .Include(p => p.Numero)
                .Include(p => p.Compte)
                .ToListAsync();
            return View(performances);
        }

        // GET: Performance/Create
        public IActionResult Create()
        {
            ViewBag.Numeros = _context.Numeros.ToList();
            ViewBag.Users = _context.Users.ToList(); // AspNetUsers table
            return View();
        }

        // POST: Performance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Performance performance)
        {
            
                _context.Add(performance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewBag.Numeros = _context.Numeros.ToList();
            ViewBag.Users = _context.Users.ToList();
            return View(performance);
        }

        // GET: Performance/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var performance = await _context.Performances.FindAsync(id);
            if (performance == null)
            {
                return NotFound();
            }
            ViewBag.Numeros = _context.Numeros.ToList();
            ViewBag.Users = _context.Users.ToList();
            return View(performance);
        }

        // POST: Performance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Performance performance)
        {
            if (id != performance.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(performance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Performances.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            
            ViewBag.Numeros = _context.Numeros.ToList();
            ViewBag.Users = _context.Users.ToList();
            return View(performance);
        }

        // GET: Performance/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var performance = await _context.Performances
                .Include(p => p.Numero)
                .Include(p => p.Compte)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (performance == null)
            {
                return NotFound();
            }
            return View(performance);
        }

        // POST: Performance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performance = await _context.Performances.FindAsync(id);
            if (performance != null)
            {
                _context.Performances.Remove(performance);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}