using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.CoreLayer.Utilities;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.Services.DbInitializer
{
    public interface IDBInitialize
    {
        void Initialize();
        void SeedData();
    }

    public class DBInitialize : IDBInitialize
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DBInitialize(IServiceScopeFactory serviceScopeFactory)
        {
            this._serviceScopeFactory = serviceScopeFactory;
        }

        public void Initialize()
        {
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                using (AppDbContext db = serviceScope.ServiceProvider.GetService<AppDbContext>())
                {
                    db.Database.Migrate();
                }
            }
        }



        public void SeedData()
        {
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                using (AppDbContext db = serviceScope.ServiceProvider.GetService<AppDbContext>())
                {
                    if (!db.Roles.Any())
                    {
                        db.Roles.AddRange(
                            new Role { Name = "admin" },
                            new Role { Name = "user" }
                        );
                        db.SaveChanges();
                    }

                    if (!db.Users.Any())
                    {
                        var adminUser = new User
                        {
                            FullName = "admin",
                            Password = "admin123".EncodeToMd5(),
                            UserName = "admin"
                        };
                        db.Users.Add(adminUser);
                        db.SaveChanges();

                        var adminRole = db.Roles.FirstOrDefault(r => r.Name == "admin");
                        if (adminRole != null)
                        {
                            db.UserRoles.Add(new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id });
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
