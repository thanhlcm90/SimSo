using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SBAdmin.Models.App.Repository
{
    public class SimTypeRepo
    {
        public void GetChildren(ICollection<SimType> result, IEnumerable<SimType> lstSimType, IEnumerable<SimType> parent, string prefix)
        {
            foreach (var item in parent)
            {
                result.Add(new SimType() { ID = item.ID, Name = prefix + item.Name });
                GetChildren(result, lstSimType, lstSimType.Where(r => r.IDParent == item.ID && r.isDeleted == false).ToList(), prefix + "---");
            }
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