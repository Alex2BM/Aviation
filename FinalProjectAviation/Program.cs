using FinalProjectAviation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using FinalProjectAviation.Repositories;
using FinalProjectAviation.Configuration;
using FinalProjectAviation.Services;


namespace FinalProjectAviation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

             builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AviationDBDbContext>(options => options.UseSqlServer(connString));
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddRepositories();
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/User/Login";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

            var app = builder.Build();

           
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
