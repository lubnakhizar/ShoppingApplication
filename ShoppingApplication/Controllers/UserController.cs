using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApplication.Controllers
{
    [Admin]
    public class UserController : Controller
    {
        ShoppingContext db = new ShoppingContext();

        // GET: User
        public ActionResult Index()
        {
            List<User> users = db.Users.ToList();
            return View(users);

        }

        // View User
        public ActionResult View(int Id)
        {
            User users = db.Users.Where(x => x.Id == Id).FirstOrDefault();
            return View(users);
        }

        //Delete User
        public ActionResult Delete()
        {
            User users = db.Users.Where(x => x.Id == 0).FirstOrDefault();
            db.Users.Remove(users);
            db.SaveChanges();
            return Redirect("/User/Index");
        }

        // Add Role
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return Redirect("/User/Index");
        }

        //Edit User
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            User user = db.Users.Where(x => x.Id == Id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user) // fill kr k bhji ha
        {
            User dbUser = db.Users.Where(x => x.Id == user.Id).FirstOrDefault(); // or yai jo abhi get ki ha.
            dbUser.Name = user.Name;
            dbUser.Email = user.Email;
            dbUser.Password = user.Password;
            dbUser.Id = user.Id;
            dbUser.RoleId = user.RoleId;
            db.SaveChanges();
            return Redirect("/User/Index");
        }
    }
}