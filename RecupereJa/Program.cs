using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RecupereJa.Repositorio;
using RecupereJa.Repository;
using RecupereJa.Services;

namespace RecupereJa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                                           .AddCookie(options =>
                                                           {
                                                               options.LoginPath = "/Usuario/Login";
                                                               options.LogoutPath = "/Usuario/Logout";
                                                           });


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configurar Entity Framework
            builder.Services.AddDbContext<ItemContext>(options => options.UseMySql
            (builder.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));


            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            builder.Services.AddScoped<IItemRepositorio, ItemRepositorio>();

            builder.Services.AddScoped<IItemService, ItemService>();

                
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
                pattern: "{controller=Item}/{action=Index}/{id?}");

            app.Run();
        }
    }
}