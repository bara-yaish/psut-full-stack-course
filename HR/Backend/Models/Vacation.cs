using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Vacation
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }
        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey("Lookup")]
        public long TypeId { get; set; }
        public Lookup Lookup { get; set; }
    }
}
