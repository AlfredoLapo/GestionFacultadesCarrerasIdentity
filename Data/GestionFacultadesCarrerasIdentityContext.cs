
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionFacultadesCarrerasIdentity.Models;
using ApplicationUser = GestionFacultadesCarrerasIdentity.Data.ApplicationUser;

public class GestionFacultadesCarrerasIdentityContext(
    DbContextOptions<GestionFacultadesCarrerasIdentityContext> options
) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Carrera> Carrera { get; set; } = default!;

    public DbSet<Facultad> Facultad { get; set; } = default!;
}
