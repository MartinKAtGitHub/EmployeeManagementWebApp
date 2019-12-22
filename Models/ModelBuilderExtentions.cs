using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Remember to Migrate this seed data also
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "SeedData = BROKE",
                    Email = "SeedMail@LOL.com",
                    Department = Dept.Payroll
                },
                new Employee
                {
                    Id = 2,
                    Name = "SeedData = BACK",
                    Email = "SeedMail@LOL.com",
                    Department = Dept.Payroll
                });
        }
    }
}
