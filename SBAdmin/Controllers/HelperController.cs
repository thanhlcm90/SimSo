using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBAdmin.Controllers
{
    [Authorize]
    public class HelperController : Controller
    {
        // GET: Helper
        [HttpPost]
        public ActionResult UploadFile(string name)
        {
            HttpPostedFileBase photo = Request.Files[name];
            if (photo != null)
            {
                string path = Server.MapPath(@"~/Content/FileUpload/");
                string photoName = System.DateTime.Now.ToFileTime() + "_" + photo.FileName;
                photo.SaveAs(path + photoName);
                return Json(@"/Content/FileUpload/" + photoName, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}