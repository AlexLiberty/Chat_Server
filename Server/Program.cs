using Microsoft.EntityFrameworkCore;
using Server.DataBase;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChatContext>(options => options.UseSqlServer(connection));


builder.Services.AddSignalR();

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

using (var creat = app.Services.CreateScope())
{
    var context = creat.ServiceProvider.GetRequiredService<ChatContext>();
    DatabaseInitializer.Initialize(context);
}

app.MapHub<ChatHub>("/chat");

app.Run();