using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projectfolio.Data;
using Projectfolio.Models;

namespace Projectfolio.Controllers
{
    [Authorize]
    public class TechnologiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechnologiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Technologies
        public async Task<IActionResult> Index()
        {
              return View(await _context.technologies.ToListAsync());
        }

        // GET: Technologies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.technologies == null)
            {
                return NotFound();
            }

            var technology = await _context.technologies
                .FirstOrDefaultAsync(m => m.TechnologyId == id);
            if (technology == null)
            {
                return NotFound();
            }

            return View(technology);
        }

        // GET: Technologies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Technologies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TechnologyId,Name,ReleaseYear")] Technology technology)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technology);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technology);
        }

        // GET: Technologies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.technologies == null)
            {
                return NotFound();
            }

            var technology = await _context.technologies.FindAsync(id);
            if (technology == null)
            {
                return NotFound();
            }
            return View(technology);
        }

        // POST: Technologies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TechnologyId,Name,ReleaseYear")] Technology technology)
        {
            if (id != technology.TechnologyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technology);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnologyExists(technology.TechnologyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(technology);
        }

        // GET: Technologies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.technologies == null)
            {
                return NotFound();
            }

            var technology = await _context.technologies
                .FirstOrDefaultAsync(m => m.TechnologyId == id);
            if (technology == null)
            {
                return NotFound();
            }

            return View(technology);
        }

        // POST: Technologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.technologies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.technologies'  is null.");
            }
            var technology = await _context.technologies.FindAsync(id);
            if (technology != null)
            {
                _context.technologies.Remove(technology);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnologyExists(int id)
        {
          return _context.technologies.Any(e => e.TechnologyId == id);
        }
    }
}
