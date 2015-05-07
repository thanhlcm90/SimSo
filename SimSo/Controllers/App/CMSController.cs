using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimSo.Controllers.App
{
    public class CMSController : Controller
    {
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                TempData["SignedIn"] = User.Identity.IsAuthenticated;
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return View("Login");
            }
            TempData["SignedIn"] = User.Identity.IsAuthenticated;
            return View();
        }
    }
}