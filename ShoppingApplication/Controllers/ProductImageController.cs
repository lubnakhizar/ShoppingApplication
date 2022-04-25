using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApplication.Controllers
{
    // [Admin]        // Comment this Secure Action 
    public class ProductImageController : Controller
    {
        ShoppingContext db = new ShoppingContext();
        // GET: ProductImage
        public ActionResult Index()
        {
            List<ProductImage> productimages = db.ProductImages.ToList(); 
            return View(productimages);
        }
        // View ProductImage
        public ActionResult View(int Id)
        {
            ProductImage productimages = db.ProductImages.FirstOrDefault(x => x.Id == Id);
            return View(productimages);
        }
        // Delete ProductImage
        public ActionResult Delete(int Id)
        {
            ProductImage  productimages = db.ProductImages.FirstOrDefault(x => x.Id == Id);
            db.ProductImages.Remove(productimages);
            db.SaveChanges();
            return Redirect("/ProductImage/Index");
        }
        //Add ProductImage
        [HttpGet]
        public ActionResult Add()
        {
            // Add dropdown
            ViewBag.Products = db.Products.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(ProductImage productimage)
        {
            db.ProductImages.Add(productimage);
            db.SaveChanges();
            return Redirect("/ProductImage/Index");
        }

        //Edit ProductImage
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            // Add dropdown
            ViewBag.Products = db.Products.ToList();
            ProductImage productimage = db.ProductImages.FirstOrDefault(x => x.Id == Id);
            return View(productimage);
        }
        [HttpPost]
        public ActionResult Edit(ProductImage productimage)
        {
            ProductImage dbProductImage = db.ProductImages.Where(x => x.Id == productimage.Id).FirstOrDefault();
             dbProductImage.Id = productimage.Id;
             dbProductImage.Image = productimage.Image;
             dbProductImage.ProductId = productimage.ProductId;
            db.SaveChanges();
            return Redirect("/ProductImage/Index");

        }
    }
}