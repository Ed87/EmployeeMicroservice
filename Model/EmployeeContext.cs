using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base (options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmploymentType>().HasData(
                new EmploymentType
                {
                    EmploymentTypeId = 1,
                    Name = "Contract"

                },
                new EmploymentType
                {
                    EmploymentTypeId = 2,
                    Name = "Permanent"

                },
                new EmploymentType
                {
                    EmploymentTypeId = 3,
                    Name = "Director"

                }
            );
        }

    }
}
