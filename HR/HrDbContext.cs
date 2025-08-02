using HR.Models;
using Microsoft.EntityFrameworkCore;

namespace HR
{
    // DbContext Class: A gateway to the database
    // This HrDbContext class represents the entire HR database

    // To add a migration, you need to use the command: Add-Migration [name of migration].
    // For the migration to take effect, use the command: Update-Database.
    // In case you had multiple DbContexts configured, you need to specify which DbContext you
    // want to migrate, for instance: Add-Migration Init -Context HrDbContext, and
    // Update-Database -Context HrDbContext.
    // Migrations are like Git Commits, you need to migrate for every change in the schema.
    // EFMigrationsHistory table in the SQL Server, keeps track of changes meaning after adding
    // a new migration, it would not execute the old migrations, only the newly added ones.

    // To remove a migration that was not published yet, NEVER delete the migration file under Migrations folder itself.
    // The reason is that the model snapshot under the Migrations folder will not be modified
    // if removed the migration file manually by deleting it.

    // If the migration was published to the database, one way to undo the changes is to get the 
    // wanted migration file (before the migration file you want to undo) and get its name.
    // Then in the package manager console, type: Update-Database [Migration File Name]
    // Like: Update-Database Add_Employee_Id_FK_To_Employees_Table
    // Finally, to remove the unwated migration file, use the command: Remove-Migration

    // IMPORTANT: if you want to remove two or more migration files back, you need to remove them
    // one-by-one because migration files rely on each other's changes
    public class HrDbContext : DbContext
    {
        // DbContextOptions is a setup for two things:
        // 1. Type of the database.
        // 2. Connection to the database.
        public HrDbContext(DbContextOptions<HrDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Lookup>()
                .HasData
                (
                    // Employee Positions (Major Code = 0)
                    new Lookup { Id = -1, MajorCode = 0, MinorCode = 0, Name = "Employee Positions" },
                    new Lookup { Id = -2, MajorCode = 0, MinorCode = 1, Name = "HR" },
                    new Lookup { Id = -3, MajorCode = 0, MinorCode = 2, Name = "Manager" },
                    new Lookup { Id = -4, MajorCode = 0, MinorCode = 3, Name = "Developer" },
                                      
                    // Department Types (Major Code = 1)
                    new Lookup { Id = -5, MajorCode = 1, MinorCode = 0, Name = "Department Types" },
                    new Lookup { Id = -6, MajorCode = 1, MinorCode = 1, Name = "Finance" },
                    new Lookup { Id = -7, MajorCode = 1, MinorCode = 2, Name = "Adminstrative" },
                    new Lookup { Id = -8, MajorCode = 1, MinorCode = 3, Name = "Technical" },

                    new Lookup { Id = -9, MajorCode = 2, MinorCode = 0, Name = "Vacation Types" },
                    new Lookup { Id = -10, MajorCode = 2, MinorCode = 1, Name = "Annual Vacation" },
                    new Lookup { Id = -11, MajorCode = 2, MinorCode = 2, Name = "Sick Vacation" },
                    new Lookup { Id = -12, MajorCode = 2, MinorCode = 3, Name = "Unpaid Vacation" }
                );

            // An Admin user seed to access and test the APIs
            modelBuilder
                .Entity<User>()
                .HasData
                (
                    // We cannot use the BCrypt hashing method every time we need to seed, because it will change its value and EF does not allow that
                    // In order to fix this, we hash the password once, then copy its hashed password string and use it for seeding everytime
                    new User { Id = 1, UserName = "Admin", HashedPassword = "$2a$11$MVRXCTLgV2dEBLH931VhPOUHtGqqfZ.006p2emcvtxwyRAT90ngym", IsAdmin = true }
                );

            // Seeding Departments data
            modelBuilder
                .Entity<Department>()
                .HasData
                (
                    new Department { Id = 1, Name = "IT", Description = "IT Department", FloorNumber = 1, TypeId = -8 }, 
                    new Department { Id = 2, Name = "HR", Description = "HR Department", FloorNumber = 2, TypeId = -7 }
                );

            // Seeding Employees data
            modelBuilder
                .Entity<Employee>()
                .HasData
                (
                    new Employee { Id = 1, Name = "manager", IsActive = true, StartDate = new DateTime(2025, 07, 01), DepartmentId = 1, PositionId = -3 },
                    new Employee { Id = 2, Name = "employee", IsActive = true, StartDate = new DateTime(2025, 07, 15), DepartmentId = 1, PositionId = -4 }
                );

            modelBuilder
                .Entity<User>()
                .HasIndex(x => x.UserName)
                .IsUnique();

            modelBuilder
                .Entity<Employee>()
                .HasIndex(x => x.UserId)
                .IsUnique();
        }        

        //----------------------------------------------------------------------
        // The following is the section where we define the tables represented
        // inside the HR database.

        // Employees Table
        // DbSet - a list of records of type T from the database
        // Any Model MUST match 100% the same structure as the one in the table (if database-first approach)
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
    }
}
