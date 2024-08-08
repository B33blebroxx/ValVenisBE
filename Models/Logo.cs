namespace ValVenisBE.Models
{
    public class Logo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? LogoImage { get; set; }
        public User? User { get; set; }
    }
}
