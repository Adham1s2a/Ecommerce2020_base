using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReflectionIT.Mvc.Paging;

namespace Ecommerce.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> CategoryList { get; set; }
        public List<Item> ItemList { get; set; }


    }
}
