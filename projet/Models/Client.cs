using System.ComponentModel.DataAnnotations;

namespace projet.Models
{
    // Models/Client.cs
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Complaint> Complaints { get; set; }
    }


}
