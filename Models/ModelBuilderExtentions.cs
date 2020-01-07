using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    public static class ModelBuilderExtentions
    {
        /// <summary>
        /// Hard coded values which we want to add to the Database
        /// ! ! ! Remember to Migrate this seed data also ! ! !
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 8888,
                    Name = "SeedData = BROKE",
                    Email = "SeedMail@LOL.com",
                    Department = Dept.Payroll
                },
                new Employee
                {
                    Id = 9999,
                    Name = "SeedData = BACK",
                    Email = "SeedMail@LOL.com",
                    Department = Dept.Payroll
                });
        }
    }
}
