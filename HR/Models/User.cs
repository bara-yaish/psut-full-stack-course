namespace HR.Models
{
    // Every Users table needs an Admin user seed
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public bool IsAdmin { get; set; }
    }
}
