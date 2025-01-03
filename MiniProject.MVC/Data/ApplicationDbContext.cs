using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniProject.MVC.Models;
using MiniProject.MVC.DTO;

namespace MiniProject.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ComplaintSparePart> ComplaintSpareParts { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<Technicien> Techniciens { get; set; }
        public DbSet<Complaint> Complaint { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Complaint>()
                .HasMany(x => x.ComplaintSpareParts)
                .WithOne(x=>x.Complaint)
                .HasForeignKey(x=>x.ComplaintId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<SparePart>()
                .HasMany(x => x.ComplaintSpareParts)
                .WithOne(x=>x.SparePart)
                .HasForeignKey(x=>x.SparePartId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
