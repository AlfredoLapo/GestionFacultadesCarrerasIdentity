
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


// COMMIT 3: Crear roles y usuarios de prueba automáticamente
using (var scope = app.Services.CreateScope())
{
    var servicios = scope.ServiceProvider;

    var userManager =
        servicios.GetRequiredService<UserManager<ApplicationUser>>();

    var roleManager =
        servicios.GetRequiredService<RoleManager<IdentityRole>>();

    // Crear roles
    string[] roles = { "Coordinador", "Aspirante" };

    foreach (var rol in roles)
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            var resultado = await roleManager.CreateAsync(
                new IdentityRole(rol));

            if (!resultado.Succeeded)
                throw new Exception(string.Join("; ",
                    resultado.Errors.Select(e => e.Description)));
        }
    }

    // Usuarios de prueba
    var usuariosPrueba = new[]
    {
        new
        {
            Email = "coordinador@example.com",
            Rol = "Coordinador"
        },
        new
        {
            Email = "aspirante@example.com",
            Rol = "Aspirante"
        }
    };

    // Contraseña de prueba local; no subirla a GitHub
    var clavePrueba = builder.Configuration["SeedUsers:Password"];

    if (string.IsNullOrWhiteSpace(clavePrueba))
        throw new InvalidOperationException(
            "Configure SeedUsers:Password en User Secrets.");

    foreach (var datos in usuariosPrueba)
    {
        var usuario = await userManager.FindByEmailAsync(datos.Email);

        if (usuario == null)
        {
            usuario = new ApplicationUser
            {
                UserName = datos.Email,
                Email = datos.Email,
                EmailConfirmed = true
            };

            var resultado = await userManager.CreateAsync(
                usuario, clavePrueba);

            if (!resultado.Succeeded)
                throw new Exception(string.Join("; ",
                    resultado.Errors.Select(e => e.Description)));
        }

        if (!await userManager.IsInRoleAsync(usuario, datos.Rol))
        {
            var resultado = await userManager.AddToRoleAsync(
                usuario, datos.Rol);

            if (!resultado.Succeeded)
                throw new Exception(string.Join("; ",
                    resultado.Errors.Select(e => e.Description)));
        }
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
