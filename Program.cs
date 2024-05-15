using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using digitalguestbook.Data;
using digitalguestbook.Areas.Identity.Data;
using digitalguestbook.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("digitalguestbookDBContextConnection") ?? throw new InvalidOperationException("Connection string 'digitalguestbookDBContextConnection' not found.");

builder.Services.AddDbContext<digitalguestbookDBContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("guestbookAppointmentDBConnection")));
builder.Services.AddDefaultIdentity<digitalguestbookUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<digitalguestbookDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews(); // Unterst�tzung von MVC hinzuf�gen

builder.Services.AddRazorPages(); // Unterst�tzung von Razor Pages hinzuf�gen

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

app.MapRazorPages(); // Routen f�r Razor Pages hinzuf�gen

app.Run();
