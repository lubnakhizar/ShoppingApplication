using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApplication.Controllers
{
    //[Buyer]// Comment this Secure Action
    public class ProductController : Controller
    {
        ShoppingContext db = new ShoppingContext();
        // GET: Product
      
        public ActionResult Index()
        {
            List<Product> products = db.Products.ToList();
            return View(products);
        }
        //View Product
        public ActionResult View(int Id)
        {
            Product products = db.Products.Where(x => x.Id == Id).FirstOrDefault();
            return View(products);
        }
        //Delete Product
        public ActionResult Delete(int Id)
        {
            Product products = db.Products.Where(x => x.Id == Id).FirstOrDefault();
            db.Products.Remove(products);
            db.SaveChanges();
            return Redirect("/Product/Index"); // view banana hi nai ,remove ho kr redirect chala jai ga...
        }
        //Add Products
        [HttpGet]
        public ActionResult Add()
        {
            // add the dropdown...
            ViewBag.ProductStatuses = db.ProductStatuses.ToList();
            ViewBag.Sellers = db.Users.Where(x=>x.RoleId==2).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return Redirect("/Product/Index");
        }
        //Edit Product
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            // add the dropdown...
            ViewBag.ProductStatuses = db.ProductStatuses.ToList();
            ViewBag.Sellers = db.Users.Where(x => x.RoleId == 2).ToList();
            Product product = db.Products.Where(x => x.Id == Id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product) // fill kr k bhji ha
        {
            Product dbProduct = db.Products.Where(x => x.Id == product.Id).FirstOrDefault(); // or yai jo abhi get ki ha.
            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.Image = product.Image;
            dbProduct.ProductStatusId = product.ProductStatusId;
            dbProduct.SellerId = product.SellerId;
            dbProduct.Id = product.Id;
            db.SaveChanges();
            return Redirect("/Product/Index");
        }
    }
}