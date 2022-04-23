using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApplication.Controllers
{
    [Admin]
    public class RoleController : Controller
    {
        ShoppingContext db = new ShoppingContext();
        // GET: Role
        public ActionResult Index()
        {
            List<Role> roles = db.Roles.ToList();
            return View(roles);
        }

        //View Role
        public ActionResult View(int Id)
        {
            Role roles =db.Roles.Where(x => x.Id == Id).FirstOrDefault();
            return View(roles);
        }
        //Delete Role
        public ActionResult Delete(int Id)
        {
            Role roles = db.Roles.Where(x => x.Id == Id).FirstOrDefault();
            db.Roles.Remove(roles);
            db.SaveChanges();
            return Redirect("/Role/Index");
        }
        //Add Role
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Role role)
        {
            db.Roles.Add(role);
            db.SaveChanges();
            return Redirect("/Role/Index");
        }
        //Edit Product
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Role role = db.Roles.Where(x => x.Id == Id).FirstOrDefault();
            return View(role);
        }
        [HttpPost]
        public ActionResult Edit(Role role) // fill kr k bhji ha
        {
            Role dbRole = db.Roles.Where(x => x.Id == role.Id).FirstOrDefault(); // or yai jo abhi get ki ha.
            dbRole.Name = role.Name;           
            dbRole.Id = role.Id;
            db.SaveChanges();
            return Redirect("/Role/Index");
        }
    }
}