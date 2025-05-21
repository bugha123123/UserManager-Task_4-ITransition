using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task_4.Interface;
using Task_4.Models;
using Task_4.Service;

var builder = WebApplication.CreateBuilder(args);

// 👇 Configure EF Core and Identity
builder.Services.AddDbContext<DbContextion>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TASK_4")));


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();



builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 1;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<DbContextion>()
.AddDefaultTokenProviders();


var app = builder.Build();

// 👇 HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapGet("/", async (HttpContext context) =>
{

    if (!context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/Auth/signin");
        return;
    }
    context.Response.Redirect("/Home/Index");

});


app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
