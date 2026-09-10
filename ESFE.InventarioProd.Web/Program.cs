using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.Name = "STOCKEO.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

var app = builder.Build();

// Configuración para producción
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Redireccionar HTTP a HTTPS
app.UseHttpsRedirection();

// Permitir archivos de wwwroot
app.UseStaticFiles();

// Habilitar el sistema de rutas
app.UseRouting();

<<<<<<< HEAD
// Autorización
=======
app.UseAuthentication();
>>>>>>> 4b42be3e61357b0472cecc72e08dda7dabef4a4b
app.UseAuthorization();

// Ruta principal de la aplicación
// Al ejecutar, abrirá:
// UsuariosController -> Index()
// -> Views/Usuarios/Index.cshtml
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Index}/{id?}");

app.Run();