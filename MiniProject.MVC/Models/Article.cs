using System.ComponentModel.DataAnnotations;

namespace MiniProject.MVC.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsUnderWarranty { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
