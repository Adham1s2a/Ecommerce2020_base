﻿using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
using Microsoft.AspNetCore.Http.Extensions;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IItemRepository itemRepository;

        public CartController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }
        public IActionResult CartItems()
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

        private int IsExist(int id)
        {
            List<CartViewModel> cart = HelperClass.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].item.ID.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult AddtoCart(int id)
        {
            int _quntity;
           CartViewModel item = new CartViewModel()
            {
                item = itemRepository.GetItem(id),
                Q = 1
            };
            _quntity = item.item.Quantity;
           
            if (HelperClass.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart") == null)
            {

                List<CartViewModel> cart = new List<CartViewModel>();
                cart.Add(item);
                HelperClass.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return Ok();
            }
            else
            {
                List<CartViewModel> cart = HelperClass.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
                int index = IsExist(id);
                if (index != -1 && cart[index].Q < _quntity)
                {
                    cart[index].Q++;
                    HelperClass.SetObjectAsJson(HttpContext.Session, "cart", cart);
                    return Ok();
                }
                else
                {
                    return StatusCode(33);
                }
               
            }
      

            //  return Redirect(Request.Headers["Referer"].ToString());

        }
        public IActionResult DectoCart(int id)
        {

            CartViewModel item = new CartViewModel()
            {
                item = itemRepository.GetItem(id),
                Q = 1
            };

            List<CartViewModel> cart = HelperClass.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
            int index = IsExist(id);
            if (index != -1 && cart[index].Q > 0)
            {
                cart[index].Q--;
                HelperClass.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return Ok();
            }
            else
            {
                return StatusCode(33);
            }

        }
        public IActionResult Remove(int id)
        {
            List<CartViewModel> cart = HelperClass.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
            int index = IsExist(id);
            cart.RemoveAt(index);
            HelperClass.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("CartItems");
        }
    }
}
