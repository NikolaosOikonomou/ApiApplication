using ApiApplication.Models;
using ApiApplication.MyApp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ApiApplication.Initialazers
{
    public class MockUpDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Shop
            Shop s1 = new Shop() { Title = "Eshop", Region = "Athens" };
            Shop s2 = new Shop() { Title = "Public", Region = "Athens" };
            Shop s3 = new Shop() { Title = "Plaisio", Region = "Athens" };

            context.Shops.AddOrUpdate(x => x.Title, s1, s2, s3);
            context.SaveChanges();
            #endregion

            #region Product
            Product p1 = new Product() { Title = "AmdGpu", Price = 500, Quantity = 20 };
            Product p2 = new Product() { Title = "NvidiaGpu", Price = 650, Quantity = 25 };
            Product p3 = new Product() { Title = "SamsungTv", Price = 450, Quantity = 15 };
            Product p4 = new Product() { Title = "LgTv", Price = 500, Quantity = 14 };
            Product p5 = new Product() { Title = "Iphone", Price = 700, Quantity = 15 };
            Product p6 = new Product() { Title = "Xiaomi", Price = 400, Quantity = 20 };

            s1.Products.Add(p1);
            s1.Products.Add(p2);
            s2.Products.Add(p3);
            s2.Products.Add(p4);
            s3.Products.Add(p5);
            s3.Products.Add(p6);

            context.Products.AddOrUpdate(x => x.Title, p1, p2, p3, p4, p5, p6);
            context.SaveChanges();

            #endregion

            base.Seed(context);
        }
    }
}