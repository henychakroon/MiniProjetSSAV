namespace projet.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Complaint
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        public string Status { get; set; } = "En attente";

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public virtual ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
    }
}
