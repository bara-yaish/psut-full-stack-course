namespace HR.Models
{
    // Lookup tables are mainly used to set predefined values for drop-down list type fields like Positions and Types
    // to prevent the user from setting values from his own which makes querying those values difficult.

    // Readonly: Lookup tables are not allowed to be modified, only used to fetch data and not add/update/delete data.
    // Uses seeding to insert predefined values every time the database is created.

    // This allows consistency between records.
    // For example:
    // Employees Position is Manager => MajorCode = 0, MinorCode = 3
    // Departments Type is Technical => MajorCode = 1, MinorCode = 1

    public class Lookup
    {
        // The reference to that specific option value in a specific table
        public long Id { get; set; }
        // 0-based index
        // Parent
        public int MajorCode { get; set; } // Maps the table (e.g., Employees Table = 0, Departments Table = 1)
        // 1-based index
        // Child
        public int MinorCode { get; set; } // Maps the field options (e.g., Positions => Developer = 1, HR = 2, Manager = 3)
        public string Name { get; set; }
    }
}
