using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class CreateUserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Address { get; set; }
        public IFormFile photo { get; set; }
    }
}
