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
