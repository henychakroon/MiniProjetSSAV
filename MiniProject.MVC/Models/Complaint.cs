using MiniProject.MVC.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject.MVC.Models
{
    public class Complaint : BaseModel
    {

        [Required]
        public string Description { get; set; }

        public DateTime DateSubmitted { get; set; }

        public string Status { get; set; }

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int? TechnicienId { get; set; }
        public Technicien? Technicien { get; set; }

        public ICollection<ComplaintSparePart>? ComplaintSpareParts { get; set; }

        public decimal MenPrice { get; set; }
    }
}
