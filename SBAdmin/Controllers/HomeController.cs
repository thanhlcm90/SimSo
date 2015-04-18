using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return View("Login");
            }
            TempData["SignedIn"] = User.Identity.IsAuthenticated;
            return View();
        }
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                TempData["SignedIn"] = User.Identity.IsAuthenticated;
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}