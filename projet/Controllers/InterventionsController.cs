using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.Data;
using projet.Models;

public class InterventionController : Controller
{
    private readonly ApplicationDbContext _context;

    public InterventionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Liste des interventions
    public IActionResult Index()
    {
        var interventions = _context.Interventions
                                    .Include(i => i.Complaint)
                                    .ToList();
        return View(interventions);
    }

    // Affiche le formulaire pour planifier une intervention
    public IActionResult Create(int complaintId)
    {
        var intervention = new Intervention { ComplaintId = complaintId };
        return View(intervention);
    }

    // Traite la création d'une intervention
    [HttpPost]
    public IActionResult Create(Intervention intervention)
    {
        if (ModelState.IsValid)
        {
            if (intervention.IsFree)
            {
                intervention.TotalCost = 0;
            }

            _context.Interventions.Add(intervention);
            _context.SaveChanges();
            return RedirectToAction("Details", "Complaint", new { id = intervention.ComplaintId });
        }

        return View(intervention);
    }
}
