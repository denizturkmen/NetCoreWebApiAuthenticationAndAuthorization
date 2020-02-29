using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetCoreWebApiJwtExample.JwtIdentity
{
    public class ApplicationDbContext:DbContext
    {
        //Veritabanına hazırladığım modeli tablo olarak eklemesini söylüyorum.
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //Veritabanı olarak SQLite kullanacağımı söylüyorum.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DENIZ-PC;Database=WebApiDB;Integrated Security=true");
            base.OnConfiguring(optionsBuilder);
        }

        //Veritabanı oluşturulurken dummy data ile oluşturulmasını istiyorum.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = 1,
                FirstName = "denizturkmen",
                Username = "deniz",
                Password = "deniz",

            });
        }
    }
}
