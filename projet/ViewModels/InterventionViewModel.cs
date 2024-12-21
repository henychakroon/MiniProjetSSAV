namespace projet.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace YourProjectNamespace.ViewModels
    {
        public class InterventionViewModel
        {
            [Required]
            public int InterventionId { get; set; }

            [Required(ErrorMessage = "Veuillez sélectionner un client.")]
            public int ClientId { get; set; }

            [Required(ErrorMessage = "Veuillez sélectionner un technicien.")]
            public int TechnicianId { get; set; }

            [Required(ErrorMessage = "Veuillez fournir une date d'intervention.")]
            [DataType(DataType.Date)]
            public DateTime InterventionDate { get; set; }

            [Required(ErrorMessage = "Veuillez spécifier si l'intervention est sous garantie.")]
            public bool IsUnderWarranty { get; set; }

            [Required(ErrorMessage = "Veuillez fournir une description de l'intervention.")]
            [StringLength(500, ErrorMessage = "La description ne doit pas dépasser 500 caractères.")]
            public string Description { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Le coût doit être un nombre positif.")]
            [Display(Name = "Coût de l'intervention")]
            public decimal Cost { get; set; }

            [Required(ErrorMessage = "Veuillez spécifier l'état de l'intervention.")]
            [StringLength(50)]
            public string Status { get; set; }

            // Propriétés pour les listes déroulantes
            public string ClientName { get; set; }
            public string TechnicianName { get; set; }
        }
    }

}
