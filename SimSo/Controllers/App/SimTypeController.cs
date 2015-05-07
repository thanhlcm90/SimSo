using SimSo.Models.App;
using SimSo.Models.App.Repository;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
namespace SimSo.Controllers.App
{
    [Authorize]
    public class SimTypeController : Controller
    {
        GenericRepository<SimType> context = null;
        public SimTypeController()
        {
            context = new GenericRepository<SimType>();
        }
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

        [HttpGet]
        public ActionResult GetByNumber(string number)
        {
            return Json(new SimTypeRepo().SimTypeGetTypeBySim(number), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "QuanLy")]
        public ActionResult Create(SimType model)
        {
            if (ModelState.IsValid)
            {
                var data = context.Insert(model);
                context.Save();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "QuanLy")]
        public ActionResult Update(SimType model)
        {
            if (ModelState.IsValid)
            {
                context.Update(model);
                context.Save();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "QuanLy")]
        [HttpPost]
        public void Delete(int id)
        {
            context.Delete(id);
            context.Save();
        }

        [HttpGet]
        public JsonResult SimTypeName()
        {
            var result = new SimTypeRepo().GetSimTypes();
            return Json(result, JsonRequestBehavior.AllowGet);
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