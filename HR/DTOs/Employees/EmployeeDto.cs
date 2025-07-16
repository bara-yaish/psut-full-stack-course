namespace HR.DTOs.Employees
{
    public class EmployeeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public long? ManagerId { get; set; }
        public string? ManagerName { get; set; }
        public long? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
