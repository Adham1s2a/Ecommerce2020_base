﻿using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class CartViewModel
    {
        public Item item{ get; set; }
        public int Q { get; set; }
    }
}
