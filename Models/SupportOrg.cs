namespace ValVenisBE.Models
{
    public class SupportOrg
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? SupportOrgName { get; set; }
        public string? SupportOrgSummary { get; set; }
        public string? SupportOrgUrl { get; set; }
        public string? SupportOrgPhone { get; set; }
        public string? SupportOrgLogo { get; set; }
        public User? User { get; set; }
    }
}
