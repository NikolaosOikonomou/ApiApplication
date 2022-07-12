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
        
        private ShopRepository shopRepo;//--->Need to Move it with ActionResult GetAllShops() method 

        public ProductApiController()
        {
            productRepo = new ProductRepository(db);
            shopRepo = new ShopRepository(db);
        }

        public ActionResult GetAllProducts()
        {
            var products = productRepo.GetAll().Select(x => new Product
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Quantity = x.Quantity,
                ShopId = x?.ShopId,
                Shop = new Shop { Id = x.Id, Title = x.Shop?.Title }
            }).ToList();


            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
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
      
            var pro = new
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Quantity = product.Quantity,
                ShopId = product.Shop?.Id,
                ShopTitle = product.Shop?.Title,
            };
            return Json(pro, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult EditProductById(int? id, Product product, int? shopId)
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
            pro.ShopId = product.ShopId;
            pro.Shop = shopRepo.GetById(shopId);

            if (ModelState.IsValid)
            {
                productRepo.Edit(pro);
                return Json(pro, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
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

      

        [HttpGet]
        public ActionResult GetAllShops()
        {
            var shops = shopRepo.GetAll().Select(x => new { x.Id, x.Title, x.Region });
            return Json(shops, JsonRequestBehavior.AllowGet);
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