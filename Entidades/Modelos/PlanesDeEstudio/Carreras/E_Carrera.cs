using System.ComponentModel.DataAnnotations;
using Entidades.Modelos.PlanesDeEstudio;

namespace Entidades.Modelos.PlanesDeEstudio.Carreras
{
    public class E_Carrera
    {
        [Key]
        public int IdCarrera { get; set; }
        public string ClaveCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public string AliasCarrera { get; set; }
        public int IdCordinador { get; set; }
        public bool EstadoCarrera { get; set; }

        // Propiedad de navegación
        public ICollection<E_PlanEstudio> PlanEstudio { get; set; }
    }
}
