using EntityFrameworkDAO.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDAO
{
    class PaymentDbInitializer : DropCreateDatabaseIfModelChanges<PaymentDbContext>
    {

        protected async override void Seed(PaymentDbContext context)
        {
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));
            var roles = new[] {
                new IdentityRole("Admin"),
                new IdentityRole("Support"),
                new IdentityRole("User"),
            };
            foreach(var r in roles){
                var res = await roleManager.CreateAsync(r);
                if(!res.Succeeded){
                    // todo: create exception.
                }
            }

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
