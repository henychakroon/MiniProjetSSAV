using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using projet.Data;
using projet.Models;
using projet.repository;
// Correction : assurez-vous que le dossier est bien nommé avec une majuscule pour "Repository"

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services nécessaires à l'application
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Connexion à la base de données

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
  //  .AddEntityFrameworkStores<ApplicationDbContext>(); // Configuration de l'Identity pour les utilisateurs

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Ajouter les services des repositories personnalisés
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
builder.Services.AddScoped<IInterventionRepository, InterventionRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

var app = builder.Build();

// Configurer le pipeline de traitement des requêtes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Sécurisation de l'application en production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Activer l'authentification
app.UseAuthorization();  // Activer l'autorisation

// Routes par défaut
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Pour les pages Razor (si nécessaire)

app.Run();
