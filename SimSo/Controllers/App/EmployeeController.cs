using Microsoft.AspNet.Identity.Owin;
using SimSo.Models;
using SimSo.Models.App;
using SimSo.Models.App.Repository;
using SimSo.Repository;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimSo.Controllers.App
{
    [Authorize(Roles = "QuanLy")]
    public class EmployeeController : Controller
    {
        GenericRepository<Employee> context = null;
        EmployeeRepo empRepo = null;
        public EmployeeController()
        {
            context = new GenericRepository<Employee>();
        }

        public ActionResult GetAll()
        {
            return Json(context.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            var data = context.Get(id);
            if (data != null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Create(Employee model, RegisterViewModel regModel)
        {
            model.CreateDate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                empRepo = new EmployeeRepo(HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(), context);
                var data = empRepo.Create(model, regModel);
                if (data != null)
                    return Json(data, JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult Update(Employee model, string currentPassword)
        {
            model.LastUpdate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                empRepo = new EmployeeRepo(HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(), context);
                int? result = empRepo.Update(model, currentPassword);
                if (result != null)
                {
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void Delete(int id)
        {
            context.Delete(id);
            context.Save();
        }

        //[HttpPost]
        //public ActionResult UploadImage()
        //{
        //    HttpPostedFileBase photo = Request.Files["photo"];
        //    if (photo != null && photo.ContentType.ToLower().Contains("image"))
        //    {
        //        string path = Server.MapPath(@"~/Content/Images/");
        //        string photoName = System.DateTime.Now.ToFileTime() + "_" + photo.FileName;
        //        photo.SaveAs(path + photoName);
        //        return Json(@"/Content/Images/" + photoName, JsonRequestBehavior.AllowGet);
        //    }
        //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
                if (empRepo != null)
                {
                    empRepo.Dispose();
                    empRepo = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}