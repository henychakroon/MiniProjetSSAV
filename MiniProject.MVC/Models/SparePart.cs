using System.ComponentModel.DataAnnotations;

namespace MiniProject.MVC.Models
{
    public class SparePart
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
