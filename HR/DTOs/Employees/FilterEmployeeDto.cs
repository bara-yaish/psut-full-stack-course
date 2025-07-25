namespace HR.DTOs.Employees
{
    public class FilterEmployeeDto
    {
        public string? Name { get; set; }
        public long? PositionId { get; set; }
        public bool IsActive { get; set; }
    }
}
