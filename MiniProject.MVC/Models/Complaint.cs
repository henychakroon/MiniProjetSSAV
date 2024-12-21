using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject.MVC.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        public string Status { get; set; } = "En attente";

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
