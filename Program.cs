using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using digitalguestbook.Data;
using digitalguestbook.Areas.Identity.Data;
using digitalguestbook.Services;
using DigitalGuestbook.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid.Extensions.DependencyInjection;


namespace DigitalGuestbook
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
         

            builder.Services.AddDbContext<digitalguestbookDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("digitalguestbookDBContextConnection")));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("guestbookAppointmentDBConnection")));

            builder.Services
                .AddDefaultIdentity<digitalguestbookUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<digitalguestbookDBContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Unterst�tzung von MVC hinzuf�gen

            builder.Services.AddRazorPages(); // Unterst�tzung von Razor Pages hinzuf�gen

            builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGridSettings"));

            builder.Services.AddSendGrid(options =>
            {
                options.ApiKey = builder.Configuration.GetSection("SendGridSettings").GetValue<string>("ApiKey");
            });
            
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
            
            /*

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string email = "root@root.com";
                string password = "root123";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            */

            app.Run();
        }
    }
}