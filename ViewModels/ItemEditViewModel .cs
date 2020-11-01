using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class ItemEditViewModel : CreateitemViewModel
    {
        public int id { get; set; }
        public string excistingphotopath { get; set; }
    }
}
