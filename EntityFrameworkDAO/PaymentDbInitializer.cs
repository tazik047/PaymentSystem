using EntityFrameworkDAO.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using Microsoft.AspNet.Identity;

namespace EntityFrameworkDAO
{
    class PaymentDbInitializer : DropCreateDatabaseIfModelChanges<PaymentDbContext>
    {

        protected override void Seed(PaymentDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<User>(context));

            // Create roles for system.
            var roles = new[]
            {
                new IdentityRole("Admin"),
                new IdentityRole("Support"),
                new IdentityRole("User"),
            };
            foreach (var r in roles)
            {
                context.Roles.Add(r);
            }
            context.SaveChanges();

            // Create users.
            var users = new[]
            {
                new User
                {
                    Email = "admin@payment.com", 
                    UserName = "admin@payment.com", 
                    LastName = "Admin", 
                    FirstName = "Admin"
                },
                new User
                {
                    Email = "support1@payment.com", 
                    UserName = "support1@payment.com",
                    FirstName = "Support1",
                    LastName = "Support1"
                },
                new User
                {
                    Email = "support2@payment.com", 
                    UserName = "support2@payment.com",
                    FirstName = "Support2",
                    LastName = "Support2"
                },
                new User
                {
                    Email = "stas249501@gmail.com",
                    UserName = "stas249501@gmail.com",
                    FirstName = "Станислав",
                    LastName = "Задорожний",
                    PhoneNumber = "(095) 312-38-38"
                },
                new User
                {
                    Email = "test@test.com",
                    UserName = "test@test.com",
                    FirstName = "ТестИмя",
                    LastName = "ТестФамилия",
                    PhoneNumber = "(093) 973-79-89"
                }
            };
            foreach (var user in users)
            {
                CheckResult(userManager.Create(user));
                CheckResult(userManager.AddPassword(user.Id, "password"));

            }
            context.SaveChanges();

            // Set roles to main users.
            CheckResult(userManager.AddToRole(users[0].Id, "Admin"));
            CheckResult(userManager.AddToRole(users[1].Id, "Support"));
            CheckResult(userManager.AddToRole(users[2].Id, "Support"));
            context.SaveChanges();

            // Set roles to other users.
            for (int i = 3; i < users.Length; i++)
                CheckResult(userManager.AddToRole(users[i].Id, "User"));
            context.SaveChanges();

            // Create accounts for users.
            var accounts = new[]
            {
                new Account
                {
                    User = users[2],
                    Card = new Card
                    {
                        Name = "Стипендеальная",
                        Cvc = 1123,
                        ExpirationDate = "09 / 2020",
                        Number = "5168123412341234",
                        FullName = "Задорожний Станислав"
                    },
                    CreationDate = new DateTime(2015, 9, 9),
                    Balance = 200,
                    Operations = new List<Operation>
                    {
                        new MobileOperation
                        {
                            MobileNumber = "(095) 312-38-38",
                            Amount = 150, OperationDate = new DateTime(2015,09,10),
                            Type = OperationType.Replenishment
                        },
                        new CardOperation
                        {
                            CardNumber = "5168123412341234",
                            OperationDate = new DateTime(2015, 9, 19),
                            Amount = 50,
                            Type = OperationType.Replenishment
                        }
                    },
                    IsBlocked = false
                },
                new Account
                {
                    User = users[2],
                    Card = new Card
                    {
                        Name = "Универсальная",
                        Cvc = 777,
                        ExpirationDate = "09 / 2018",
                        Number = "5168123412344321",
                        FullName = "Задорожний Станислав"
                    },
                    CreationDate = new DateTime(2015, 9, 15),
                    Balance = 50,
                    Operations = new List<Operation>
                    {
                        new MobileOperation
                        {
                            MobileNumber = "(095) 312-38-38",
                            Amount = 100, OperationDate = new DateTime(2015,9,16),
                            Type = OperationType.Replenishment
                        },
                        new CardOperation
                        {
                            CardNumber = "5168123412341234",
                            OperationDate = new DateTime(2015, 9, 19),
                            Amount = 50,
                            Type = OperationType.Paymnet
                        }
                    },
                    IsBlocked = true
                }
            };
            context.Accounts.AddRange(accounts);
            context.SaveChanges();

            base.Seed(context);
        }

        private void CheckResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new Exception("False");
            }
        }
    }
}
