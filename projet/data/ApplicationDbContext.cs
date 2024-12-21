using Microsoft.EntityFrameworkCore;
using projet.Models;

namespace projet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets pour chaque entité
        public DbSet<Client> Clients { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemple de configuration supplémentaire si nécessaire

            // Configuration des relations Client - Complaint
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Client)
                .WithMany(cl => cl.Complaints)
                .HasForeignKey(c => c.ClientId);

            // Configuration des relations Complaint - Intervention
            modelBuilder.Entity<Intervention>()
                .HasOne(i => i.Complaint)
                .WithMany(c => c.Interventions)
                .HasForeignKey(i => i.ComplaintId);

            // Configuration des relations Article - SparePart
            modelBuilder.Entity<SparePart>()
                .HasOne(sp => sp.Article)
                .WithMany(a => a.SpareParts)
                .HasForeignKey(sp => sp.ArticleId);
        }
    }
}
