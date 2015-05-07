using SimSo.Models.App;
using SimSo.Models.App.Repository;
using System.Web.Mvc;
using System.Linq;
namespace SimSo.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext context;
        public HomeController()
        {
            context = new AppDbContext();
        }
        public ActionResult Index(int? simtype, int? network, decimal? price_min, decimal? price_max, string searchStr, string year, string orderBy, int? page)
        {
            ViewBag.Menus = context.Menus.OrderBy(m => m.Stand).AsEnumerable();
            ViewBag.Networks = context.NetWorks.AsEnumerable();
            ViewBag.SimTypes = new SimTypeRepo().GetSimTypes();
            //
            ViewBag.Page = page ?? 1;
            ViewBag.PageSize = 20;
            ViewBag.Simtype = simtype;
            ViewBag.Network = network;
            ViewBag.Price_Min = price_min;
            ViewBag.Price_Max = price_max;
            ViewBag.SearchStr = searchStr;
            ViewBag.OrderBy = orderBy;

            var filter = new SIMFilter()
            {
                stId = simtype,
                nwId = network,
                price_min = price_min,
                price_max = price_max,
                searchStr = searchStr,
                year = year,
                pageIndex = page ?? 1,
                pageSize = 20,
                orderBy = orderBy
            };
            var model = new SIMRepository().GetSimsFilter(filter);
            return View(model);
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