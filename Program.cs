
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GestionFacultadesCarrerasIdentity.Models;

// Alias para evitar conflictos entre clases ApplicationUser

using ApplicationUser = GestionFacultadesCarrerasIdentity.Data.ApplicationUser;


var builder = WebApplication.CreateBuilder(args);

// Configurar conexión con SQL Server
var connectionString = builder.Configuration.GetConnectionString(
    "GestionFacultadesCarrerasIdentityContext")
    ?? throw new InvalidOperationException(
        "Connection string not found.");

// Registrar el contexto de base de datos
builder.Services.AddDbContext<GestionFacultadesCarrerasIdentityContext>(
    options => options.UseSqlServer(connectionString));

// Configurar ASP.NET Core Identity

builder.Services.AddDefaultIdentity<ApplicationUser>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;

        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GestionFacultadesCarrerasIdentityContext>();


// Servicios MVC y páginas Razor de Identity
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var servicios = scope.ServiceProvider;

    var userManager =
        servicios.GetRequiredService<UserManager<ApplicationUser>>();

    var roleManager =
        servicios.GetRequiredService<RoleManager<IdentityRole>>();

    // Crear los roles si todavía no existen
    foreach (var nombreRol in new[] { "Administrador", "Usuario" })
    {
        if (!await roleManager.RoleExistsAsync(nombreRol))
        {
            var resultado = await roleManager.CreateAsync(
                new IdentityRole(nombreRol));

            if (!resultado.Succeeded)
                throw new Exception(string.Join("; ",
                    resultado.Errors.Select(e => e.Description)));
        }
    }

    // Asignar Administrador al usuario registrado
    var usuario = await userManager.FindByEmailAsync(
        "Usuario3@example.com");

    if (usuario == null)
        throw new Exception(
            "No se encontró Usuario3@example.com en la base de datos.");

    if (!await userManager.IsInRoleAsync(usuario, "Administrador"))
    {
        var resultado = await userManager.AddToRoleAsync(
            usuario, "Administrador");

        if (!resultado.Succeeded)
            throw new Exception(string.Join("; ",
                resultado.Errors.Select(e => e.Description)));
    }
}



// Configurar manejo de errores
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Configurar middleware
app.UseHttpsRedirection();
app.UseRouting();

// Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Archivos estáticos
app.MapStaticAssets();

// Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Habilitar páginas de Identity
app.MapRazorPages();

// Ejecutar aplicación
app.Run();
