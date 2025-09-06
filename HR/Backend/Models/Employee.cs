using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Employee
    {
        [Key] // Primary Key (optional if the property is named "Id")
        public long Id { get; set; }
        [MaxLength(50)] // NVARCHAR(50)
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        [MaxLength(50)] // NVARCHAR(50)
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ImagePath { get; set; }


        // FOREIGN KEYS
        // It is like saying: DepartmentId FOREIGN KEY REFERENCES Departments(Id)
        // References the Department table with the "Id" field that you want to reference.
        // EF will automatically detect the Id in the referenced table and take that as a
        // reference even if the property name is not the same as the Id field in the Departments table.
        // Navigation properties can be used for JOIN statements
        // For Instance: employee.DepartmentRow.Name

        [ForeignKey("DepartmentRow")]
        public long? DepartmentId { get; set; } // The Foreign Key
        public Department? Department { get; set; } // Navigation Property: The Department Table Reference (name does not matter)
        [ForeignKey("Manager")]
        public long? ManagerId { get; set; }
        public Employee? Manager { get; set; } // Navigation Property
        [ForeignKey("Lookup")]
        public long? PositionId { get; set; } // Reference to the lookup table
        public Lookup? Lookup { get; set; }
        [ForeignKey("User")]
        public long? UserId { get; set; }
        public User? User { get; set; }
    }
}
