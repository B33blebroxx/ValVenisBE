namespace ValVenisBE.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? QuoteText { get; set; }
        public string? QuoteAuthor { get; set; }
    }
}
