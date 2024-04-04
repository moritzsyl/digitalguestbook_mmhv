using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using digitalguestbook.Data;
using digitalguestbook.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("digitalguestbookDBContextConnection") ?? throw new InvalidOperationException("Connection string 'digitalguestbookDBContextConnection' not found.");

builder.Services.AddDbContext<digitalguestbookDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<digitalguestbookUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<digitalguestbookDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews(); // Unterstützung von MVC hinzufügen

builder.Services.AddRazorPages(); // Unterstützung von Razor Pages hinzufügen

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Routen für Razor Pages hinzufügen

app.Run();
