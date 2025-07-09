namespace HR.DTOs.Employees
{
    public class FilterEmployeeDto
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
        public bool IsActive { get; set; }
    }
}
