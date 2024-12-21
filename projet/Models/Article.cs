using System.ComponentModel.DataAnnotations;

namespace projet.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsUnderWarranty { get; set; }  // Garantie : true = sous garantie, false = hors garantie
        public DateTime PurchaseDate { get; set; }
        public ICollection<SparePart> SpareParts { get; set; }
    }
}
