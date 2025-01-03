using MiniProject.MVC.Models;

namespace MiniProject.MVC.DTO
{
    public class SparePartDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ArticleId { get; set; }
        public string? ArticleName { get; set; }
        public SparePart ToSparePart()
        {
            return new SparePart { Id = Id, Name = Name, Price = Price, ArticleId = ArticleId };
        }

        public SparePartDTO()
        {

        }
        public SparePartDTO(SparePart sp)
        {
            Id = sp.Id;
            Name = sp.Name;
            Price = sp.Price;
            ArticleId = sp.ArticleId;
            ArticleName = sp.Article?.Name;
        }
    }
}
