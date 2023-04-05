using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItemLog.Context;
using ItemLog.Models;
using ItemLog.Models.ViewModels;
using ItemLog.TagHelpers;
using Microsoft.AspNetCore.Hosting;


namespace ItemLog.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ItemsController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemsController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Items
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Items.Count() / pageSize);

            return View(await _context.Items.OrderByDescending(p => p.Id)
                                                                            .Include(p => p.Category)
                                                                            .Skip((p - 1) * pageSize)
                                                                            .Take(pageSize)
                                                                            .ToListAsync());
            //stock code 
            //return _context.Items != null ? 
            //              View(await _context.Items.ToListAsync()) :
            //              Problem("Entity set 'DataContext.Items'  is null.");
        }




        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Admin/Items/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            return View();
        }

        // POST: Admin/Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create( Item item)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);

            if (ModelState.IsValid)
            {
                item.Slug = item.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Items.FirstOrDefaultAsync(p => p.Slug == item.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The item already exists.");
                    return View(item);
                }

                if (item.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/Items");
                    string imageName = Guid.NewGuid().ToString() + "_" + item.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await item.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    item.ImageURL = imageName;
                }

                _context.Add(item);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The item has been created!";

                return RedirectToAction("Index");

            }

            return View(item);
        }

        // GET: Admin/Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Item item)
        {

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                item.Slug = item.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Items.FirstOrDefaultAsync(p => p.Slug == item.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The item already exists.");
                    return View(item);
                }

                if (item.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/items");
                    string imageName = Guid.NewGuid().ToString() + "_" + item.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await item.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    item.ImageURL = imageName;
                }

                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "The item has been edited!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            return View(item);
        }


        // GET POST: Items/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Item item = await _context.Items.FindAsync(id);

            if (!string.Equals(item.ImageURL, "noimage.png"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/items");
                string oldImagePath = Path.Combine(uploadsDir, item.ImageURL);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            TempData["Success"] = "The item has been deleted!";

            return RedirectToAction("Index");
        }

        private bool ItemExists(int id)
        {
          return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
