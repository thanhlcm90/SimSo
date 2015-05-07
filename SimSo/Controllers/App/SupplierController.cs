using SimSo.Models.App;
using SimSo.Models.App.Repository;
using System.Web.Mvc;

namespace SimSo.Controllers.App
{
    [Authorize(Roles="QuanLy, NhanVien")]
    public class SupplierController : Controller
    {
        GenericRepository<Supplier> context = null;
        public SupplierController()
        {
            context = new GenericRepository<Supplier>();
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

        [HttpPost]
        public ActionResult Create(Supplier model)
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
        public ActionResult Update(Supplier model)
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