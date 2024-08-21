using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<Email> Emails { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PhoneNumber>()
           .HasOne(p => p.Contact)
           .WithMany(c => c.PhoneNumbers)
           .HasForeignKey(c => c.ContactId)
           .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Email>()
            .HasOne(p => p.Contact)
            .WithMany(c => c.Emails)
            .HasForeignKey(c => c.ContactId)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<UserRole>()
                .HasKey(u => new {u.RoleId, u.UserId});

            modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);


        }


    }
}
