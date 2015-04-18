using SBAdmin.Models.App;
using SBAdmin.Models.App.Repository;
using System.Web;
using System.Web.Mvc;

namespace SBAdmin.Controllers.App
{
    [Authorize]
    public class EmployeeController : Controller
    {
        GenericRepository<Employee> context = null;
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
        public ActionResult Create(Employee model)
        {
            model.CreateDate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                var data = context.Insert(model);
                context.Save();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult Update(Employee model)
        {
            model.LastUpdate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                context.Update(model);
                context.Save();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void Delete(int id)
        {
            context.Delete(id);
            context.Save();
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            HttpPostedFileBase photo = Request.Files["photo"];
            if (photo != null)
            {
                string path = Server.MapPath(@"~/Content/Images/");
                string photoName = System.DateTime.Now.ToFileTime() + "_" + photo.FileName;
                photo.SaveAs(path + photoName);
                return Json(@"/Content/Images/" + photoName, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}