using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApplication.Models
{
    public class Shop
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Region { get; set; }

        public Shop()
        {
            Products = new HashSet<Product>();
        }

        public virtual ICollection<Product> Products { get; set; }
    }
}