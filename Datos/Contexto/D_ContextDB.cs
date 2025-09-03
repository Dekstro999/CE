using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;

namespace Datos.Contexto
{
    public class D_ContextDB(DbContextOptions<D_ContextDB> options) : DbContext(options)
    {
        //DbSets for your entities
        public DbSet<E_Carrera> Carreras { get; set; }
        public DbSet<E_PlanDeEstudios> PlanDeEstudios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configurations if needed
            modelBuilder.ApplyConfiguration(new CarreraConfig());
            modelBuilder.ApplyConfiguration(new PlanDeEstudiosConfig());
        }
    }
}