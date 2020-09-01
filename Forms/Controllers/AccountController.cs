using Forms.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;

namespace Forms.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Register(User u1)
        {
            if (ModelState.IsValid)
            {
                u1.Register();
                return RedirectToAction("Index");
            }
            else
            {
                return View("SignUp", u1);
            }
          
        }
        public ActionResult Login(User u1)
        {
            int expiretime = 60;
           if(!u1.Email.IsNullOrWhiteSpace()&& !u1.Password.IsNullOrWhiteSpace())
            {
                if (u1.IsAuthentic())
                {
                    FormsAuthenticationTicket ticket = new
   FormsAuthenticationTicket(
        1,
        u1.id.ToString(),
        System.DateTime.Now,
        System.DateTime.Now.AddMinutes(expiretime), true, "sdf",
        FormsAuthentication.FormsCookiePath);
                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    // Create the cookie.
                    Response.Cookies.Add(new
                HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    //FormsAuthentication.SetAuthCookie(u1.id.ToString(), true);
                    Response.Cookies.Add(new HttpCookie("Name", u1.Name) { Expires = DateTime.Now.AddMinutes(expiretime) });
                    Response.Cookies.Add(new HttpCookie("Email", u1.Email) { Expires = DateTime.Now.AddMinutes(expiretime) });
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("error", "Invalid Email or Password");
                }
            }
            else
            {
                ModelState.AddModelError("error", "Empty fields");
            }
            return View("Index");
        }
        public ActionResult SignOut(User u1)
        {
            Response.Cookies.Add(new HttpCookie("Name") { Expires = DateTime.Now.AddMinutes(-1) });
            Response.Cookies.Add(new HttpCookie("Email") { Expires = DateTime.Now.AddMinutes(-1) });

            FormsAuthentication.SignOut();
            
            return RedirectToAction("Index");
        }
    }
}