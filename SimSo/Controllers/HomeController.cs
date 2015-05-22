using SimSo.Models.App;
using SimSo.Models.App.Repository;
using System.Web.Mvc;
using System.Linq;
using SimSo.Repository;
namespace SimSo.Controllers
{
    public class HomeController : Controller
    {
        private const int PageSize = 20;
        AppDbContext context = null;
        public HomeController()
        {
            context = new AppDbContext();
            GetData();
        }

        public void GetData()
        {
            ViewBag.Menus = context.Menus.OrderBy(m => m.Stand).AsEnumerable();
            ViewBag.Networks = context.NetWorks.AsEnumerable();
            ViewBag.SimTypes = new SimTypeRepo().GetSimTypes();
            ViewBag.TopOrders = context.Orders.Where(o => o.Status == 1).OrderByDescending(o => o.CreateDate).Take(5);
            ViewBag.TopNews = new NewRepository().GetTopNews();
            StatisticRepo statisRepo = new StatisticRepo();
            ViewBag.TotalCount = statisRepo.GetTotalCount();
            ViewBag.ToDayCount = statisRepo.GetTodayCount();
            ViewBag.CurrentCount = statisRepo.GetCurrentCount();
            ViewBag.Employee = context.Employees.Where(e => e.WorkingStatus == true).OrderBy(e => e.STT).Take(3);
            statisRepo.Dispose();
        }

        //[HttpPost]
        public ActionResult Index(int? simtype, int? network, decimal? price_min, decimal? price_max, string searchStr, int? page, int? numCount, int? pageSize, string orderBy)
        {
            ViewBag.Page = page ?? 1;
            ViewBag.PageSize = pageSize ?? PageSize;
            ViewBag.Simtype = simtype;
            ViewBag.Network = network;
            ViewBag.Price_Min = price_min;
            ViewBag.Price_Max = price_max;
            ViewBag.SearchStr = searchStr;
            ViewBag.OrderBy = string.IsNullOrEmpty(searchStr) ? "price" : "price-des";
            ViewBag.NumCount = numCount ?? 0;
            ViewBag.Tags = new NetworkRepo().GetTagsById(null);
            var filter = new SIMFilter()
            {
                stId = simtype ?? 0,
                nwId = network ?? 0,
                price_min = price_min ?? 0,
                price_max = price_max ?? 0,
                searchStr = string.IsNullOrEmpty(searchStr) ? "" : searchStr,
                pageIndex = page ?? 1,
                pageSize = pageSize ?? PageSize,
                orderBy = orderBy == "price" ? "0" : "1",
                numCount = numCount ?? 0,
            };
            var model = new SIMRepository().GetSimsFilter(filter);
            if (!string.IsNullOrEmpty(searchStr))
            {
                if (model.Items.Count() == 1)
                {
                    dynamic m = model.Items.First();
                    return RedirectToRoute("order", new { number = m.Number });
                }
                else if (model.Items.Count() == 0 && !searchStr.Contains("*"))
                {
                    return RedirectToRoute("order", new { number = searchStr });
                }
            }
            return View("Index", model);
        }

