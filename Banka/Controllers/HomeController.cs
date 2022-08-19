using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Banka.Models;

namespace Banka.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
    

        public ActionResult Login()
        {


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
       
        public ActionResult Login(Kullanıcılar k)
        {
            BankaEntities1 n = new BankaEntities1();
            Kullanıcılar user = n.Kullanıcılar.FirstOrDefault(x => x.Tc == k.Tc && x.sifre == k.sifre);
            if (user != null)
            {
                Session["Tc"] = user.Tc;
                FormsAuthentication.SetAuthCookie(user.Adı_Soyadı , false);
                
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.mesaj = "Kullanıcı Adı veya Şifre hatalı";
                return View();
            }
            


        }
        public ActionResult LogOut()
        {
            string name = FormsAuthentication.FormsCookieName;
            HttpCookie authcookie = HttpContext.Request.Cookies[name];
            FormsAuthenticationTicket t = FormsAuthentication.Decrypt(authcookie.Value);
            string tname = t.Name;

            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }




    }
}