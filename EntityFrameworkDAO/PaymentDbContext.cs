﻿using System;
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
        //public DbSet<User> Users { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>().ToTable("Operations");
            modelBuilder.Entity<BankOperation>().ToTable("BankOperations");
            modelBuilder.Entity<CardOperation>().ToTable("CardOperations");
            modelBuilder.Entity<MobileOperation>().ToTable("MobileOperations");
            base.OnModelCreating(modelBuilder);
        }

        /*public static PaymentDbContext Create()
        {
            return new PaymentDbContext();
        }*/
    }
}
