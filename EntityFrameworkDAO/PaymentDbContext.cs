using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EntityFrameworkDAO
{
    class PaymentDbContext : IdentityDbContext<User>
    {
        public PaymentDbContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new PaymentDbInitializer());
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<BankOperation> BankOperations { get; set; }
        public DbSet<CardOperation> CardOperations { get; set; }
        public DbSet<MobileOperation> MobileOperations { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Message> Messages { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>().ToTable("Operations");
            modelBuilder.Entity<BankOperation>().ToTable("BankOperations");
            modelBuilder.Entity<CardOperation>().ToTable("CardOperations");
            modelBuilder.Entity<MobileOperation>().ToTable("MobileOperations");
            modelBuilder.Entity<Card>()
                .HasRequired<Account>(c => c.Account)
                .WithRequiredDependent(a => a.Card);
            base.OnModelCreating(modelBuilder);
        }
    }
}
