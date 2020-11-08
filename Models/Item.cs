using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Item
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="Item Name Cannot exceed 50 characters")]
        public string ItemName { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public string ItemDesc { get; set; }
        [Required]
        public double SellPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public int  ActionBy { get; set; }
        public DateTime ActionOn { get; set; }
        public string Photopath { get; set; }
        public string Photopath1 { get; set; }
        public string Photopath2 { get; set; }
    }
}
