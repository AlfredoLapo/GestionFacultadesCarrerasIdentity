using System.ComponentModel.DataAnnotations;

namespace GestionFacultadesCarrerasIdentity.Models
{
    public class Carrera
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nombre de la Carrera")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código de la carrera es obligatorio.")]
        [StringLength(10, ErrorMessage = "El campo {0} no puede superar los {1} caracteres.")]
        [Display(Name = "Código")]
        public string Codigo { get; set; } = string.Empty;
        /// <summary>
        /// Duración de la carrera validada en un rango de 1 a 14 semestres.
        /// </summary>
        [Required(ErrorMessage = "La duración en semestres es obligatoria.")]
        [Range(1, 14, ErrorMessage = "La duración debe estar entre {1} y {2} semestres.")]
        [Display(Name = "Duración (Semestres)")]
        public int? DuracionSemestres { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una facultad.")]
        [Display(Name = "Facultad")]
        public int FacultadId { get; set; }

        public Facultad? Facultad { get; set; }
    }
}
