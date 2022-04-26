using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
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
            Product product = db.Products.Where(x => x.Id == Id).FirstOrDefault(); 
            product.Image= GetImageBytes($"{Server.MapPath("~/App_Data")}{product.Image}");           
            return View(product);
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
        [HttpPost]                             // , Map the file we upload...
        public ActionResult Add(Product product, HttpPostedFileBase file)
        {
            // Check Laga rhai ha k image ka size itna hona chahiyai...
            if(file.ContentLength > 5242880)
            {
                ViewBag.Error = "Your file is above 5MB";
                ViewBag.ProductStatuses = db.ProductStatuses.ToList();
                ViewBag.Sellers = db.Users.Where(x => x.RoleId == 2).ToList();
                return View();
            }


            //Firstly,we give the path to server which automatically access the image path where actually image is placed.
            //  DateTime.UtcNow.Ticks + ".jpg" -> yai dynamic image k hr dafa unique image k naam ho ...

            string filename =  DateTime.UtcNow.Ticks + ".jpg";
            file.SaveAs(Server.MapPath("~/App_Data/Images/") + filename);
            product.Image = "/Images/"+filename;       // Image save hogii

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

        // Secure the Image Folder App_Data
        public static string GetImageBytes(string path)
        {
            try
            {
                using (Image image = Image.FromFile(path))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        return @String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(m.ToArray()));
                    }

                }
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}