using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Paypal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Controllers
{
    public class Paypal : Controller
    {
        public IConfiguration configuration { get; }

        public Paypal(IConfiguration _configuration) 
        {
            configuration = _configuration;
        }
        
        [HttpPost]
        public async Task <IActionResult> Checkout(double total)
        {
            var payPalAPI = new PaypalAPI(configuration);
            string url = await payPalAPI.getRedirectUrlToPaypal(total,"EUR");
            return Redirect(url);
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
