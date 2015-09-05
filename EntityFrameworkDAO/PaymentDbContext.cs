﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;

namespace EntityFrameworkDAO
{
    class PaymentDbContext : DbContext
    {
        public static readonly  PaymentDbContext Instance = new PaymentDbContext();

        public PaymentDbContext()
            : base("PaymentDbContext")
        {
            Database.SetInitializer(new PaymentDbInitializer());
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<User> Users { get; set; } 
    }
}
