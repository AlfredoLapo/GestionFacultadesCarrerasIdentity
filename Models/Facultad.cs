using System.ComponentModel.DataAnnotations;

namespace GestionFacultadesCarrerasIdentity.Models
{
    public class Facultad
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la facultad es necesario")]
        [StringLength(200)]
        public string Nombre { get; set; } = "";

        [StringLength(100)]
        public string? Decano { get; set; }

        public ICollection<Carrera>? Carreras { get; set; }
    }
}

