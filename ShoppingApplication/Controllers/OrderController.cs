using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApplication.Controllers
{
   // [Buyer]           // Comment this Secure Action 
    public class OrderController : Controller
    {
        ShoppingContext db = new ShoppingContext();

        // GET: Order
        public ActionResult Index()
        {
            // kisi user ko srf usi ka order dikhaanai k liyai naa k yai ho k wo sarai orders ki list dikhaa dai..
            User user = new AccountController().GetUser(Request);
            List<Order> orders = db.Orders.Where(x => x.BuyerId == user.Id).ToList();

           // List<Order> orders = db.Orders.ToList();
            return View(orders);
        }
        //View Order
        public ActionResult View(int Id)
        {
            Order orders = db.Orders.Where(x => x.Id == Id).FirstOrDefault();
            return View(orders);
        }
        //Delete Order
        public ActionResult Delete(int Id)
        {
            Order orders = db.Orders.Where(x => x.Id == Id).FirstOrDefault();
            db.Orders.Remove(orders);
            db.SaveChanges();
            return Redirect("/Order/Index"); // view banana hi nai ,remove ho kr redirect chala jai ga...
        }
        // Add Order
        [HttpGet]
        public ActionResult Add()
        {
            // add the dropdown...
            ViewBag.OrderStatuses = db.OrderStatuses.ToList();
            ViewBag.Buyers = db.Users.Where(x => x.RoleId == 3).ToList();
            ViewBag.Products = db.Products.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Order order)
        {
            // add the dropdown...
            ViewBag.OrderStatuses = db.OrderStatuses.ToList();
            ViewBag.Buyers = db.Users.Where(x => x.RoleId == 3).ToList();
            ViewBag.Products = db.Products.ToList();
            db.Orders.Add(order);
            db.SaveChanges();
            return Redirect("/Order/Index");
        }  
        //Place a  order......
        public ActionResult Add(int Id)
        {
            // kisi user ko srf usi ka order dikhaanai k liyai naa k yai ho k wo sarai orders ki list dikhaa dai..
            User user = new AccountController().GetUser(Request);
            Order order = new Order
            {
                BuyerId = user.Id,
                OrderStatusId = 1,
                DateTime = DateTime.UtcNow.AddHours(6),
                ProductId = Id
            };
      
            db.Orders.Add(order);
            db.SaveChanges();
            return Redirect("/Order/Index");

            //yai view mai lik dena..nichai wala
            // <a href="/Order/Add/@Model.Id">Buy Now </a>
        }


        //Edit Order
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            // add the dropdown...
            ViewBag.OrderStatuses = db.OrderStatuses.ToList();
            ViewBag.Buyers = db.Users.Where(x => x.RoleId == 3).ToList();
            ViewBag.Products = db.Products.ToList();
            Order order = db.Orders.Where(x => x.Id == Id).FirstOrDefault();
            return View(order);
        }
        [HttpPost]
        public ActionResult Edit(Order order) // fill kr k bhji ha
        {
            Order dbOrder = db.Orders.Where(x => x.Id ==order.Id).FirstOrDefault(); // or yai jo abhi get ki ha.
            dbOrder.OrderStatusId = order.OrderStatusId;
            dbOrder.DateTime = order.DateTime;
            dbOrder.BuyerId = order.BuyerId;
            dbOrder.ProductId =order.ProductId;
            dbOrder.Id =order.Id;
            db.SaveChanges();
            return Redirect("/Order/Index");
        }
    }
}