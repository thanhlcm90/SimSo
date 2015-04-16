using SBAdmin.Models.App;
using SBAdmin.Models.App.Repository;
using System.Web.Mvc;

namespace SBAdmin.Controllers.App
{
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

        public ActionResult Create(Supplier model)
        {
            if (ModelState.IsValid)
            {
                var data = context.Insert(model);
                context.Save();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public void Update(Supplier model)
        {
            if (ModelState.IsValid)
            {
                context.Update(model);
                context.Save();
            }
        }

        public void Delete(int id)
        {
            context.Delete(id);
            context.Save();
        }
    }
}