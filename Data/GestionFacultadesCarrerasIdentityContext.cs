using Microsoft.EntityFrameworkCore;

public class GestionFacultadesCarrerasIdentityContext(DbContextOptions<GestionFacultadesCarrerasIdentityContext> options) : DbContext(options)
{
    public DbSet<GestionFacultadesCarrerasIdentity.Models.Carrera> Carrera { get; set; } = default!;
    public DbSet<GestionFacultadesCarrerasIdentity.Models.Facultad> Facultad { get; set; } = default!;
}
