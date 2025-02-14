﻿using MiniProject.MVC.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniProject.MVC.Models
{
    public class Article : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsUnderWarranty { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
