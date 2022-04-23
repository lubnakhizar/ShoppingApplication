using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApplication
{
    public class Seller: ActionFilterAttribute
    {
        ShoppingContext db = new ShoppingContext();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpCookie httpCookie = filterContext.HttpContext.Request.Cookies.Get("user-access-token");
            if (httpCookie != null)
            {
                if (!db.Users.Where(x => x.AccessToken.Equals(httpCookie.Value) && x.Role.Name.Equals("Seller")).Any())
                    filterContext.Result = new RedirectResult("/Account/Login"); // or   ( /Account/login)
            }

            else
                    filterContext.Result = new RedirectResult("/Account/Login");    // or   ( /Account/login)

            //Agr wo admin ha tu usai aglai page pr janai do jaha wo jana chahta ha...
            base.OnActionExecuting(filterContext);

            // Remember me Cookie store krwa rhai ha
            HttpCookie RememberMe = filterContext.HttpContext.Request.Cookies.Get("user-access-token");
            if (RememberMe != null)
            {
                if (RememberMe.Value.Equals("True"))
                {
                    //Method of set the value                                                       =     Get the Value
                    filterContext.HttpContext.Response.Cookies["user-access-token"].Value = filterContext.HttpContext.Request.Cookies.Get("user-access-token").Value;
                    filterContext.HttpContext.Response.Cookies["user-access-token"].Expires = DateTime.UtcNow.AddDays(30);

                }
            }
        }
    }
}