namespace Server.DataBase
{
    public class Chat
    {
        public int Id { get; set; }
        public string? Message {  get; set; }
        public string Timestamp { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Chat()
        {
            Timestamp = DateTime.Now.ToString("f");
        }
    }
}

