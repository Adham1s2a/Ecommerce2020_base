using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Category
    {
        
        public int ID { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public bool  OfferStatus { get; set; }
        public int? OfferPersent { get; set; } = 0;
        public double? OfferAmount { get; set; } = 0;
        public bool IsDeleted { get; set; }
        public int  ActionBy { get; set; }
        public DateTime ActionOn { get; set; }
        public string Photopath { get; set; }
        public string Background { get; set; }
    }
}
