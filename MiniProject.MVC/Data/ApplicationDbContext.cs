using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniProject.MVC.Models;

namespace MiniProject.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<MiniProject.MVC.Models.Complaint> Complaint { get; set; } = default!;
    }
}
