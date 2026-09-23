// Este archivo es el punto de entrada de la aplicación web: registra los servicios (MVC, almacenamiento de imágenes, autenticación por cookies) y configura el pipeline y las rutas.
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using ESFE.GestionProductos.LN;
using ESFE.InventarioProd.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

// Galería de imágenes de Producto (Sprint A)
builder.Services.AddScoped<IImagenStorage, ImagenStorage>();
builder.Services.AddScoped<ImagenProductoLN>();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 6 * 1024 * 1024;  // 5MB archivo + overhead
});

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

// Sirve archivos físicos en runtime (ej. wwwroot/uploads/, generados después del build) —
// MapStaticAssets() de abajo resuelve por manifest de compilación y no los ve.
app.UseStaticFiles();

// Habilitar el sistema de rutas
app.UseRouting();

// Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Ruta principal: HomeController.Index() redirige a Dashboard (autenticado) o Login (anónimo)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();