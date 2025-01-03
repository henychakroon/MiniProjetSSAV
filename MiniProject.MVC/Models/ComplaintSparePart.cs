using MiniProject.MVC.Models.Base;

namespace MiniProject.MVC.Models
{
    public class ComplaintSparePart : BaseModel
    {
        public int ComplaintId { get; set; }
        public Complaint Complaint { get; set; }

        public int SparePartId { get; set; }
        public SparePart SparePart { get; set; }
    }
}
