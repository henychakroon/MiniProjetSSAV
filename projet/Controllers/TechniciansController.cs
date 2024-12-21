using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.Data;
using projet.Models;


public class TechniciansController : Controller
{
    private readonly ApplicationDbContext _context;

    public TechniciansController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Technicians.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Technicien technician)
    {
        if (ModelState.IsValid)
        {
            _context.Add(technician);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(technician);
    }
}
