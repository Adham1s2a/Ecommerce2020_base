using Ecommerce.Models;
using Ecommerce.Views.Cart;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly IItemRepository itemRepository;

        public CartController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }
        public IActionResult CartItems()
        {
            var sessiondata = JsonConvert.DeserializeObject<CartItemsModel>(HttpContext.Session.GetString("cart"));

            return View(sessiondata);
        }

        public IActionResult AddtoCart(int id)
        {
            if (HttpContext.Session.GetString("cart") == null)
            {

                List<CartItemsModel> cart = new List<CartItemsModel>();
                cart.Add(new CartItemsModel(itemRepository.GetItem(id), 1));
                HttpContext.Session.SetString("cart",JsonConvert.SerializeObject(cart));
                return View("CartItems", cart);
            }
            else
            {
                return View("CartItems");
            }

        
        }
    }
}
