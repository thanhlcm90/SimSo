using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBAdmin.Models.App.Repository
{
    public class SimTypeRepo
    {
        AppDbContext context = null;
        public SimTypeRepo()
        {
            context = new AppDbContext();
        }

        public SimType GetByNumber(string number)
        {
            return null;
        }
    }
}