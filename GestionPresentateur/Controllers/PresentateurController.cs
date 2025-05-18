using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionPresentateur.Data;
using GestionPresentateur.Models;
using System.Linq;

namespace GestionPresentateur.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Admin")] ----------------------------------------
    public class PresentateurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresentateurController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Presentateur
        public async Task<IActionResult> Index()
        {
            var presentateurs = await _context.Presentateurs
                .Include(p => p.Role)
                .ToListAsync();
            return View(presentateurs);
        }

        // GET: Presentateur/Create
        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        // POST: Presentateur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Presentateur presentateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(presentateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(presentateur);
        }

        // GET: Presentateur/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var presentateur = await _context.Presentateurs.FindAsync(id);
            if (presentateur == null)
            {
                return NotFound();
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(presentateur);
        }

        // POST: Presentateur/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Presentateur presentateur)
        {
            if (id != presentateur.CodeP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presentateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Presentateurs.Any(e => e.CodeP == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(presentateur);
        }

        // GET: Presentateur/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var presentateur = await _context.Presentateurs
                .Include(p => p.Role)
                .FirstOrDefaultAsync(p => p.CodeP == id);
            if (presentateur == null)
            {
                return NotFound();
            }
            return View(presentateur);
        }

        // POST: Presentateur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presentateur = await _context.Presentateurs.FindAsync(id);
            if (presentateur != null)
            {
                _context.Presentateurs.Remove(presentateur);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}