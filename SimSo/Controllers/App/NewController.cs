using SimSo.Models.App;
using SimSo.Models.App.Repository;
using System.Web;
using System.Web.Mvc;
using System.Linq;
namespace SimSo.Controllers.App
{
    [Authorize(Roles = "QuanLy, NhanVien")]
    public class NewController : Controller
    {
        GenericRepository<New> context = null;
        public NewController()
        {
            context = new GenericRepository<New>();
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            return Json(context.GetAll(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetByMenu(int? menuID, int pageIndex, int pageSize)
        {
            return Json(new NewRepository().GetNewsByMenu(menuID, pageIndex, pageSize), JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(New model)
        {
            model.CreateDate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                if (model.IDMenu != null)
                {
                    using (var entities = new AppDbContext())
                    {
                        if (entities.News.Where(n => n.IDMenu == model.IDMenu && n.isDeleted == false).Count() > 0)
                        {
                            var menu = entities.Menus.Find(model.IDMenu);
                            menu.Type = 0;      //group
                            entities.Entry(menu).State = System.Data.Entity.EntityState.Modified;
                            entities.SaveChanges();
                        }
                        else
                        {
                            var menu = entities.Menus.Find(model.IDMenu);
                            menu.Type = 1;
                            entities.Entry(menu).State = System.Data.Entity.EntityState.Modified;
                            entities.SaveChanges();
                        }
                    }
                }
                var data = context.Insert(model);
                context.Save();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult Update(New model)
        {
            model.LastUpdate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                if (model.IDMenu != null)
                {
                    using (var entities = new AppDbContext())
                    {
                        if (entities.News.Where(n => n.IDMenu == model.IDMenu && n.isDeleted == false).Count() > 0)
                        {
                            var menu = entities.Menus.Find(model.IDMenu);
                            menu.Type = 0;      //group
                            entities.Entry(menu).State = System.Data.Entity.EntityState.Modified;
                            entities.SaveChanges();
                        }
                        else
                        {
                            var menu = entities.Menus.Find(model.IDMenu);
                            menu.Type = 1;
                            entities.Entry(menu).State = System.Data.Entity.EntityState.Modified;
                            entities.SaveChanges();
                        }
                    }
                }
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