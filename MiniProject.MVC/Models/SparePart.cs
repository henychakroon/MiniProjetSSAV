using MiniProject.MVC.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniProject.MVC.Models
{
    public class SparePart : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public ICollection<ComplaintSparePart> ComplaintSpareParts { get; set; }

    }
}
