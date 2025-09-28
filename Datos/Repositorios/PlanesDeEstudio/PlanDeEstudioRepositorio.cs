using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanEstudios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datos.Repositorios.PlanesDeEstudio
{
    public class PlanDeEstudioRepositorio : IPlanDeEstudioRepositorio
    {
        private readonly D_ContextDB _context;
        public PlanDeEstudioRepositorio(D_ContextDB context)
        {
            _context = context;
        }

        public async Task<IEnumerable<E_PlanEstudio>> GetAll()
        {
            return await _context.PlanDeEstudios.ToListAsync();
        }

        public async Task<ResultadoAcciones> InsertarPlanDeEstudio(E_PlanEstudio planDeEstudio)
        {
            await _context.PlanDeEstudios.AddAsync(planDeEstudio);
            await _context.SaveChangesAsync();
            return new ResultadoAcciones { Resultado = true };
        }

        public async Task<ResultadoAcciones> ModificarPlanDeEstudio(E_PlanEstudio planDeEstudio)
        {
            var existente = await _context.PlanDeEstudios.FindAsync(planDeEstudio.IdPlanEstudio);
            if (existente == null)
                return new ResultadoAcciones { Resultado = false, Mensajes = { "No encontrado" } };

            // Actualiza solo los campos editables
            existente.PlanEstudio = planDeEstudio.PlanEstudio;
            existente.FechaCreacion = planDeEstudio.FechaCreacion;
            existente.TotalCreditos = planDeEstudio.TotalCreditos;
            existente.CreditosOptativos = planDeEstudio.CreditosOptativos;
            existente.CreditosObligatorios = planDeEstudio.CreditosObligatorios;
            existente.PerfilDeIngreso = planDeEstudio.PerfilDeIngreso;
            existente.PerfelDeEgreso = planDeEstudio.PerfelDeEgreso;
            existente.CampoOcupacional = planDeEstudio.CampoOcupacional;
            existente.Comentarios = planDeEstudio.Comentarios;
            existente.EstadoPlanEstudio = planDeEstudio.EstadoPlanEstudio;
            existente.IdCarrera = planDeEstudio.IdCarrera;
            existente.IdNivelAcademico = planDeEstudio.IdNivelAcademico;

            await _context.SaveChangesAsync();
            return new ResultadoAcciones { Resultado = true };
        }

        public async Task<ResultadoAcciones> BorrarPlanDeEstudio(int idPlanEstudio)
        {
            var plan = await _context.PlanDeEstudios.FindAsync(idPlanEstudio);
            if (plan == null)
                return new ResultadoAcciones { Resultado = false, Mensajes = { "No encontrado" } };
            _context.PlanDeEstudios.Remove(plan);
            await _context.SaveChangesAsync();
            return new ResultadoAcciones { Resultado = true };
        }
    }
}
