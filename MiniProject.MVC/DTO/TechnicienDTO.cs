using MiniProject.MVC.Models;

namespace MiniProject.MVC.DTO
{
    public class TechnicienDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Technicien ToTechnicien()
        {
            return new Technicien { Id = Id, Name = Name };
        }

        public TechnicienDTO()
        {

        }
        public TechnicienDTO(Technicien tc)
        {
            Id = tc.Id;
            Name = tc.Name;
        }
    }
}