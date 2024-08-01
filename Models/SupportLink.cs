namespace ValVenisBE.Models
{
    public class SupportLink
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? SupportOrgName { get; set; }
        public string? SupportSummary { get; set; }
        public string? SupportLinkUrl { get; set; }
        public string? SupportPhone { get; set; }
        public string? SupportOrgLogo { get; set; }
    }
}
