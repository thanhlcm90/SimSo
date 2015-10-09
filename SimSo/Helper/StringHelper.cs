using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SimSo.Helper
{
    public class StringHelper
    {
        public static string UnicodeParse(string input)
        {
            string output = "";
            if (!String.IsNullOrEmpty(input))
            {
                input = input.ToLower();
                output = Regex.Replace(input, "[àáạảãâầấậẩẫăằắặẳẵ]", "a");
                output = Regex.Replace(output, "[èéẹẻẽêềếệểễ]", "e");
                output = Regex.Replace(output, "[ìíịỉĩ]", "i");
                output = Regex.Replace(output, "[òóọỏõôồốộổỗơờớợởỡ]", "o");
                output = Regex.Replace(output, "[ùúụủũưừứựửữ]", "u");
                output = Regex.Replace(output, "[ỳýỵỷỹ]", "y");
                output = output.Replace('đ', 'd');
                output = output.Replace(' ', '-');
            }
            return output;
        }

        public static string FormatNumber(string number)
        {
            if (!String.IsNullOrEmpty(number))
                number = Regex.Replace(number, "[ .()-]", "");
            if (!number.StartsWith("0"))
            {
                number = "0" + number;
            }
            return number;
        }
        
        public static void CreateRole(string roleName)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (!roleManager.RoleExists(roleName))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = roleName;
                roleManager.Create(role);
            }
        }
    }
}
