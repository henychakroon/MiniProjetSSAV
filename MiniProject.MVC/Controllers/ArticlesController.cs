﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProject.MVC.DTO;
using MiniProject.MVC.Data;
using MiniProject.MVC.Repositories;
using MiniProject.MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace MiniProject.MVC.Controllers
{
    [Authorize(Roles =ApplicationRoles.SAV)]
    public class ArticlesController : Controller
    {
        private readonly IGenericRepository<Article> repoArt;
        public ArticlesController(IGenericRepository<Article> repoArt)
        {
            this.repoArt = repoArt;
        }

        // GET: Technicien
        public async Task<IActionResult> Index()
        {
            var data = await repoArt.GetAllAsync(
                predicate: null,
                includes: new List<string> { }
            );
            return View(data.Select(t => new ArticleDTO(t)));
        }

        // GET: Technicien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ArticleDTO = await repoArt.GetByIdAsync(id.Value, new List<string> { });
            if (ArticleDTO == null)
            {
                return NotFound();
            }

            return View(ArticleDTO);
        }

        // GET: Technicien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Technicien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleDTO ArticleDTO)
        {
            if (ModelState.IsValid)
            {
                repoArt.Add(ArticleDTO.ToArticle());
                await repoArt.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ArticleDTO);
        }

        // GET: Technicien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Article= await repoArt.GetByIdAsync(id.Value, new List<string> { });
            if (Article== null)
            {
                return NotFound();
            }
            return View(new ArticleDTO(Article));
        }

        // POST: Technicien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArticleDTO ArticleDTO)
        {
            if (id != ArticleDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repoArt.Update(ArticleDTO.ToArticle());
                    await repoArt.SaveAsync();
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ArticleDTO);
        }

        // GET: Technicien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ArticleDTO = await repoArt.GetByIdAsync(id.Value, new List<string> { });

            if (ArticleDTO == null)
            {
                return NotFound();
            }

            return View(ArticleDTO);
        }

        // POST: Technicien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technicien = await repoArt.GetByIdAsync(id, new List<string> { });
            if (technicien == null)
                if (technicien != null)
                {
                    return NotFound();
                }
            repoArt.Delete(technicien);
            await repoArt.SaveAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
