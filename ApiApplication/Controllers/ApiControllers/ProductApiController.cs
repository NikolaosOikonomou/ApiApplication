using ApiApplication.Models;
using ApiApplication.MyApp;
using ApiApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ApiApplication.Controllers.ApiControllers
{
    public class ProductApiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ProductRepository productRepo;

        public ProductApiController()
        {
            productRepo = new ProductRepository(db);
        }

        public ActionResult GetAllProducts()
        {
            var products = productRepo.GetAllWithShops();
           
      
            foreach (var product in products)
            {
               var e = product.Shop.Title;
            }
            var productsWithShops = products.Select(x => new
            {
                Title = x.Title,
                Price = x.Price,
                Quantity = x.Quantity,
                ShopId = x.ShopId,
                Shop = x.Shop.Title
            });
            return Json(productsWithShops, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetProductById(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = productRepo.GetById(id);
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return Json(product, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DeleteProductById(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = productRepo.GetById(id);
            
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            if (ModelState.IsValid)
            {
                productRepo.Delete(product);
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepo.Add(product);
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        [HttpPost]
        public ActionResult EditProductById(int? id, Product product)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pro = productRepo.GetById(id);
            if (pro == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            pro.Title = product.Title;
            pro.Quantity = product.Quantity;
            pro.Price = product.Price;

            if (ModelState.IsValid)
            {
                productRepo.Edit(pro);
                return Json(pro, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}