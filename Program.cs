using ProjetoJan.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.MapHub<ChatHub>("/chat");

app.Run();
