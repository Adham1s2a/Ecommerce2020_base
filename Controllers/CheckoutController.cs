using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class CheckoutController :Controller
    {
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
    }
}
