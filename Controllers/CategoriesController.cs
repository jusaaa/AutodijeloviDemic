using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutodijeloviDemic.Data;
using AutodijeloviDemic.Models;

namespace AutodijeloviDemic.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);  // Vraća listu svih kategorija
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == categoryId);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);  // Vraća detalje kategorije
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();  // Vraća formu za kreiranje kategorije
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name")] Category category, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await Image.CopyToAsync(ms);
                        category.ImageData = ms.ToArray();
                        category.ImageMimeType = Image.ContentType;
                    }
                }

                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);  // Prikazuje ponovo formu ako postoji greška u validaciji
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);  // Vraća kategoriju za uređivanje
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int categoryId, [Bind("CategoryId,Name,ImageData,ImageMimeType")] Category category, IFormFile? Image)
        {
            if (categoryId != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoryToUpdate = await _context.Categories.FindAsync(categoryId);

                    if (categoryToUpdate == null)
                    {
                        return NotFound();
                    }

                    // Ažuriraj naziv kategorije
                    categoryToUpdate.Name = category.Name;

                    // Ažuriraj sliku ako je nova slika dodana
                    if (Image != null && Image.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await Image.CopyToAsync(ms);
                            categoryToUpdate.ImageData = ms.ToArray();
                            categoryToUpdate.ImageMimeType = Image.ContentType;
                        }
                    }

                    _context.Update(categoryToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == categoryId);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);  // Prikazuje formu za brisanje kategorije
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
