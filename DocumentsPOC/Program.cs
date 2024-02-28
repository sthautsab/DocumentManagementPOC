using DocumentsPOC.Context;
using DocumentsPOC.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        //option.ExpireTimeSpan = TimeSpan.FromMinutes(20);

    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Set session timeout
                                                    // Other session options
});

//builder.Services.AddDbContext<DocumentDbContext>(options =>
//            options.UseSqlServer("Data Source=LAPTOP-RLAT42JM\\MSSQLSERVER05;  Initial Catalog=[DocumentDb]; Integrated Security=True;"));
builder.Services.AddDbContext<DocumentDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlConnectionString")), ServiceLifetime.Singleton);

builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IFolderRepository, FolderRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
