using SBAdmin.Models.App;
using SBAdmin.Models.App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBAdmin.Controllers.App
{
    [Authorize(Roles="QuanLy, NhanVien")]
    public class OrderController : Controller
    {
        private GenericRepository<Order> context = null;
        public OrderController()
        {
            context = new GenericRepository<Order>();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Json(new OrderRepository().GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var data = new OrderRepository().Get(id);
            if (data != null)
                return Json(data, JsonRequestBehavior.AllowGet);
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Create(Order model)
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
        public ActionResult Update(Order model)
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