namespace projet.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Invoice
    {
        public int Id { get; set; }

        public DateTime DateIssued { get; set; } = DateTime.Now;

        public decimal Amount { get; set; }

        [ForeignKey("Intervention")]
        public int InterventionId { get; set; }
        public virtual Intervention Intervention { get; set; }
    }
}
