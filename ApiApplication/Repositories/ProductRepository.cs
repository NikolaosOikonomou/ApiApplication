using ApiApplication.Models;
using ApiApplication.MyApp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiApplication.Repositories
{
    /// <summary>
    ///All methods that interact with the database on behalf Product Model
    /// </summary>
    public class ProductRepository
    {
        private ApplicationDbContext db;

        public ProductRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public IEnumerable<Product> GetAllWithShops()
        {
            return db.Products.Include(x => x.Shop).ToList();
        }

        public Product GetById(int? id)
        {
            return db.Products.Find(id);
        }

        public void Add(Product product)
        {
            db.Entry(product).State = EntityState.Added;
            Save();
        }

        public void Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            Save();
        }

        public void Delete(Product product)
        {
            db.Entry(product).State = EntityState.Deleted;
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}