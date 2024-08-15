using Microsoft.EntityFrameworkCore;

namespace Server.DataBase
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatHistory> ChatHistories { get; set; }
        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }
    }
}
