using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Entidades.Modelos.PlanesDeEstudio
{
    public class E_PlanEstudio
    {
        [Key]
        public int IdPlanEstudio { get; set; }

        [Required]
        public string NombrePlan { get; set; }

        public int IdCarrera { get; set; }

        [ForeignKey(nameof(IdCarrera))]
        public E_Carrera Carrera { get; set; }
    }
}
