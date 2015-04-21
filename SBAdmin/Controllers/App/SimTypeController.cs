﻿using SBAdmin.Models.App;
using SBAdmin.Models.App.Repository;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
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

        public void Delete(int id)
        {
            context.Delete(id);
            context.Save();
        }

        [HttpGet]
        public JsonResult SimTypeName()
        {
            var result = new List<SimType>();
            var lstST = context.GetAll();
            new SimTypeRepo().GetChildren(result, lstST, lstST.Where(s => (s.IDParent == null || s.IDParent == 0) && s.isDeleted == false), "");
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