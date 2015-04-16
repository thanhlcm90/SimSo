using SBAdmin.Models.App;
using SBAdmin.Models.App.Repository;
using System.Web.Mvc;

namespace SBAdmin.Controllers.App
{
    [Authorize]
    public class SupplierController : Controller
    {
        GenericRepository<Supplier> context = null;
        public SupplierController()
        {
            context = new GenericRepository<Supplier>();
        }
        public ActionResult GetListSupplier()
        {
            return Json(context.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            return Json(context.Get(id), JsonRequestBehavior.AllowGet);
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
    }
}