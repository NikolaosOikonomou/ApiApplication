using ApiApplication.Models;
using ApiApplication.MyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApplication.Repositories
{
    public class ShopRepository
    {
        private ApplicationDbContext db;

        public ShopRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public IEnumerable<Shop> GetAll()
        {
            var shops = db.Shops.ToList();

            return shops;
        }

        public Shop GetById(int? id)
        {
            return db.Shops.Find(id);
        }

    }
}