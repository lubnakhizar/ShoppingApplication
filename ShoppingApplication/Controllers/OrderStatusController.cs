using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApplication.Controllers
{
    [Seller]
    public class OrderStatusController : Controller
    {
        ShoppingContext db = new ShoppingContext();
        // GET: OrderStatus
        public ActionResult Index()
        {
            List<OrderStatus> orderstatuses = db.OrderStatuses.ToList();
            return View(orderstatuses);
        }
        //View OrderStatus
        public ActionResult View(int Id)
        {
            OrderStatus orderstatuses = db.OrderStatuses.Where(x => x.Id == Id).FirstOrDefault();
            return View(orderstatuses);
        }

        //Delete OrderStatus
        public ActionResult Delete(int Id)
        {
            OrderStatus orderstatuses = db.OrderStatuses.Where(x => x.Id == Id).FirstOrDefault();
            db.OrderStatuses.Remove(orderstatuses);
            db.SaveChanges();
            return Redirect("/OrderStatus/Index"); // view banana hi nai ,remove ho kr redirect chala jai ga...
        }
        // Add OrderStatus
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(OrderStatus orderstatus)
        {
            db.OrderStatuses.Add(orderstatus);
            db.SaveChanges();
            return Redirect("/OrderStatus/Index");
        }

        //Edit OrderStatus
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            OrderStatus orderstatus = db.OrderStatuses.Where(x => x.Id == Id).FirstOrDefault();
            return View(orderstatus);
        }
        [HttpPost]
        public ActionResult Edit(OrderStatus orderstatus) // fill kr k bhji ha
        {
            OrderStatus dbOrderStatus = db.OrderStatuses.Where(x => x.Id == orderstatus.Id).FirstOrDefault(); // or yai jo abhi get ki ha.        
            dbOrderStatus.Name = orderstatus.Name;
            dbOrderStatus.Id = orderstatus.Id;
            db.SaveChanges();
            return Redirect("/OrderStatus/Index");
        }
    }
}