using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApiJwtExample.Entities;

namespace NetCoreWebApiJwtExample.DataAccess.Concrete.EntityFrameworkCore
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext()
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DENIZ-PC;Database=WebApiDB;Integrated Security=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                Id = 1,
                Name = "DenizTurkmen",
                Salary = 3000
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
