using Microsoft.AspNetCore.Mvc;
using projet.Data;
using projet.Models;
using Microsoft.EntityFrameworkCore;

public class ArticleController : Controller
{
    private readonly ApplicationDbContext _context;

    public ArticleController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Liste des articles
    public IActionResult Index()
    {
        var articles = _context.Articles.ToList();
        return View(articles);
    }

    // Affiche le formulaire pour ajouter un article
    public IActionResult Create()
    {
        return View();
    }

    // Traite l'ajout d'un article
    [HttpPost]
    public IActionResult Create(Article article)
    {
        if (ModelState.IsValid)
        {
            _context.Articles.Add(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(article);
    }

    // Affiche les détails d'un article
    public IActionResult Details(int id)
    {
        var article = _context.Articles
                              .Include(a => a.SpareParts)
                              .FirstOrDefault(a => a.Id == id);

        if (article == null)
            return NotFound();

        return View(article);
    }
}
