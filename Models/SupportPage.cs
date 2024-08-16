namespace ValVenisBE.Models
{
    public class SupportPage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? SupportPageHeader { get; set; }
        public string? SupportPageIntro { get; set; }
        public User? User { get; set; }
    }
}
