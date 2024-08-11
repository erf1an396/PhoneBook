using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.CoreLayer.Utilities;
using PhoneBook.DataLayer.Context;
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
                    if (!db.Users.Any())
                    {
                        db.Users.Add(new DataLayer.Entities.User()
                        {
                            FullName = "admin",
                            Password = "admin".EncodeToMd5(),
                            UserName = "admin",

                        });
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
