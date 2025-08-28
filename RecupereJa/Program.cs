using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RecupereJa.Data;
using RecupereJa.Repository;
using RecupereJa.Repositorio;
using RecupereJa.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do DbContext com Pomelo MySQL
builder.Services.AddDbContext<RecupereJaContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 39)))
);

// Inje��es de depend�ncia
builder.Services.AddScoped<IItemRepositorio, ItemRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Usuario/Login";
        o.AccessDeniedPath = "/Usuario/AcessoNegado";
        o.SlidingExpiration = true;
        o.Cookie.HttpOnly = true;
        o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();