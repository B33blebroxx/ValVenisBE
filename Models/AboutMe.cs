namespace ValVenisBE.Models
{
    public class AboutMe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? AboutMeText { get; set; }
        public string? AboutMeImage { get; set; }
        public string? AboutMeProfileLink { get; set; }
    }
}
