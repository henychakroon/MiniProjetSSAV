using MiniProject.MVC.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniProject.MVC.Models
{
    public class Technicien : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
