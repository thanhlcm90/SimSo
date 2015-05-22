using LinqToExcel;
using Microsoft.AspNet.Identity;
using SimSo.Helper;
using SimSo.Models.App;
using SimSo.Models.App.Repository;
using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;

namespace SimSo.Controllers.App
{
    [Authorize]
    public class SIMController : Controller
    {
        GenericRepository<SIM> context = null;
        SIMRepository simRepo = null;

        // get list sim filter // dai ly
        [HttpGet]
        public ActionResult GetSIMsByNumber(string number, int pageIndex, int pageSize)
        {
            simRepo = new SIMRepository();
            return Json(simRepo.CheckSimsByNumber(number, pageIndex, pageSize), JsonRequestBehavior.AllowGet);
        }

        // get all sim // quan ly+nhan vien
        [HttpGet]
        public ActionResult GetPageSim(int pageIndex, int pageSize, int? included)
        {
            simRepo = new SIMRepository();
            return Json(simRepo.GetPageSim(pageIndex, pageSize, included), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            context = new GenericRepository<SIM>();
            var data = context.Get(id);
            if (data != null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        [AllowAnonymous]
        public ActionResult GetByNumber(string number)
        {
            simRepo = new SIMRepository();
            var data = simRepo.GetByNumber(number);
            if (data != null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Create(SIM model)
        {
            model.CreateDate = System.DateTime.Now;
            model.Status = 0;
            if (ModelState.IsValid)
            {
                context = new GenericRepository<SIM>();
                model.Number = StringHelper.FormatNumber(model.Number);
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
                model.Number = StringHelper.FormatNumber(model.Number);
                context = new GenericRepository<SIM>();
                context.Update(model);
                context.Save();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DuyetTrang(int page, int pageSize, string updateBy, int status, int? included)
        {
            simRepo = new SIMRepository();
            simRepo.DuyetPage(page, pageSize, updateBy, status, included);
        }

        [HttpPost]
        public ActionResult Approve_Sim(SIM sim)
        {
            sim.LastUpdate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                simRepo = new SIMRepository();
                simRepo.Approve_Sim(sim);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void Delete(int id)
        {
            context = new GenericRepository<SIM>();
            context.Delete(id);
            context.Save();
        }

        [HttpPost]
        public ActionResult UploadFile(int? supID)
        {
            System.Web.HttpPostedFileBase file = Request.Files["excel"];
            if (file != null)
            {
                string path = Server.MapPath(@"~/Content/FileTemp/");
                string fileName = file.FileName;
                try
                {
                    file.SaveAs(path + fileName);
                    int? supplierId = 0;
                    if (supID != null)
                    {
                        supplierId = supID;
                    }
                    else
                    {
                        using (var entities = new AppDbContext())
                        {
                            string userName = User.Identity.GetUserName();
                            var supp = entities.Suppliers.Where(st => st.UserName == userName);
                            if (supp.Count() > 0)
                            {
                                supplierId = supp.First().ID;
                            }
                            else
                            {
                                return Json("Chưa chọn đại lý!", JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    ImportSimFromExcel(path + fileName, supplierId);
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                catch (System.Exception exp)
                {
                    return Json(exp.Message, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    System.IO.File.Delete(path + fileName);
                }
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        void ImportSimFromExcel(string path, int? supID)
        {
            string pathToExcelFile = path;
            var excelFile = new ExcelQueryFactory(pathToExcelFile);
            var sims = from a in excelFile.Worksheet(0) select a;
            context = new GenericRepository<SIM>();
            var stRepo = new SimTypeRepo();
            var nwRepo = new NetworkRepo();
            foreach (var sim in sims)
            {
                var SIM = new SIM();
                SIM.Number = StringHelper.FormatNumber(sim[0].ToString());
                try
                {
                    SIM.Price = decimal.Parse(sim[1].ToString());
                    SIM.Price_Sup = SIM.Price;
                }
                catch
                {
                    SIM.Price = 0;
                    SIM.Price_Sup = 0;
                }
                SIM.TrangThaiSim = sim[2].ToString();
                SIM.CamKet = sim[3].ToString();
                SIM.Status = 0;
                SIM.isActive = true;
                SIM.isDeleted = false;
                SIM.CreateBy = User.Identity.Name;
                SIM.CreateDate = System.DateTime.Now;
                SIM.SimType_ID = stRepo.SimTypeGetTypeBySim(SIM.Number);
                SIM.NetWork_ID = nwRepo.GetIdByNumber(SIM.Number);
                SIM.Supplier_ID = supID;
                context.Insert(SIM);
            }
            context.Save();
            nwRepo.Dispose();
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
                if (simRepo != null)
                {
                    simRepo.Dispose();
                    simRepo = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}