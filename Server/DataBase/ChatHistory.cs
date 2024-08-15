namespace Server.DataBase
{
    public class ChatHistory
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Message { get; set; }
        public string Timestamp { get; set; }
    }
}
