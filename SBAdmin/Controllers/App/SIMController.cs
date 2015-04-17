using SBAdmin.Models.App;
using SBAdmin.Models.App.Repository;
using System.Web.Mvc;

namespace SBAdmin.Controllers.App
{
    [Authorize]
    public class SIMController : Controller
    {
        GenericRepository<SIM> context = null;
        public SIMController()
        {
            context = new GenericRepository<SIM>();
        }
        // get list sim filter
        public ActionResult GetListSIM(SIMFilter filter)
        {
            return Json(context.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAll()
        {
            return Json(new SIMRepository().GetAll(), JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(SIM model)
        {
            model.CreateDate = System.DateTime.Now;
            model.Status = true;
            if (ModelState.IsValid)
            {
                var data = context.Insert(model);
                context.Save();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult Update(SIM model)
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