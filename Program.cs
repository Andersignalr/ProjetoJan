using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProjetoJan.Data;
using ProjetoJan.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.MapPost("/auth/login", async (
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> SignInManager,
    LoginDto login) =>
{
    var user = await userManager.FindByNameAsync(login.UserName);
    if (user == null)
        return Results.Unauthorized();

    var result = await SignInManager.PasswordSignInAsync(
        user, login.Password, true, false);

    return result.Succeeded ? Results.Ok() : Results.Unauthorized();
});

app.Run();
