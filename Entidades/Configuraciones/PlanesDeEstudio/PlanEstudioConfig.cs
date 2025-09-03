using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entidades.Modelos.PlanesDeEstudio;

namespace Entidades.Configuraciones.PlanesDeEstudio
{
    public class PlanEstudioConfig : IEntityTypeConfiguration<E_PlanEstudio>
    {
        public void Configure(EntityTypeBuilder<E_PlanEstudio> builder)
        {
            builder.ToTable("PlanesEstudio", "CEF");

            builder.HasKey(p => p.IdPlanEstudio);

            builder.Property(p => p.NombrePlan)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(p => p.Carrera)
                .WithMany(c => c.PlanEstudio)
                .HasForeignKey(p => p.IdCarrera);
        }
    }
}