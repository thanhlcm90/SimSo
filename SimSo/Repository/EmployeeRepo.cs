using Microsoft.AspNet.Identity;
using SimSo.Models;
using SimSo.Models.App;
using SimSo.Models.App.Repository;
using System.Transactions;

namespace SimSo.Repository
{
    public class EmployeeRepo
    {
        private ApplicationUserManager UserManager = null;
        private GenericRepository<Employee> EmpContext = null;

        public EmployeeRepo(ApplicationUserManager _userManager, GenericRepository<Employee> _empRepo)
        {
            UserManager = _userManager;
            EmpContext = _empRepo;
        }

        public EmployeeRepo(GenericRepository<Employee> _emRepo)
        {
            EmpContext = _emRepo;
        }

        public Employee Create(Employee emp, RegisterViewModel model)
        {
            Employee data = null;
            using (var scope = new TransactionScope())
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(UserManager.FindByName(user.UserName).Id, "NhanVien");
                    data = EmpContext.Insert(emp);
                    EmpContext.Save();
                    scope.Complete();
                }
            }
            return data;
        }

        public int? Update(Employee emp, string currentPassword)
        {
            using (var scope = new TransactionScope())
            {
                // var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                if (currentPassword != emp.Password)
                {
                    var result = UserManager.ChangePassword(UserManager.FindByName(emp.UserName).Id, currentPassword, emp.Password);
                    if (result.Succeeded)
                    {
                        EmpContext.Update(emp);
                        EmpContext.Save();
                        scope.Complete();
                        return 1;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    EmpContext.Update(emp);
                    EmpContext.Save();
                    scope.Complete();
                    return 1;
                }
            }
        }

        public void Dispose()
        {
            if (EmpContext != null)
            {
                EmpContext.Dispose();
                EmpContext = null;
            }
            if (UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
        }
    }
}