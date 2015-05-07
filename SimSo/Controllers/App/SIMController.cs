using LinqToExcel;
using Microsoft.AspNet.Identity;
using SimSo.Models.App;
using SimSo.Models.App.Repository;
using System.Linq;
using System.Web.Mvc;

namespace SimSo.Controllers.App
{
    [Authorize]
    public class SIMController : Controller
    {
        GenericRepository<SIM> context = null;
        public SIMController()
        {
            context = new GenericRepository<SIM>();
        }
        // get list sim filter // dai ly
        [HttpGet]
        public ActionResult GetSIMsByNumber(string number, int pageIndex, int pageSize)
        {
            return Json(new SIMRepository().CheckSimsByNumber(number, pageIndex, pageSize), JsonRequestBehavior.AllowGet);
        }

        // get all sim // quan ly+nhan vien
        [HttpGet]
        public ActionResult GetPageSim(int pageIndex, int pageSize)
        {
            return Json(new SIMRepository().GetPageSim(pageIndex, pageSize), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var data = context.Get(id);
            if (data != null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        public ActionResult GetByNumber(string number)
        {
            var data = new SIMRepository().GetByNumber(number);
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


        [HttpPost]
        public ActionResult UploadFile()
        {
            System.Web.HttpPostedFileBase file = Request.Files["excel"];
            if (file != null)
            {
                string path = Server.MapPath(@"~/Content/FileTemp/");
                string fileName = file.FileName;
                try
                {
                    file.SaveAs(path + fileName);
                    ImportSimFromExcel(path + fileName, "Sheet1");
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                catch (System.Exception exp)
                {
                    return Json(exp, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    System.IO.File.Delete(path + fileName);
                }
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        void ImportSimFromExcel(string path, string sheetName)
        {
            string pathToExcelFile = path;
            var excelFile = new ExcelQueryFactory(pathToExcelFile);
            var sims = from a in excelFile.Worksheet(sheetName) select a;
            foreach (var sim in sims)
            {
                var SIM = new SIM();
                SIM.Number = (string)sim["Number"].Value;
                SIM.Price = decimal.Parse(sim["Price"].Value.ToString());
                SIM.Status = 0;
                SIM.isActive = true;
                SIM.isActive = false;
                SIM.CreateBy = User.Identity.Name;
                SIM.CreateDate = System.DateTime.Now;
                SIM.SimType_ID = new SimTypeRepo().SimTypeGetTypeBySim(SIM.Number);
                SIM.NetWork_ID = new NetworkRepo().GetIdByNumber(SIM.Number);
                using (var entities = new AppDbContext())
                {
                    string userName = User.Identity.GetUserName();
                    SIM.Supplier_ID = entities.Suppliers.Where(st => st.UserName == userName).First().ID;
                }
                context.Insert(SIM);
            }
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