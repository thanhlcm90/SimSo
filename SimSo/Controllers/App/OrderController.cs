using SimSo.Models.App;
using SimSo.Models.App.Repository;
using SimSo.Repository;
using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;

namespace SimSo.Controllers.App
{
    [Authorize(Roles = "QuanLy, NhanVien")]
    public class OrderController : Controller
    {
        private GenericRepository<Order> context = null;
        private AppDbContext entities = null;
        private OrderRepository orderRepo = null;
        public OrderController()
        {

        }

        [HttpGet]
        public ActionResult GetAll(int pageIndex, int pageSize, int? status, string searchNumber)
        {
            orderRepo = new OrderRepository();
            var userBusiness = User.Identity.Name;
            return Json(orderRepo.GetAll(pageIndex, pageSize, status, searchNumber, userBusiness), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            orderRepo = new OrderRepository();
            var data = orderRepo.Get(id);
            if (data != null)
                return Json(data, JsonRequestBehavior.AllowGet);
            return HttpNotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create(string number)
        {
            entities = new AppDbContext();
            // layout
            ViewBag.Networks = entities.NetWorks.AsEnumerable();
            ViewBag.Menus = entities.Menus.OrderBy(m => m.Stand).ToList();
            ViewBag.SimTypes = new SimTypeRepo().GetSimTypes();
            ViewBag.TopOrders = entities.Orders.Where(o => o.Status == 1).OrderByDescending(o => o.CreateDate).Take(5);
            ViewBag.TopNews = new NewRepository().GetTopNews();
            ViewBag.Tags = new NetworkRepo().GetTagsById(null);
            StatisticRepo statisRepo = new StatisticRepo();
            ViewBag.TotalCount = statisRepo.GetTotalCount();
            ViewBag.ToDayCount = statisRepo.GetTodayCount();
            ViewBag.CurrentCount = statisRepo.GetCurrentCount();
            ViewBag.Employee = entities.Employees.Where(e => e.WorkingStatus == true).OrderBy(e => e.STT).Take(3);
            statisRepo.Dispose();
            //
            ViewBag.UTitle = "active";
            if (!String.IsNullOrEmpty(number))
            {
                ViewBag.Number = number;
                var data = entities.SIMs.Where(s => s.Number == number).OrderByDescending(s => s.Price);
                if (data.Count() != 0)
                {
                    var s = data.First();
                    ViewBag.NwName = (from nw in entities.NetWorks where nw.ID == s.NetWork_ID select nw.Name).First();
                }
            }
            return View();
        }

        // api
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(Order model)
        {
            model.CreateDate = System.DateTime.Now;
            model.Status = model.Status ?? 1;
            context = new GenericRepository<Order>();
            model.Number = Helper.StringHelper.FormatNumber(model.Number);
            if (ModelState.IsValid)
            {
                var data = context.Insert(model);
                context.Save();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        // MVC
        [HttpPost]
        [AllowAnonymous]
        public ActionResult OrderSim(Order model)
        {
            model.CreateDate = System.DateTime.Now;
            model.Status = model.Status ?? 1;
            model.Number = Helper.StringHelper.FormatNumber(model.Number);
            context = new GenericRepository<Order>();
            entities = new AppDbContext();
            Random r = new Random();
            var x = entities.Employees.Where(e => e.WorkingStatus == true);
            try
            {
                int skipTo = r.Next(0, x.Count()) - 1;
                model.UserBusiness = x.OrderBy(e => e.ID).Skip(skipTo).Take(1).First().UserName;
            }
            catch
            {
                model.UserBusiness = String.Empty;
            }

            if (ModelState.IsValid)
            {
                var data = context.Insert(model);
                context.Save();
                return RedirectToAction("Index", "Home");
            }
            return View("Create");
        }

        [HttpPost]
        public ActionResult Update(Order model)
        {
            model.LastUpdate = System.DateTime.Now;
            context = new GenericRepository<Order>();
            entities = new AppDbContext();
            if (ModelState.IsValid)
            {
                context.Update(model);
                using (var scope = new TransactionScope())
                {
                    if (model.Status == 4)
                    {
                        var simClient = entities.Sim_Clients.Find(model.Number);
                        entities.Sim_Clients.Remove(simClient);
                    }
                    entities.SaveChanges();
                    context.Save();
                    scope.Complete();
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void Delete(int id)
        {
            context = new GenericRepository<Order>();
            context.Delete(id);
            context.Save();
        }

        [HttpGet]
        public JsonResult GetCountByStatus()
        {
            orderRepo = new OrderRepository();
            return Json(orderRepo.GetCountByStatus(User.Identity.Name), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCustomerOrders(string mobile)
        {
            orderRepo = new OrderRepository();
            return Json(orderRepo.GetCustomerOrders(mobile), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTopOrders()
        {
            entities = new AppDbContext();
            var data = entities.Orders.Where(x => x.Status == 4).OrderByDescending(m => m.LastUpdate).Take(10).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
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
                if (orderRepo != null)
                {
                    orderRepo.Dispose();
                    orderRepo = null;
                }
                if (entities != null)
                {
                    entities.Dispose();
                    entities = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}