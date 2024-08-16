namespace ValVenisBE.Models
{
    public class QuotePage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? QuotePageHeader { get; set; }
        public string? QuotePageIntro { get; set; }
        public User? User { get; set; }
    }
}
