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
        public IEnumerable<SIM> GetSimsFilter(SIMFilter filter)
        {
            //var data = from sim in context.SIMs 
            //           where (sim.isDeleted==false||sim.isDeleted==filter.deletedIncluded)&&
            //           (sim.isActive==true || sim.isActive!=filter.activeIncluded)&&
            //           (filter.nwId==null||sim.NetWork_ID==filter.nwId)&&
            //           (filter.spId==null||filter.spId==sim.Supplier_ID)&&
            //           (filter.stId==null||filter.stId==sim.SimType_ID)&&
            //           filter.
            return null;
        }

        public IEnumerable<SIMViewModel> GetAll()
        {
            var data = from sim in context.SIMs select sim;
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
                    Status = sim.Status == 1 ? "Available" : "Unavailable",
                    isActive = sim.isActive,
                    isDelete = sim.isDeleted
                };
            }
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