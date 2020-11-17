using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class CheckoutController :Controller
         
    {
        private readonly AppDbContext _context;
        public CheckoutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult checkout()
        {
           
            ViewBag.datafound = 0;
            if (HelperClass.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart") == null)
            {

                ViewBag.message = "No item in the shopping cart";
                ViewBag.datafound = 0;
                return View();
            }
            else
            {
                ViewBag.datafound = 1;
                // List<CartItemsModel> sessiondata = JsonConvert.DeserializeObject<List<CartItemsModel>>(HttpContext.Session.GetString("cart"));
                var cart = HelperClass.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(X => X.item.SellPrice * X.Q);
                return View();
            }

        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsValidOfferCode(string Code)
        {
            var code = _context.Offers.FindAsync(Code);

            if (code != null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Offer Code {Code} is not valid");
            }

        }
    }
}
