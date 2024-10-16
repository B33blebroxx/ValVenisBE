namespace ValVenisBE.Models
{
    public class ExternalLink
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? LinkName { get; set; }
        public string? LinkUrl { get; set; }
        public User? User { get; set; }
    }
}
