namespace HR.DTOs.Employees
{
    public class SaveEmployeeDto
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Position { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
