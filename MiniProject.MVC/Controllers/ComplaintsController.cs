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
using MiniProject.MVC.DTO;
using MiniProject.MVC.Models;
using MiniProject.MVC.Repositories;

namespace MiniProject.MVC.Controllers
{
    [Authorize]
    public class ComplaintsController : Controller
    {
        private readonly IGenericRepository<Complaint> repoCom;
        private readonly IGenericRepository<Article> repoArt;
        private readonly IGenericRepository<SparePart> repoSP;
        private readonly IGenericRepository<Technicien> repoTec;
        private readonly IGenericRepository<ComplaintSparePart> repoComSp;

        public ComplaintsController(ApplicationDbContext context, IGenericRepository<Complaint> repoCom, IGenericRepository<Article> repoArt, IGenericRepository<SparePart> repoSP, IGenericRepository<Technicien> repoTec, IGenericRepository<ComplaintSparePart> repoComSp)
        {
            this.repoCom = repoCom;
            this.repoArt = repoArt;
            this.repoSP = repoSP;
            this.repoTec = repoTec;
            this.repoComSp = repoComSp;
        }

        [Authorize(Roles =ApplicationRoles.SAV)]
        // GET: Complaints
        public async Task<IActionResult> Index()
        {

            var list = await repoCom.GetAllAsync(predicate: null,new List<string> { "Article", "Client", "Technicien", "ComplaintSpareParts.SparePart" });

            return View(list.Select(s => new ComplaintDTO(s)));
        }

        [Authorize(Roles = ApplicationRoles.CLIENT)]
        // GET: Complaints
        public async Task<IActionResult> MesReclamations()
        {
            var currentUserId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            var list = await repoCom.GetAllAsync(predicate: c => c.ClientId == currentUserId, new List<string> { "Article", "Client", "Technicien", "ComplaintSpareParts.SparePart" });

            return View(list.Select(s=>new ComplaintDTO(s)));
        }

        [Authorize(Roles = ApplicationRoles.CLIENT)]

        // GET: Complaints/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ArticleId"] = new SelectList(await repoArt.GetAllAsync(), "Id", "Name");
            var Complaint = new ComplaintDTO
            {
                ClientId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value
            };
            return View(Complaint);
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.CLIENT)]
        public async Task<IActionResult> Create(ComplaintDTO complaintDTO)
        {
            if (ModelState.IsValid)
            {
                var complaint = complaintDTO.ToComplaint();
                complaint.Status = ComplainState.EnAttente;
                complaint.ClientId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
                complaint.DateSubmitted = DateTime.Now;
                repoCom.Add(complaint);
                await repoCom.SaveAsync();
                return RedirectToAction(nameof(MesReclamations));
            }
            ViewData["ArticleId"] = new SelectList(await repoArt.GetAllAsync(), "Id", "Name", complaintDTO.ArticleId);
            return View(complaintDTO);
        }
        [Authorize(Roles = ApplicationRoles.SAV)]
        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await repoCom.GetByIdAsync(id.Value, new List<string> { "Article", "Client", "Technicien", "ComplaintSpareParts.SparePart" });

            if (complaint == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(await repoArt.GetAllAsync(), "Id", "Name", complaint.ArticleId);
            ViewData["TechnicienId"] = new SelectList(await repoTec.GetAllAsync(), "Id", "Name", complaint.TechnicienId);

            var complaintDTO = new ComplaintDTO(complaint);

            complaintDTO.SparePartsIds = complaint.ComplaintSpareParts?.Select(s => s.SparePartId).ToList() ?? new List<int>() ;

            complaintDTO.AllComplaintSpareParts = ( await repoSP.GetAllAsync(predicate : a => a.ArticleId == complaint.ArticleId))?.Select(s=>new SparePartDTO(s)).ToList() ?? new List<SparePartDTO>();

            return View(complaintDTO);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.SAV)]

        public async Task<IActionResult> Edit(int id, ComplaintDTO complaintDTO)
        {
            if (id != complaintDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var complaint = complaintDTO.ToComplaint();
                    
                    var clearCp = await repoComSp.GetAllAsync(predicate : c => c.ComplaintId == complaint.Id);

                    repoComSp.DeleteRange(clearCp);

                    repoCom.Update(complaint);

                    await repoCom.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaintDTO.Id))
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
            ViewData["ArticleId"] = new SelectList(await repoArt.GetAllAsync(), "Id", "Name", complaintDTO.ArticleId);
            ViewData["TechnicienId"] = new SelectList(await repoTec.GetAllAsync(), "Id", "Name", complaintDTO.TechnicienId);

            complaintDTO.AllComplaintSpareParts = (await repoSP.GetAllAsync(predicate: a => a.ArticleId == complaintDTO.ArticleId))?.Select(s => new SparePartDTO(s)).ToList() ?? new List<SparePartDTO>();

            return View(complaintDTO);
        }


        [Authorize(Roles = ApplicationRoles.CLIENT)]

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Annuler(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await repoCom.GetByIdAsync(id.Value, new List<string> { "Article", "Client", "Technicien", "ComplaintSpareParts.SparePart" });

            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("AnnulerComplain")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.CLIENT)]
        public async Task<IActionResult> AnnulerComplain(int id)
        {
            var complaint = await repoCom.GetByIdAsync(id);
            if (complaint != null)
            {
                complaint.Status = ComplainState.Annulee;
                repoCom.Update(complaint);
                await repoCom.SaveAsync();
            }

            return RedirectToAction(nameof(MesReclamations));
        }

        private bool ComplaintExists(int id)
        {
            return repoCom.Exit(id);
        }
    }
}
