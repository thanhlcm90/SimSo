using Microsoft.AspNet.Identity.Owin;
using SimSo.Models;
using SimSo.Models.App;
using SimSo.Models.App.Repository;
using SimSo.Repository;
using System.Web;
using System.Web.Mvc;

namespace SimSo.Controllers.App
{
    [Authorize(Roles="QuanLy, NhanVien")]
    public class SupplierController : Controller
    {
        GenericRepository<Supplier> context = null;
        SupplierRepo supRepo = null;
        public SupplierController()
        {
            context = new GenericRepository<Supplier>();
        }
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            return Json(context.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var data = context.Get(id);
            if (data != null)
            {
                return Json(context.Get(id), JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Create(Supplier model, RegisterViewModel regModel)
        {
            model.CreateDate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                supRepo = new SupplierRepo(HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(), context);
                var data = supRepo.Create(model, regModel);
                if (data != null)
                    return Json(data, JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult Update(Supplier model, string currentPassword)
        {
            model.LastUpdate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                supRepo = new SupplierRepo(HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(), context);
                int? result = supRepo.Update(model, currentPassword);
                if (result != null)
                {
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        public void Delete(int id)
        {
            context.Delete(id);
            context.Save();
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