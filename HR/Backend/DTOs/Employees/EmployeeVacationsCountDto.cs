using HR.Models;

namespace HR.DTOs.Employees
{
    public class EmployeeVacationsCountDto
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; internal set; }
        public int VacationsCount { get; set; }
    }
}
