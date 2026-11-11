namespace GestionFacultadesCarrerasIdentity.Models
{
    public class Carrera
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int DuracionSemestres { get; set; }
    }
}
