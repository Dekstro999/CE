using Entidades.DTO;
using Entidades.Modelos;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Entidaodes.Generales;

// Fix for CS0246: Ensure the 'Entidades' namespace is referenced in your project.
// 1. Check that the 'Entidades' project/assembly is added as a reference to your current project.
//    - In Visual Studio, right-click your project > Add Reference... > Projects > select 'Entidades'.
// 2. If 'Entidades' is in a separate assembly, make sure it is built and available.
// 3. If 'Entidades' is a folder in your solution, ensure it is declared as a namespace in the relevant files.
// 4. If the namespace is named differently, update the using directives accordingly.

// No code changes are needed in this file if the namespace and reference are correct.
// If you need further help, please provide the structure of your solution and where 'Entidades' is defined.

namespace Datos.IRepositorios.PlanesDeEstudio
{
    internal interface ICarreraRepositorios
    {
        // Aqui lo que se esta utilizando es la entidad E_Carrera no el modelo de DTO
        public Task<ResultadoAcciones> InsertarCarrera(E_Carrera carrera);

        public Task BorrarCarrera(int idCarrera);

        public Task<ResultadoAcciones> ModificarCarrera(E_Carrera carrera);

        public Task<E_Carrera> BuscarCarrera(int idCarrera);

        public Task<IEnumerable<E_Carrera>> ListarCarreras();

        //Este es un metodo sobrecargado que permite buscar carreras por un criterio de 
        public Task<IEnumerable<E_Carrera>> ListarCarreras(string? criterioBusqueda = null);
    }
}