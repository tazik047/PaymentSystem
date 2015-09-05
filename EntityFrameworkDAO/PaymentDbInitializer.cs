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

        protected override void Seed(PaymentDbContext context)
        {
            context.SaveChanges();
        }
    }
}
