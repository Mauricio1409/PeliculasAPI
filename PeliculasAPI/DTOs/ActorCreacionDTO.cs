using System.ComponentModel.DataAnnotations;
using PeliculasAPI.Validaciones;

namespace PeliculasAPI.DTOs
{
    public class ActorCreacionDTO
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [PesoArchivoValidacion(pesoMaximoMegabytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo:GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
