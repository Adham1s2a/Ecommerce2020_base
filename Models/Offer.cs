using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Offer
    {
        public int ID { get; set; }
       
        [Display(Name = "Offer Code")]
        [Remote(action: "IsValidOfferCode", controller: "Checkout")]
        public string OfferCode { get; set; }
        public int OfferPercentt { get; set; }
    }
}
