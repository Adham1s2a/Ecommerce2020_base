using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Views.Cart
{
    public class CartItemsModel : PageModel
    {
        private Item item;
        private int v;

        public CartItemsModel(Item item, int v)
        {
            this.item = item;
            this.v = v;
        }

        public void OnGet()
        {
        }
    }
}
