using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBAdmin.Models.App.Repository
{
    public class NetworkRepo
    {
        AppDbContext context = null;
        public NetworkRepo()
        {
            context = new AppDbContext();
        }
        public NetWork GetByNumber(string number)
        {
            var net = from n in context.NetWorks
                      where (n.isDeleted == false && (n.Number.Contains(number.Substring(0, 3)) || n.Number.Contains(number.Substring(0, 4))))
                      select n;
            if (net.Count() > 0)
                return net.First();
            return null;
        }
    }
}