using Microsoft.AspNet.Identity;
using SimSo.Models;
using SimSo.Models.App;
using SimSo.Models.App.Repository;
using System.Transactions;

namespace SimSo.Repository
{
    public class SupplierRepo
    {
        private ApplicationUserManager UserManager = null;
        private GenericRepository<Supplier> SupContext = null;

        public SupplierRepo(ApplicationUserManager _userManager, GenericRepository<Supplier> _supRepo)
        {
            UserManager = _userManager;
            SupContext = _supRepo;
        }

        public SupplierRepo(GenericRepository<Supplier> _supRepo)
        {
            SupContext = _supRepo;
        }

        public Supplier Create(Supplier sup, RegisterViewModel model)
        {
            Supplier data = null;
            using (var scope = new TransactionScope())
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(UserManager.FindByName(user.UserName).Id, "DaiLy");
                    data = SupContext.Insert(sup);
                    SupContext.Save();
                    scope.Complete();
                }
            }
            return data;
        }

        public int? Update(Supplier sup, string currentPassword)
        {
            using (var scope = new TransactionScope())
            {
                if (currentPassword != sup.Password)
                {
                    var result = UserManager.ChangePassword(UserManager.FindByName(sup.UserName).Id, currentPassword, sup.Password);
                    if (result.Succeeded)
                    {
                        SupContext.Update(sup);
                        SupContext.Save();
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
                    SupContext.Update(sup);
                    SupContext.Save();
                    scope.Complete();
                    return 1;
                }
            }
        }

        public void Dispose()
        {
            if (SupContext != null)
            {
                SupContext.Dispose();
                SupContext = null;
            }
            if (UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
        }
    }
}