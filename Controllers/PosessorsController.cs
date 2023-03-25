using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItemLog.Context;
using ItemLog.Models;

namespace ItemLog.Views
{
    public class PosessorsController : Controller
    {
        private readonly DataContext _context;

        public PosessorsController(DataContext context)
        {
            _context = context;
        }

        // GET: Posessors
        public async Task<IActionResult> Index()
        {
              return _context.Posessors != null ? 
                          View(await _context.Posessors.ToListAsync()) :
                          Problem("Entity set 'DataContext.Posessors'  is null.");
        }

        // GET: Posessors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posessors == null)
            {
                return NotFound();
            }

            var posessor = await _context.Posessors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posessor == null)
            {
                return NotFound();
            }

            return View(posessor);
        }

        // GET: Posessors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posessors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StaffNum,FirstName,LastName,OtherName,PhoneNumber,Department")] Posessor posessor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posessor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(posessor);
        }

        // GET: Posessors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posessors == null)
            {
                return NotFound();
            }

            var posessor = await _context.Posessors.FindAsync(id);
            if (posessor == null)
            {
                return NotFound();
            }
            return View(posessor);
        }

        // POST: Posessors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StaffNum,FirstName,LastName,OtherName,PhoneNumber,Department")] Posessor posessor)
        {
            if (id != posessor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posessor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosessorExists(posessor.Id))
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
            return View(posessor);
        }

        // GET: Posessors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posessors == null)
            {
                return NotFound();
            }

            var posessor = await _context.Posessors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posessor == null)
            {
                return NotFound();
            }

            return View(posessor);
        }

        // POST: Posessors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posessors == null)
            {
                return Problem("Entity set 'DataContext.Posessors'  is null.");
            }
            var posessor = await _context.Posessors.FindAsync(id);
            if (posessor != null)
            {
                _context.Posessors.Remove(posessor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosessorExists(int id)
        {
          return (_context.Posessors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