        [HttpGet]
        public JsonResult GetDataOrdered(int? simtype, int? network, decimal? price_min, decimal? price_max, string searchStr, int? page, int? numCount, int? pageSize, string orderBy)
        {
            ViewBag.Page = page ?? 1;
            ViewBag.PageSize = pageSize ?? PageSize;
            ViewBag.Simtype = simtype;
            ViewBag.Network = network;
            ViewBag.Price_Min = price_min;
            ViewBag.Price_Max = price_max;
            ViewBag.SearchStr = searchStr;
            ViewBag.OrderBy = orderBy;
            ViewBag.NumCount = numCount ?? 0;
            var filter = new SIMFilter()
            {
                stId = simtype ?? 0,
                nwId = network ?? 0,
                price_min = price_min ?? 0,
                price_max = price_max ?? 0,
                searchStr = string.IsNullOrEmpty(searchStr) ? "" : searchStr,
                pageIndex = page ?? 1,
                pageSize = pageSize ?? PageSize,
                orderBy = orderBy,
                numCount = numCount ?? 0,
            };
            var model = new SIMRepository().GetSimsFilter(filter);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Network(string name, int? simtype, decimal? price_min, decimal? price_max, string searchStr, int? page, int? numCount, int? pageSize, string orderBy)
        {
            var data = context.NetWorks.ToList().Where(n => string.Compare(Helper.StringHelper.UnicodeParse(n.Name), name, true) == 0);
            if (data.Count() == 0)
                return RedirectToAction("Index");
            var nw = data.First();
            ViewBag.Page = page ?? 1;
            ViewBag.PageSize = PageSize;
            ViewBag.Simtype = simtype;
            ViewBag.Network = nw.ID;
            ViewBag.Title = nw.Title;
            ViewBag.Description = nw.Description;
            ViewBag.Price_Min = price_min;
            ViewBag.Price_Max = price_max;
            ViewBag.SearchStr = searchStr;
            ViewBag.OrderBy = string.IsNullOrEmpty(searchStr) ? "price" : "price-des";
            ViewBag.NumCount = numCount ?? 0;
            ViewBag.Keywords = context.NetWorks.Find(nw.ID) != null ? context.NetWorks.Find(nw.ID).Keyword : "";
            ViewBag.Tags = new NetworkRepo().GetTagsById(nw.ID);

            var filter = new SIMFilter()
            {
                stId = simtype ?? 0,
                nwId = nw.ID,
                price_min = price_min ?? 0,
                price_max = price_max ?? 0,
                searchStr = string.IsNullOrEmpty(searchStr) ? "" : searchStr,
                pageIndex = page ?? 1,
                pageSize = pageSize ?? PageSize,
                orderBy = orderBy == "price" ? "0" : "1",

                numCount = numCount ?? 0,
            };
            var model = new SIMRepository().GetSimsFilter(filter);
            if (!string.IsNullOrEmpty(searchStr))
            {
                if (model.Items.Count() == 1)
                {
                    dynamic m = model.Items.First();
                    return RedirectToRoute("order", new { number = m.Number });
                }
                else if (model.Items.Count() == 0)
                {
                    return RedirectToRoute("order", new { number = searchStr });
                }
            }
            return View(model);
        }

        public ActionResult SimType(string title, int? network, decimal? price_min, decimal? price_max, string searchStr, int? page, int? numCount, int? pageSize, string orderBy)
        {
            var data = context.SimTypes.ToList().Where(n => string.Compare(Helper.StringHelper.UnicodeParse(n.Name), title, true) == 0);
            if (data.Count() == 0)
                return RedirectToAction("Index");
            var st = data.First();
            ViewBag.Page = page ?? 1;
            ViewBag.PageSize = PageSize;
            ViewBag.Simtype = st.ID;
            ViewBag.Network = network;
            ViewBag.Name = st.Name;
            ViewBag.Price_Min = price_min;
            ViewBag.Price_Max = price_max;
            ViewBag.SearchStr = searchStr;
            ViewBag.OrderBy = string.IsNullOrEmpty(searchStr) ? "price" : "price-des";
            ViewBag.Tags = new NetworkRepo().GetTagsById(null);
            ViewBag.Title = st.Title;
            ViewBag.Description = st.Description;
            ViewBag.NumCount = numCount ?? 0;
            ViewBag.Keywords = context.SimTypes.Find(st.ID) != null ? context.SimTypes.Find(st.ID).Keyword : "";

            var filter = new SIMFilter()
            {
                stId = st.ID,
                nwId = network ?? 0,
                price_min = price_min ?? 0,
                price_max = price_max ?? 0,
                searchStr = string.IsNullOrEmpty(searchStr) ? "" : searchStr,
                pageIndex = page ?? 1,
                pageSize = pageSize ?? PageSize,
                orderBy = orderBy == "price" ? "0" : "1",

                numCount = numCount ?? 0,
            };
            var model = new SIMRepository().GetSimsFilter(filter);
            if (!string.IsNullOrEmpty(searchStr))
            {
                if (model.Items.Count() == 1)
                {
                    dynamic m = model.Items.First();
                    return RedirectToRoute("order", new { number = m.Number });
                }
                else if (model.Items.Count() == 0)
                {
                    return RedirectToRoute("order", new { number = searchStr });
                }
            }
            return View(model);
        }

        public ActionResult Tags(string tag)
        {
            ViewBag.Page = 1;
            ViewBag.PageSize = PageSize;
            ViewBag.Simtype = null;
            ViewBag.Network = null;
            ViewBag.Price_Min = null;
            ViewBag.Price_Max = null;
            ViewBag.SearchStr = tag + "*";
            ViewBag.OrderBy = null;
            ViewBag.NumCount = 0;
            ViewBag.Tags = new NetworkRepo().GetTagsById(null);
            var filter = new SIMFilter()
            {
                searchStr = tag + "*",
                stId = 0,
                nwId = 0,
                price_min = 0,
                price_max = 0,
                pageIndex = 1,
                numCount = 0,
                pageSize = PageSize,
                orderBy = "0",
            };
            var model = new SIMRepository().GetSimsFilter(filter);
            if (!string.IsNullOrEmpty(tag + "*"))
            {
                if (model.Items.Count() == 1)
                {
                    dynamic m = model.Items.First();
                    return RedirectToRoute("order", new { number = m.Number });
                }
                else if (model.Items.Count() == 0)
                {
                    return RedirectToRoute("order", new { number = tag + "*" });
                }
            }
            return View("Index", model);
        }

        public ActionResult Menu(string title, int? page, int? id, string titleNew)
        {
            var menus = context.Menus.ToList().Where(m => string.Compare(Helper.StringHelper.UnicodeParse(m.Name), title, true) == 0);
            if (menus.Count() == 0)
                return RedirectToAction("NotFound", "Error");
            var menu = menus.First();
            ViewBag.UTitle = title;
            ViewBag.Tags = new NetworkRepo().GetTagsById(null);
            //type==0 ==>Group
            if (menu.Type == 0)
            {
                if (id != null)
                {
                    ViewBag.RelativeNews = new NewRepository().GetRelativeNews(menu.ID, id);
                    ViewBag.Name = menu.Name;
                    var model = context.News.Where(n => n.IDMenu == menu.ID && n.ID == id);
                    if (model.Count() == 0)
                        return RedirectToRoute("menu", new { title = title });
                    var result = model.First();
                    if (Helper.StringHelper.UnicodeParse(result.Title).Equals(titleNew))
                        return View("Details", result);
                    return RedirectToRoute("menu", new { title = title, id = id, titleNew = Helper.StringHelper.UnicodeParse(result.Title) });
                }
                ViewBag.Title = menu.Name;
                ViewBag.Page = page ?? 1;
                return View("Group", new NewRepository().GetNewsByMenu(menu.ID, page ?? 1, PageSize));
            }
            else
            {
                var data = context.News.Where(n => n.IDMenu == menu.ID);
                if (data.Count() > 0)
                {
                    ViewBag.RelativeNews = new NewRepository().GetRelativeNews(menu.ID, null);
                    return View("Details", data.OrderBy(n => n.CreateBy).First());
                }
                return RedirectToAction("NotFound", "Error");
            }
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