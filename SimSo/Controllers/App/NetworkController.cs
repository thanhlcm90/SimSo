using SimSo.Models.App;
using SimSo.Models.App.Repository;
using System.Web;
using System.Web.Mvc;

namespace SimSo.Controllers.App
{
    [Authorize]
    public class NetworkController : Controller
    {
        GenericRepository<NetWork> context = null;
        public NetworkController()
        {
            context = new GenericRepository<NetWork>();
        }
        public ActionResult GetAll()
        {
            return Json(context.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult Get(int id)
        {
            var data = context.Get(id);
            if (data != null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "QuanLy")]
        [HttpPost]
        public ActionResult Create(NetWork model)
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

        [Authorize(Roles = "QuanLy")]
        [HttpPost]
        public ActionResult Update(NetWork model)
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

        [Authorize(Roles = "QuanLy")]
        [HttpPost]
        public void Delete(int id)
        {
            context.Delete(id);
            context.Save();
        }

        public JsonResult GetBySimNumber(string number)
        {
            return Json(new NetworkRepo().GetBySimNumber(number), JsonRequestBehavior.AllowGet);
        }
        //[Authorize(Roles = "QuanLy")]
        //[HttpPost]
        //public ActionResult UploadFile()
        //{
        //    HttpPostedFileBase photo = Request.Files["photo"];
        //    if (photo != null)
        //    {
        //        string path = Server.MapPath(@"~/Content/Images/");
        //        string photoName = System.DateTime.Now.ToFileTime() + "_" + photo.FileName;
        //        photo.SaveAs(path + photoName);
        //        return Json(@"/Content/Images/" + photoName, JsonRequestBehavior.AllowGet);
        //    }
        //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        //}

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