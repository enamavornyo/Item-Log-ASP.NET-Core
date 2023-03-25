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
    public class PossRecordsController : Controller
    {
        private readonly DataContext _context;

        public PossRecordsController(DataContext context)
        {
            _context = context;
        }

        // GET: PossRecords
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.PosessionRecords.Include(p => p.Admin).Include(p => p.Item).Include(p => p.posessor);
            return View(await dataContext.ToListAsync());
        }

        // GET: PossRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PosessionRecords == null)
            {
                return NotFound();
            }

            var posessionRecord = await _context.PosessionRecords
                .Include(p => p.Admin)
                .Include(p => p.Item)
                .Include(p => p.posessor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posessionRecord == null)
            {
                return NotFound();
            }

            return View(posessionRecord);
        }

        // GET: PossRecords/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "FirstName");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id");
            ViewData["PosessorId"] = new SelectList(_context.Posessors, "Id", "FirstName");
            return View();
        }

        // POST: PossRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdminId,PosessorId,ItemId,RequestDate,ReturnDate")] PosessionRecord posessionRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posessionRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "FirstName", posessionRecord.AdminId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Category", posessionRecord.ItemId);
            ViewData["PosessorId"] = new SelectList(_context.Posessors, "Id", "FirstName", posessionRecord.PosessorId);
            return View(posessionRecord);
        }

        // GET: PossRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PosessionRecords == null)
            {
                return NotFound();
            }

            var posessionRecord = await _context.PosessionRecords.FindAsync(id);
            if (posessionRecord == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "FirstName", posessionRecord.AdminId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Category", posessionRecord.ItemId);
            ViewData["PosessorId"] = new SelectList(_context.Posessors, "Id", "FirstName", posessionRecord.PosessorId);
            return View(posessionRecord);
        }

        // POST: PossRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdminId,PosessorId,ItemId,RequestDate,ReturnDate")] PosessionRecord posessionRecord)
        {
            if (id != posessionRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posessionRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosessionRecordExists(posessionRecord.Id))
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
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "FirstName", posessionRecord.AdminId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Category", posessionRecord.ItemId);
            ViewData["PosessorId"] = new SelectList(_context.Posessors, "Id", "FirstName", posessionRecord.PosessorId);
            return View(posessionRecord);
        }

        // GET: PossRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PosessionRecords == null)
            {
                return NotFound();
            }

            var posessionRecord = await _context.PosessionRecords
                .Include(p => p.Admin)
                .Include(p => p.Item)
                .Include(p => p.posessor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posessionRecord == null)
            {
                return NotFound();
            }

            return View(posessionRecord);
        }

        // POST: PossRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PosessionRecords == null)
            {
                return Problem("Entity set 'DataContext.PosessionRecords'  is null.");
            }
            var posessionRecord = await _context.PosessionRecords.FindAsync(id);
            if (posessionRecord != null)
            {
                _context.PosessionRecords.Remove(posessionRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosessionRecordExists(int id)
        {
          return (_context.PosessionRecords?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
