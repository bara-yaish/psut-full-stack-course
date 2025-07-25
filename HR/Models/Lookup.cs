namespace HR.Models
{
    // Lookup are like Enums
    // We use lookup to create predefined values like a dropdown list
    // Like Positions: Developer, Manager, Engineer
    // We cannot allow any other values than the defined here
    // In our case, we want to create predefined values for Employee Positions and Department Names
    public class Lookup
    {
        public long Id { get; set; }
        public int MajorCode { get; set; } // The code that identifies the table (i.e., Employees Table -> 0, Departments Table -> 1)
        public int MinorCode { get; set; } // The code that identifies the options from each table (i.e., Positions in Employees -> Developer = 0, Engineer = 1, Manager = 2)
        public string Name { get; set; }
    }
}
