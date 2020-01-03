using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    //public class AppDdContext : IdentityDbContext // the default IdentityUser
    public class AppDdContext : IdentityDbContext<ApplicationUser> // Need this for our custom IdentityUser class (ApplicationUser)
    {
        // Need to make a set for every Type(Model) you want inn the DB
        // The Db sets will allow you to query and save to the Type you specified
        public DbSet<Employee> Employees { get; set; }


        public AppDdContext(DbContextOptions<AppDdContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
