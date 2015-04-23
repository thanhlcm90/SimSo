using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBAdmin.Models.App.Repository
{
    public class NetworkRepo
    {
        public int? GetIdByNumber(string number)
        {
            using (var entities = new AppDbContext())
            {
                var data = from nw in entities.NetWorks
                           where nw.isActive == true && (nw.Number.Contains(number.Substring(0, 3)) || nw.Number.Contains(number.Substring(0, 4)))
                           select nw;
                if (data.Count() > 0)
                {
                    return data.First().ID;
                }
                return null;
            }
        }
    }
}