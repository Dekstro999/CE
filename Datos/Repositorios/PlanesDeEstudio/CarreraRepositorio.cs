/*
    Aqui basicamente ira el CRUD de nuestra aplicacion el cual tambien contara con la validacion de EF Core
*/

using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositorios.PlanesDeEstudio
{
    public class CarreraRepositorio(D_ContextDB contextoBD) : ICarreraRepositorios
    {
        private readonly D_ContextDB _contextoBD = contextoBD;

        // Insertar
        public async Task<ResultadoAcciones> InsertarCarrera(E_Carrera carrera)
        {
            ResultadoAcciones validacion = await ValidarCarrera(carrera);

            try
            {
                if (validacion.Resultado)
                {
                    await _contextoBD.Carreras.AddAsync(carrera);
                    await _contextoBD.SaveChangesAsync();
                }
                return validacion;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string mensaje = "Ocurrio un error inesperado...";
                validacion.Mensajes.Add(mensaje);
                validacion.Resultado = false;
                return validacion;
            }
        }

        // Borrar
        public async Task<ResultadoAcciones> BorrarCarrera(int idCarrera)
        {
            var carreraBD = await _contextoBD.Carreras.FindAsync(idCarrera);
            if (carreraBD == null)
            {
                ResultadoAcciones resultado = new ResultadoAcciones();
                resultado.Resultado = false;
                resultado.Mensajes.Add("No se encontro la carrera");
                throw new KeyNotFoundException($"Carrera con ID {idCarrera} no encontrada.");
            }

            _contextoBD.Carreras.Remove(carreraBD);
            await _contextoBD.SaveChangesAsync();
            return new ResultadoAcciones();
        }

        // Modificar
        public async Task<ResultadoAcciones> ModificarCarrera(E_Carrera carrera)
        {
            ResultadoAcciones validacion = await ValidarCarrera(carrera);
            try
            {
                if (validacion.Resultado)
                {
                    var existente = await BuscarCarrera(carrera.IdCarrera);
                    if (existente == null)
                    {
                        validacion.Mensajes.Add($" - La carrera con ID {carrera.IdCarrera} no encontrada");
                        validacion.Resultado = false;
                        return validacion;
                    }
                    // Actualizar solo los campos editables
                    existente.ClaveCarrera = carrera.ClaveCarrera;
                    existente.NombreCarrera = carrera.NombreCarrera;
                    existente.AliasCarrera = carrera.AliasCarrera;
                    existente.IdCoordinador = carrera.IdCoordinador;
                    existente.EstadoCarrera = carrera.EstadoCarrera;
                    await _contextoBD.SaveChangesAsync();
                }
                return validacion;
            }
            catch (Exception)
            {
                string mensaje = "Ocurrio un error inesperado...";
                validacion.Mensajes.Add(mensaje);
                validacion.Resultado = false;
                return validacion;
            }
        }

        // Listar
        public async Task<IEnumerable<E_Carrera>> ListarCarreras()
        {
            return await _contextoBD.Carreras.ToListAsync();
        }

        // Listar con Criterio de Busqueda
        public async Task<IEnumerable<E_Carrera>> ListarCarreras(string? criterioBusqueda = null)
        {
            var query = _contextoBD.Carreras.AsQueryable();

            if (!string.IsNullOrWhiteSpace(criterioBusqueda))
            {
                query = query.Where(c =>
                    c.ClaveCarrera.Contains(criterioBusqueda) ||
                    c.NombreCarrera.Contains(criterioBusqueda) ||
                    c.AliasCarrera.Contains(criterioBusqueda)
                );
            }
            return await query.ToListAsync();
        }

        // Buscar
        public async Task<E_Carrera?> BuscarCarrera(int idCarrera)
        {
            return await _contextoBD.Carreras.FindAsync(idCarrera);
        }

        // Metodos par validaciones
        public async Task<ResultadoAcciones> ValidarCarrera(E_Carrera carrera)
        {
            ResultadoAcciones resultado = new ResultadoAcciones();

            #region
            if (string.IsNullOrEmpty(carrera.ClaveCarrera))
            {
                resultado.Mensajes.Add("La clave de la carrera es obligatoria");
                resultado.Resultado = false;
            }
            else
            {
                if (carrera.ClaveCarrera.Length > 3)
                {
                    resultado.Mensajes.Add("La clave de la carrera no debe exceder los 10 caracteres");
                    resultado.Resultado = false;
                }
                // Validación de unicidad excluyendo el registro actual
                if (await _contextoBD.Carreras.AnyAsync(c => c.ClaveCarrera == carrera.ClaveCarrera && c.IdCarrera != carrera.IdCarrera))
                {
                    resultado.Mensajes.Add("La clave de la carrera ya existe");
                    resultado.Resultado = false;
                }
            }
            #endregion

            #region Validacion Nombre
            if (string.IsNullOrWhiteSpace(carrera.NombreCarrera))
            {
                resultado.Mensajes.Add("El nombre de la carrera es obligatorio");
                resultado.Resultado = false;
            }
            else
            {
                if (carrera.NombreCarrera.Length > 100)
                {
                    resultado.Mensajes.Add("El nombre de la carrera no debe exceder los 50 caracteres");
                    resultado.Resultado = false;
                }
                if (await _contextoBD.Carreras.AnyAsync(c => c.NombreCarrera == carrera.NombreCarrera && c.IdCarrera != carrera.IdCarrera))
                {
                    resultado.Mensajes.Add("El nombre de la carrera ya existe");
                    resultado.Resultado = false;
                }
            }
            #endregion

            #region Validacion Alias
            if (string.IsNullOrWhiteSpace(carrera.AliasCarrera))
            {
                resultado.Mensajes.Add("El alias de la carrera es obligatorio");
                resultado.Resultado = false;
            }
            else
            {
                if (carrera.AliasCarrera.Length > 100)
                {
                    resultado.Mensajes.Add("El alias de la carrera no debe exceder los 50 caracteres");
                    resultado.Resultado = false;
                }
                if (await _contextoBD.Carreras.AnyAsync(c => c.AliasCarrera == carrera.AliasCarrera && c.IdCarrera != carrera.IdCarrera))
                {
                    resultado.Mensajes.Add("El alias de la carrera ya existe");
                    resultado.Resultado = false;
                }
            }
            #endregion

            return resultado;
        }

        //=====================================================================//

        public async Task<bool> ExisteClaveCarrera(string claveCarrera)
        {
            try
            {
                return await _contextoBD.Carreras.AnyAsync(c => c.ClaveCarrera == claveCarrera);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExisteNombreCarrera(string nombreCarrera)
        {
            try
            {
                return await _contextoBD.Carreras.AnyAsync(c => c.NombreCarrera == nombreCarrera);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExisteAliasCarrera(string aliasCarrera)
        {
            try
            {
                return await _contextoBD.Carreras.AnyAsync(c => c.AliasCarrera == aliasCarrera);
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Verificar que la  interfaz si esta realizada correctamente
        public Task<IEnumerable<E_Carrera>> ListarCarrerasPorPlanEstudio(int value)
        {
            throw new NotImplementedException();
        }
    }
}