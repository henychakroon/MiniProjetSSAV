using Microsoft.AspNetCore.Mvc;
using System.Linq;
using projet.Models;
using projet.Data;  // Assurez-vous que le namespace des modèles est correct


public class ClientController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClientController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Affiche le formulaire d'inscription
    public IActionResult Register()
    {
        return View();
    }

    // Traite le formulaire d'inscription
    [HttpPost]
    public IActionResult Register(Client client)
    {
        if (ModelState.IsValid)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(client);
    }

    // Affiche le formulaire de connexion
    public IActionResult Login()
    {
        return View();
    }

    // Traite le formulaire de connexion
    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Email == email && c.Password == password);
        if (client != null)
        {
            // Stocker l'ID du client dans la session
            HttpContext.Session.SetInt32("ClientId", client.Id);
            return RedirectToAction("Index", "Complaint");
        }
        ViewBag.Error = "Email ou mot de passe incorrect";
        return View();
    }

    // Déconnexion
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
