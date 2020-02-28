using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApiJwtExample.Entities;

namespace NetCoreWebApiJwtExample.DataAccess.Concrete.EntityFrameworkCore
{
    public class DatabaseTaskContext:DbContext
    {
        public DatabaseTaskContext()
        {
            
        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DENIZ-PC;Database=WebApiDB;Integrated Security=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(new Person()
            {
                Id = 1,
                Name = "Denizzz",
                Department = "Bilisim"
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
