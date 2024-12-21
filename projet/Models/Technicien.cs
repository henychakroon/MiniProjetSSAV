namespace projet.Models
{
    public class Technicien
    {
        public int TechnicienID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        // Navigation property
        public ICollection<Intervention> Interventions { get; set; }
    }
}
