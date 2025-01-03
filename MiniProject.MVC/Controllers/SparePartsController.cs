using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProject.MVC.Data;
using MiniProject.MVC.DTO;
using MiniProject.MVC.Models;
using MiniProject.MVC.Repositories;

namespace MiniProject.MVC.Controllers
{
    [Authorize(Roles = ApplicationRoles.SAV)]
    public class SparePartsController : Controller
    {
        private readonly IGenericRepository<SparePart> repoSP;
        private readonly IGenericRepository<Article> repoAR;

        public SparePartsController(IGenericRepository<Article> repoAR, IGenericRepository<SparePart> repoSP)
        {
            this.repoSP = repoSP;
            this.repoAR = repoAR;
        }

        // GET: SpareParts
        public async Task<IActionResult> Index()
        {
            var data =await repoSP.GetAllAsync(predicate:null,includes:new List<string> { "Article" }) ;//_context.SpareParts.Include(s => s.Article);
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

            var sparePart = await repoSP.GetByIdAsync(id.Value, new List<string> { "Article" });
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // GET: SpareParts/Create
        public async Task<IActionResult> Create()
        {
            var articles = await repoAR.GetAllAsync();
            ViewData["ArticleId"] = new SelectList(articles.ToList(), "Id", "Name");
            return View();
        }

        // POST: SpareParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SparePartDTO sparePartDTO)
        {
            if (ModelState.IsValid)
            {
                 
                repoSP.Add(sparePartDTO.ToSparePart());
                await repoSP.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            var articles = await repoAR.GetAllAsync();
            ViewData["ArticleId"] = new SelectList(articles.ToList(), "Id", "Name");
            return View(sparePartDTO);
        }

        // GET: SpareParts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await repoSP.GetByIdAsync(id.Value, new List<string> { "Article" });
            if (sparePart == null)
            {
                return NotFound();
            }
            var articles = await repoAR.GetAllAsync();
            ViewData["ArticleId"] = new SelectList(articles.ToList(), "Id", "Name");
            return View(new SparePartDTO(sparePart));
        }

        // POST: SpareParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,SparePartDTO sparePartDTO)
        {
            if (id != sparePartDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    repoSP.Update(sparePartDTO.ToSparePart());
                    await repoSP.SaveAsync();
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            var articles = await repoAR.GetAllAsync();
            ViewData["ArticleId"] = new SelectList(articles.ToList(), "Id", "Name");
            return View(sparePartDTO);
        }

        // GET: SpareParts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await repoSP.GetByIdAsync(id.Value, new List<string> { "Article" });
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
            var sparePart = await repoSP.GetByIdAsync(id, new List<string> { "Article" });
            if (sparePart == null)
            {
                return NotFound();
            }
            repoSP.Delete(sparePart);
            await repoSP.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
