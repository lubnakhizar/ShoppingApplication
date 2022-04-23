using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        ShoppingContext db = new ShoppingContext();
        // GET:  
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            //agr email or password match hojai tu line#31 pr chala jai ga..
            //goes to =>
            User dbuser = db.Users.Where(m => m.Email == user.Email && m.Password == user.Password).FirstOrDefault();
            //if Email or password you enter is incorrect, then it shows the message...
            if (dbuser == null)
            {
                ViewBag.Error = "Your email or password is incorrect";
                return View();
            }
            HttpCookie mycookie = new HttpCookie("user-access-token");
            mycookie.Value = dbuser.AccessToken;
            mycookie.Expires = DateTime.UtcNow.AddDays(5).AddHours(5);
            Response.Cookies.Remove("user-access-token");
            Response.Cookies.Add(mycookie);
            return Redirect("/Home/Index");
        }
        // GET:  
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            user.RoleId = 2;  // khud set ki id,buyer ki k user buyer ban kr aiga...
            //(UtcNow) it creates the unique identifier (long) string which is changed at every milli second...
            // it is vary to vary every user 
            user.AccessToken = DateTime.UtcNow.Ticks.ToString();
            db.Users.Add(user);
            db.SaveChanges();

            //Cookies  
            HttpCookie mycookie = new HttpCookie("user-access-token");
            mycookie.Value = user.AccessToken;
            mycookie.Expires = DateTime.UtcNow.AddDays(5).AddHours(5);
            Response.Cookies.Remove("user-access-token");
            Response.Cookies.Add(mycookie);
            return Redirect("/Home/Index");
        }
        // Logout...
        [HttpGet]
        public ActionResult Logout()
        {
            if (Request.Cookies["user-access-token"] != null)
            {
                Response.Cookies["user-access-token"].Expires = DateTime.UtcNow.AddHours(5).AddDays(-1);
            }
            return Redirect("/Home/Index");
        }
/*
        // Add secure data...
        public User GetUser(HttpRequestBase request)
        {
            string AccessToken = "";
            if (request.Cookies.Get("user-access-token") != null)
                AccessToken = request.Cookies.Get("user-access-token").Value;

            return db.Users.Where(x => x.AccessToken == AccessToken).FirstOrDefault();

        }*/
    }
}