using MiniProject.MVC.Models;

namespace MiniProject.MVC.DTO
{
    public class ComplaintSparePartDTO
    {
        public int Id { get; set; }
        public int ComplaintId { get; set; }
        public int SparePartId { get; set; }
        public string SparePartName { get; set; }
        public decimal SparePartPrice { get; set; }
        public ComplaintSparePart ToComplaintSparePart()
        {
            return new ComplaintSparePart { Id = Id, ComplaintId = ComplaintId ,  SparePartId = SparePartId  };
        }
        public ComplaintSparePartDTO()
        {
            
        }
        public ComplaintSparePartDTO(ComplaintSparePart csp)
        {
            Id = csp.Id;
            ComplaintId = csp.ComplaintId;
            SparePartId = csp.SparePartId;
            SparePartName = csp.SparePart.Name;
            SparePartPrice = csp.SparePart.Price;
        }
    }
}
