using Microsoft.EntityFrameworkCore;

public class GestionFacultadesCarrerasIdentityContext(DbContextOptions<GestionFacultadesCarrerasIdentityContext> options) : DbContext(options)
{
    public DbSet<GestionFacultadesCarrerasIdentity.Models.Carrera> Carrera { get; set; } = default!;
}
