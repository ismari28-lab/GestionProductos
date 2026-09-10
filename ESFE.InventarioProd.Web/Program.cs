var builder = WebApplication.CreateBuilder(args);

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

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

// Autorización
app.UseAuthorization();

// Ruta principal de la aplicación
// Al ejecutar, abrirá:
// UsuariosController -> Index()
// -> Views/Usuarios/Index.cshtml
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Index}/{id?}");

app.Run();