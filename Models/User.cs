namespace ValVenisBE.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public bool IsAdmin { get; set; }
        public string? Uid { get; set; }
    }
}
