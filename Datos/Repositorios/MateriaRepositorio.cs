using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class CarreraRepositorio(D_ContextDB contextoBD) : ICarreraRepositorios
    {
        private readonly D_ContextDB _contextoBD = contextoBD;

        public async Task<ResultadoAcciones> InsertarCarrera(E_Carrera carrera)
        {
            ResultadoAcciones validacion = await ValidaCarrera(carrera);

            try
            {
                if (validacion.Resultado)
                {
                    await _contextoBD.Carreras.AddAsync(carrera);
                    await _contextoBD.SaveChangesAsync();
                }
                return validacion;
            }
            catch (Exception)
            {
                validacion.Mensaje = "Ocurrion un error inesperado";
                validacion.Resultado = false;
                return validacion;
            }

        }

        public async Task BorrarCarrera(int idCarrera)
        {
            var carreraBD await _contextoBD.Carreras.FindAsync(idCarrera);
            if (carreraBD == null)
            {
                throw new KeyNotFoundException($"Carrera con ID {idCarrera} no encontrada");
            }

            _contextoBD.Remove(carreraBD);
            await _contextoBD.SaveChangesAsync();
        }

        public async Task<ResultadosAcciones> ModificarCarrera(E_Carrera carrera)
        {
            ResultadoAcciones validacion = await ValidaCarrera(carrera);
            try
            {
                if (validacion.Resultado)
                {
                    var existente = await BuscarCarrera(carrera.IdCarrera);
                    if (existente == null)
                    {
                        validacion.Mensajes = $" - La carrera con ID {carrera.IdCarrera} no encontrada";
                        validacion.Resultado = false;
                        return validacion;
                    }
                    _contextoBD.Carreras.Update(carrera);
                    await _contextoBD.SaveChangesAsync();
                }
                return validacion;
            }
            catch (Exception)
            {
                validacion.Mensajes = "Occurio un error inesperado...";
                validacion.Resultado = false;
                return validacion;
            }
        }

        public async Task<IEnumerable<E_Carrera>> ListarCarreras()
        {
            return await _contextoBD.Carreras.ToListAsync();
        }

        public async Task<IEnumerable<E_Carrera>> ListarCarreras(string criterioBusqueda = null)
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

        public async Task<E_Carrera> BuscarCarrera(int idCarrera)
        {
            return await _contextoBD.Carreras.FindAsync(idCarrera);
        }

        // Metodos par validaciones
        public async Task<ResultadoAcciones> ValidaCarreras(E_Carrera carrera)
        {
            ResultadoAcciones resultado = new ResultadoAcciones();

            #region Validacion de ClaveCarrera
            if (string.IsNullOrWhiteSpace(carrera.ClaveCarrera))
            {
                resultado.Mensajes += "- La clave de carrera es requerida.\n";
                resultado.Resultado = false
            }
            else
            {
                if (carrera.ClaveCarrera.Length > 3)
                {
                    resultado.Mensaje += "Laclave de la carrera no  puede exceder 3 digutos";
                    resultado.Resultado = false;
                }

                if (await ExisteClaveCarrera(carrera.ClaveCarrera))
                {
                    resultado.Mensajes += $"LA clase";
                    resultado.Resultado = false;
                }
            }
            #endregion


            //Lalo tomo la foto para ver que onde
        }

        //Falta el de ID de carrera

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
                return await _contextoBD.Carreras.AnyAsync(c => c.NombreCarrera == aliasCarrera);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}