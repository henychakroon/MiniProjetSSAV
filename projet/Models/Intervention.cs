using System.ComponentModel.DataAnnotations;

namespace projet.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        [Required]
        public string TechnicianName { get; set; }
        public DateTime InterventionDate { get; set; } = DateTime.Now;
        public string Notes { get; set; }
        public bool IsFree { get; set; }  // true = gratuite (sous garantie), false = facturée
        public decimal? TotalCost { get; set; }  // Coût total si facturée
        public int ComplaintId { get; set; }
        public Complaint Complaint { get; set; }
    }

}
