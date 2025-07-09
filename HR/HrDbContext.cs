using HR.Models;
using Microsoft.EntityFrameworkCore;

namespace HR
{
    // DbContext Class: A gateway to the database
    // This HrDbContext class represents the entire HR database
    public class HrDbContext : DbContext
    {
        // DbContextOptions is a setup for two things:
        // 1. Type of the database.
        // 2. Connection to the database.
        public HrDbContext(DbContextOptions<HrDbContext> options) : base(options) {}

        //----------------------------------------------------------------------
        // The following is the section where we define the tables represented
        // inside the HR database.

        // Employees Table
        // DbSet - a list of records of type T from the database
        // Any Model MUST match 100% the same structure as the one in the table
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
