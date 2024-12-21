using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProject.MVC.Data;
using MiniProject.MVC.DTO;
using MiniProject.MVC.Models;
using MiniProject.MVC.Repositories;

namespace MiniProject.MVC.Controllers
{
    public class SparePartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<SparePart> repo;

        public SparePartsController(ApplicationDbContext context, IGenericRepository<SparePart> repo)
        {
            _context = context;
            this.repo = repo;
        }

        // GET: SpareParts
        public async Task<IActionResult> Index()
        {
            var data =await repo.GetAllAsync(predicate:null,includes:new List<string> { "Article" }) ;//_context.SpareParts.Include(s => s.Article);
           // return View(await applicationDbContext.ToListAsync());
            return View( data.Select(s=>new SparePartDTO(s)) );
        }

        // GET: SpareParts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SpareParts
                .Include(s => s.Article)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // GET: SpareParts/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Name");
            return View();
        }

        // POST: SpareParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,ArticleId")] SparePartDTO sparePartDTO)
        {
            if (ModelState.IsValid)
            {
                 
                _context.Add(sparePartDTO.ToSparePart());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Name", sparePartDTO.ArticleId);
            return View(sparePartDTO);
        }

        // GET: SpareParts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SpareParts.FindAsync(id);
            if (sparePart == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Name", sparePart.ArticleId);
            return View(sparePart);
        }

        // POST: SpareParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ArticleId")] SparePart sparePart)
        {
            if (id != sparePart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sparePart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SparePartExists(sparePart.Id))
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
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Name", sparePart.ArticleId);
            return View(sparePart);
        }

        // GET: SpareParts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SpareParts
                .Include(s => s.Article)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // POST: SpareParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sparePart = await _context.SpareParts.FindAsync(id);
            if (sparePart != null)
            {
                _context.SpareParts.Remove(sparePart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SparePartExists(int id)
        {
            return _context.SpareParts.Any(e => e.Id == id);
        }
    }
}
