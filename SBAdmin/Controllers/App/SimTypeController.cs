using SBAdmin.Models.App;
using SBAdmin.Models.App.Repository;
using System.Web.Mvc;

namespace SBAdmin.Controllers.App
{
    [Authorize]
    public class SimTypeController : Controller
    {
        GenericRepository<SimType> context = null;
        public SimTypeController()
        {
            context = new GenericRepository<SimType>();
        }
        public ActionResult GetListSimType()
        {
            return Json(context.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            return Json(context.Get(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(SimType model)
        {
            if (ModelState.IsValid)
            {
                var data = context.Insert(model);
                context.Save();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public void Update(SimType model)
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