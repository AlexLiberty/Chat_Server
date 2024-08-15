namespace Server.DataBase
{
    public class DatabaseInitializer
    {
        public static void Initialize(ChatContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
