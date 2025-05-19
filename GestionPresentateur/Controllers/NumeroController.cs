using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using GestionPresentateur.Data;
using GestionPresentateur.Models;

namespace GestionPresentateur.Controllers
{
    [Authorize]
    public class NumeroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NumeroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List and Search
        public async Task<IActionResult> Index(string searchString, int? minDuration)
        {
            var numeros = _context.Numeros
                .Include(n => n.Presentateur)
                .ThenInclude(p => p.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                numeros = numeros.Where(n => n.Titre.Contains(searchString));
                ViewData["CurrentFilter"] = searchString;
            }

            if (minDuration.HasValue)
            {
                numeros = numeros.Where(n => n.Duree >= minDuration.Value);
                ViewData["CurrentDuration"] = minDuration;
            }

            return View(await numeros.ToListAsync());
        }

        // Buy Ticket
        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            var numero = await _context.Numeros.FindAsync(id);
            if (numero == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var performance = new Performance
            {
                CodeN = id,
                CompteId = userId,
                PerformanceDate = DateTime.Now
            };

            _context.Performances.Add(performance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Admin CRUD: Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Presentateurs = _context.Presentateurs.ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Numero numero)
        {
            
                _context.Add(numero);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Number created successfully!";
                return RedirectToAction(nameof(Index));
            
            ViewBag.Presentateurs = _context.Presentateurs.ToList();
            return View(numero);
        }

        // Admin CRUD: Edit
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Edit(int id)
        {
            var numero = await _context.Numeros.FindAsync(id);
            if (numero == null)
            {
                return NotFound();
            }
            ViewBag.Presentateurs = _context.Presentateurs.ToList();
            return View(numero);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Numero numero)
        {
            if (id != numero.CodeN)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Numeros.Any(e => e.CodeN == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            
            ViewBag.Presentateurs = _context.Presentateurs.ToList();
            return View(numero);
        }

        // Admin CRUD: Delete
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Delete(int id)
        {
            var numero = await _context.Numeros.FindAsync(id);
            if (numero == null)
            {
                return NotFound();
            }
            return View(numero);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var numero = await _context.Numeros.FindAsync(id);
            if (numero != null)
            {
                _context.Numeros.Remove(numero);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}