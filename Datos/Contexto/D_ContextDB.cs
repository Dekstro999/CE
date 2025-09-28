using Entidades.Configuraciones.PlanesDeEstudio;
using Entidades.Modelos.PlanesDeEstudio;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;
using E_PlanEstudio = Entidades.Modelos.PlanEstudios.E_PlanEstudio;

namespace Datos.Contexto
{
    public class D_ContextDB(DbContextOptions<D_ContextDB> options) : DbContext(options)
    {
        //DbSets para las entidades
        public DbSet<E_Carrera> Carreras { get; set; }
        public DbSet<E_PlanEstudio> PlanDeEstudios { get; set; }
        public DbSet<Entidades.Modelos.PlanEstudios.E_PlanEstudio> PlanesEstudio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CarreraConfig());
            modelBuilder.ApplyConfiguration(new Entidades.Configuraciones.PlanesDeEstudio.PlanEstudioConfiguration());
        }
    }
}