namespace ValVenisBE.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public bool IsAdmin { get; set; }
        public string? Uid { get; set; }
        public ICollection<SupportOrg>? SupportOrg { get; set; }
        public ICollection<Quote>? Quote { get; set; }
        public MissionStatement? MissionStatement { get; set; }
        public Logo? Logo { get; set; }
        public AboutMe? AboutMe { get; set; }

    }
}
