using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}