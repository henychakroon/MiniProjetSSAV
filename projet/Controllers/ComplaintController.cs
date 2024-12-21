using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.Data;
using projet.Models;

public class ComplaintController : Controller
{
    private readonly ApplicationDbContext _context;

    public ComplaintController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Liste des réclamations pour le client
    public IActionResult Index()
    {
        int? clientId = HttpContext.Session.GetInt32("ClientId");
        if (clientId == null)
            return RedirectToAction("Login", "Client");

        var complaints = _context.Complaints
                                .Where(c => c.ClientId == clientId)
                                .Include(c => c.Client)
                                .ToList();
        return View(complaints);
    }

    // Affiche le formulaire de création de réclamation
    public IActionResult Create()
    {
        return View();
    }

    // Traite la création de réclamation
    [HttpPost]
    public IActionResult Create(Complaint complaint)
    {
        int? clientId = HttpContext.Session.GetInt32("ClientId");
        if (clientId == null)
            return RedirectToAction("Login", "Client");

        complaint.ClientId = clientId.Value;
        complaint.DateSubmitted = DateTime.Now;
        complaint.Status = "En attente";

        if (ModelState.IsValid)
        {
            _context.Complaints.Add(complaint);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(complaint);
    }

    // Détails d'une réclamation
    public IActionResult Details(int id)
    {
        var complaint = _context.Complaints
                                .Include(c => c.Client)
                                .Include(c => c.Interventions)
                                .FirstOrDefault(c => c.Id == id);
        if (complaint == null)
            return NotFound();

        return View(complaint);
    }

    // Pour le responsable SAV : changer le statut de la réclamation
    public IActionResult UpdateStatus(int id, string status)
    {
        var complaint = _context.Complaints.Find(id);
        if (complaint != null)
        {
            complaint.Status = status;
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
