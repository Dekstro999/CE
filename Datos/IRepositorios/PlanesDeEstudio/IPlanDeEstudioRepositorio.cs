/*
    Aqui
*/

using Entidades.Generales;
using Entidades.Modelos.PlanEstudios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datos.IRepositorios.PlanesDeEstudio
{
    public interface IPlanDeEstudioRepositorio
    {
        Task<ResultadoAcciones> InsertarPlanDeEstudio(E_PlanEstudio planDeEstudio);
        Task<IEnumerable<E_PlanEstudio>> GetAll();
        Task<ResultadoAcciones> ModificarPlanDeEstudio(E_PlanEstudio planDeEstudio);
        Task<ResultadoAcciones> BorrarPlanDeEstudio(int idPlanEstudio);
    }
}