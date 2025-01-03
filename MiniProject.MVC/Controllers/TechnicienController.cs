using System;
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
    [Authorize(Roles = ApplicationRoles.SAV)]
    public class TechnicienController : Controller
    {
        private readonly IGenericRepository<Technicien> repoTec;

        public TechnicienController(IGenericRepository<Technicien> repoTec)
        {
            this.repoTec = repoTec;
        }

        // GET: Technicien
        public async Task<IActionResult> Index()
        {
            var data = await repoTec.GetAllAsync(
                predicate: null,
                includes: new List<string> { }
            );
            return View(data.Select(t => new TechnicienDTO(t)));
        }

        // GET: Technicien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicienDTO = await repoTec.GetByIdAsync(id.Value, new List<string> { });
            if (technicienDTO == null)
            {
                return NotFound();
            }

            return View(technicienDTO);
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
        public async Task<IActionResult> Create(TechnicienDTO technicienDTO)
        {
            if (ModelState.IsValid)
            {
                repoTec.Add(technicienDTO.ToTechnicien());
                await repoTec.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technicienDTO);
        }

        // GET: Technicien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicien = await repoTec.GetByIdAsync(id.Value, new List<string> { });
            if (technicien == null)
            {
                return NotFound();
            }
            return View(new TechnicienDTO(technicien));
        }

        // POST: Technicien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id, TechnicienDTO technicienDTO)
        {
            if (id != technicienDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repoTec.Update(technicienDTO.ToTechnicien());
                    await repoTec.SaveAsync();
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(technicienDTO);
        }

        // GET: Technicien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicienDTO = await repoTec.GetByIdAsync(id.Value, new List<string> {  });
           
            if (technicienDTO == null)
            {
                return NotFound();
            }

            return View(technicienDTO);
        }

        // POST: Technicien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technicien = await repoTec.GetByIdAsync(id, new List<string> { });
            if (technicien == null)
                if (technicien != null)
            {
                    return NotFound();
                }
            repoTec.Delete(technicien);
            await repoTec.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
