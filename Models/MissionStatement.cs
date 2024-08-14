namespace ValVenisBE.Models
{
    public class MissionStatement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? WelcomeMessage { get; set; }
        public string? MissionStatementText { get; set; }
        public string? MissionStatementAcronym { get; set; }
        public User? User { get; set; }
    }
}
