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
            var products = productRepo.GetAllProducts();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult DeleteProductById(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = productRepo.GetProductById(id);
            if(product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
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