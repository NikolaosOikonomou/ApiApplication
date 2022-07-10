using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApplication.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        //Foreign Key
        public int? ShopId { get; set; }
        //Navigation
        public virtual Shop Shop { get; set; }
    }
}