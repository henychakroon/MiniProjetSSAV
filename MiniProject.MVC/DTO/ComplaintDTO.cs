using MiniProject.MVC.Models;

namespace MiniProject.MVC.DTO
{
    public class ComplaintDTO
    {
        public Complaint ToComplaint()
        {
            return new Complaint
            {
                Id = Id,
                Description = Description,
                DateSubmitted = DateSubmitted,
                Status = Status,
                ClientId = ClientId,
                ArticleId = ArticleId,
                TechnicienId = TechnicienId,
                MenPrice = MenPrice,
                ComplaintSpareParts = SparePartsIds?.Select(spId => new ComplaintSparePart
                {
                    ComplaintId = Id,
                    SparePartId = spId
                }).ToList()
                //ComplaintSpareParts = ComplaintSpareParts?.Select(csp=> csp.ToComplaintSparePart()).ToList() ?? new List<ComplaintSparePart>()
            };
        }

        public ComplaintDTO()
        {

        }
        public ComplaintDTO(Complaint cp)
        {
            Id = cp.Id;
            Description = cp.Description;
            DateSubmitted = cp.DateSubmitted;
            Status = cp.Status;
            ClientId = cp.ClientId;
            ClientName = cp.Client.FullName;
            ArticleId = cp.ArticleId;
            ArticleName = cp.Article.Name;
            TechnicienId = cp.TechnicienId;
            TechnicienName = cp.Technicien?.Name;
            MenPrice = cp.MenPrice;
            ComplaintSpareParts = cp.ComplaintSpareParts.Select(s=> new SparePartDTO(s.SparePart)).ToList();
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
        public string? Status { get; set; } = ComplainState.EnAttente;
        public string? ClientId { get; set; }
        public string? ClientName { get; set; }
        public int ArticleId { get; set; }
        public string? ArticleName { get; set; }

        public int? TechnicienId { get; set; }
        public string? TechnicienName { get; set; }

        public List<int>? SparePartsIds { get; set; }
        public List<SparePartDTO>? ComplaintSpareParts { get; set; }
        public List<SparePartDTO>? AllComplaintSpareParts { get; set; }

        public decimal MenPrice { get; set; }
    }
}
