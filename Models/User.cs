namespace ValVenisBE.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
        public ICollection<SupportOrg>? SupportOrg { get; set; }
        public ICollection<Quote>? Quote { get; set; }
        public ICollection<ExternalLink>? ExternalLink { get; set; }
        public MissionStatement? MissionStatement { get; set; }
        public AboutMe? AboutMe { get; set; }
        public Logo? Logo { get; set; }


    }
}
