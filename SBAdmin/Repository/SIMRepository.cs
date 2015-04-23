using SBAdmin.Models.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBAdmin.Models.App.Repository
{
    public class SIMRepository
    {
        private AppDbContext context;

        public SIMRepository()
        {
            context = new AppDbContext();
        }

        // kiem tra sim (dai ly)
        public ListSimViewModel GetSimsByNumber(string number, int pageIndex, int itemsPerPage)
        {
            var data = (from sim in context.SIMs
                        where sim.Number.Contains(number)
                        orderby sim.Price
                        select sim);
            int count = (int)Math.Ceiling((double)data.Count() / (double)itemsPerPage);
            var sims = data.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage);
            return new ListSimViewModel { Count = count, ListSim = GetSimsChecking(sims) };
        }
        private IEnumerable<Object> GetSimsChecking(IQueryable<SIM> data)
        {
            foreach (var sim in data)
            {
                yield return new
                {
                    Number = sim.Number,
                    SupplierName = context.Suppliers.Find(sim.Supplier_ID).Name,
                    SupplierMobile = context.Suppliers.Find(sim.Supplier_ID).Mobile,
                    Price = sim.Price,
                };
            }
        }

        // quan ly sim (quan ly va nhan vien)
        private IEnumerable<SIMViewModel> GetPageSim(IQueryable<SIM> data)
        {
            foreach (var sim in data)
            {
                yield return new SIMViewModel
                {
                    ID = sim.ID,
                    Number = sim.Number,
                    Network = context.NetWorks.Find(sim.NetWork_ID),
                    SimType = context.SimTypes.Find(sim.SimType_ID).Name,
                    Supplier = context.Suppliers.Find(sim.Supplier_ID).Name,
                    Price = sim.Price,
                    Status = sim.Status == 1 ? "Đã duyệt" : "Chưa duyệt",
                    isActive = sim.isActive,
                    isDelete = sim.isDeleted
                };
            }
        }
        public ListSimViewModel GetPageSim(int pageIndex, int itemsPerPage)
        {
            var data = (from sim in context.SIMs select sim).OrderBy(s => s.Number);
            int count = (int)Math.Ceiling((double)data.Count() / (double)itemsPerPage);
            var sims = data.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage);
            return new ListSimViewModel { Count = count, ListSim = GetPageSim(sims) };
        }

        public SIM GetByNumber(string number)
        {
            var sim = from s in context.SIMs
                      where s.Number == number &&
                      s.isActive == true &&
                      s.Status == 1
                      orderby s.Price
                      select s;
            if (sim.Count() > 0)
                return sim.First();
            return null;
        }
    }
}