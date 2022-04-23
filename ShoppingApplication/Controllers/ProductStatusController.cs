using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApplication.Controllers
{
    [Seller]    // make seller
    public class ProductStatusController : Controller
    {
        ShoppingContext db = new ShoppingContext();
        // GET: ProductStatus
        public ActionResult Index()
        {
            List<ProductStatus> productstatuses = db.ProductStatuses.ToList(); 
            return View(productstatuses);
        }

        // View ProductStatus
        public ActionResult View(int Id)
        {
            ProductStatus productstatuses = db.ProductStatuses.Where(x => x.Id == Id).FirstOrDefault();
            return View(productstatuses);
        }

        //Delete ProductStatus
        public ActionResult Delete(int Id)
        {
            ProductStatus productstatuses = db.ProductStatuses.Where(x => x.Id == Id).FirstOrDefault();
            db.ProductStatuses.Remove(productstatuses);
            db.SaveChanges();
            return Redirect("/ProductStatus/Index");
        }

        //Add ProductStatus
        [HttpGet]
        public ActionResult Add()
        {
             return View();
        }
        [HttpPost]
        public ActionResult Add(ProductStatus productstatus)
        {
            db.ProductStatuses.Add(productstatus);
            db.SaveChanges();
            return Redirect("/ProductStatus/Index");
        }

        //Edit ProductStatus
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ProductStatus productstatus = db.ProductStatuses.Where(x => x.Id == Id).FirstOrDefault();
            return View(productstatus);
        }


        public ActionResult Edit(ProductStatus productstatus)
        {
            ProductStatus dbProductStatus = db.ProductStatuses.Where(x => x.Id == productstatus.Id).FirstOrDefault(); // or yai jo abhi get ki ha.        
            dbProductStatus.Name = productstatus.Name;
            dbProductStatus.Id = productstatus.Id;
            db.SaveChanges();
            return Redirect("/ProductStatus/Index");
        }
    }
}