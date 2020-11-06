using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class CreateCategoryViewModel
    {
        public string CategoryName { get; set; }
        public bool OfferStatus { get; set; }
        public int OfferPersent { get; set; }
        public double OfferAmount { get; set; }
        public bool IsDeleted { get; set; }
        public int ActionBy { get; set; }
        public DateTime ActionOn { get; set; }

        public IFormFile Photo { get; set; }
        public IFormFile BG { get; set; }
    }
}
