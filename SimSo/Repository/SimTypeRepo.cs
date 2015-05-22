using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SimSo.Models.App.Repository
{
    public class SimTypeRepo
    {
        public void GetChildren(ICollection<SimType> result, IEnumerable<SimType> lstSimType, IEnumerable<SimType> parent, string prefix)
        {
            foreach (var item in parent)
            {
                result.Add(new SimType() { ID = item.ID, Name = prefix + item.Name});
                GetChildren(result, lstSimType, lstSimType.Where(r => r.IDParent == item.ID && r.isDeleted == false).ToList(), prefix + "---");
            }
        }

        public IEnumerable<SimType> GetSimTypes()
        {
            var result = new List<SimType>();
            var lstST = new AppDbContext().SimTypes.ToList();
            new SimTypeRepo().GetChildren(result, lstST, lstST.Where(s => (s.IDParent == null || s.IDParent == 0) && s.isDeleted == false), "");
            return result;
        }

        public int SimTypeGetTypeBySim(string number)
        {
            using (var entities = new AppDbContext())
            {
                return entities.Database.SqlQuery<int>("SimTypeGetTypeBySim @SimNumber", new SqlParameter("@SimNumber", number)).First();
            }
        }
    }
}