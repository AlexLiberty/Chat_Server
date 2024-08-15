using Microsoft.AspNetCore.SignalR;

namespace Server.DataBase
{
    public class ChatHub : Hub
    {
        private readonly ChatContext _context;

        public ChatHub(ChatContext context)
        {
            _context = context;
        }

        public async Task Send(string username, string message)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == username);
            if (user == null)
            {
                return;
            }

            var chatMessage = new Chat
            {
                Message = message,
                Timestamp = DateTime.Now.ToString("f"),
                UserId = user.Id
            };

            _context.Chats.Add(chatMessage);
            await _context.SaveChangesAsync();

            var chatHistory = new ChatHistory
            {
                UserName = username,
                Message = message,
                Timestamp = DateTime.Now.ToString("f")
            };

            _context.ChatHistories.Add(chatHistory);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("AddMessage", username, message);
        }

        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!_context.Users.Any(x => x.ConnectionId == id))
            {
                var newUser = new User { ConnectionId = id, Name = userName };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                await Clients.Caller.SendAsync("Connected", id, userName, _context.Users.ToList());
                await Clients.AllExcept(id).SendAsync("NewUserConnected", id, userName);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var item = _context.Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                _context.Users.Remove(item);
                await _context.SaveChangesAsync();
                var id = Context.ConnectionId;
                await Clients.All.SendAsync("UserDisconnected", id, item.Name);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
