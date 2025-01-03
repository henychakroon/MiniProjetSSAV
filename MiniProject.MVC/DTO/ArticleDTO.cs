
using MiniProject.MVC.Models;

namespace MiniProject.MVC.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsUnderWarranty { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Article ToArticle()
        {
            return new Article { Id = Id, Name = Name, IsUnderWarranty = IsUnderWarranty, PurchaseDate = PurchaseDate };
        }

        public ArticleDTO()
        {

        }
        public ArticleDTO(Article ar)
        {
            Id = ar.Id;
            Name = ar.Name;
            IsUnderWarranty = ar.IsUnderWarranty;
            PurchaseDate = ar.PurchaseDate;
        }
    }
}
