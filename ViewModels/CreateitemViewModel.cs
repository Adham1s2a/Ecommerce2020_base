using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class CreateitemViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Item Name Cannot exceed 50 characters")]
        public string ItemName { get; set; }
        [Required]
       // public int CategoryName { get; set; }
        public string ItemDesc { get; set; }
        [Required]
        public double SellPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public int ActionBy { get; set; }
        public DateTime ActionOn { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile Photo1 { get; set; }
        public IFormFile Photo2 { get; set; }
        public CategoriesNamesenum CategoryNames { get; set; }

    }
}
