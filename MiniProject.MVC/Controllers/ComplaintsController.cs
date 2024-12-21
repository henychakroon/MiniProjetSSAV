using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProject.MVC.Data;
using MiniProject.MVC.Models;

namespace MiniProject.MVC.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Complaints
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Complaint.Include(c => c.Article).Include(c => c.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint
                .Include(c => c.Article)
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: Complaints/Create
        [Authorize(Roles = $"{ApplicationRoles.CLIENT}")]
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Name");
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles =$"{ApplicationRoles.CLIENT}")]
        public async Task<IActionResult> Create([Bind("Id,Description,DateSubmitted,Status,ClientId,ArticleId")] Complaint complaint)
        {
            complaint.ClientId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            if (ModelState.IsValid)
            {
                complaint.ClientId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Name", complaint.ArticleId);
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", complaint.ClientId);
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Name", complaint.ArticleId);
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", complaint.ClientId);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,DateSubmitted,Status,ClientId,ArticleId")] Complaint complaint)
        {
            if (id != complaint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id))
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
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Name", complaint.ArticleId);
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", complaint.ClientId);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint
                .Include(c => c.Article)
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaint = await _context.Complaint.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaint.Remove(complaint);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaint.Any(e => e.Id == id);
        }
    }
}
