/*
    Aqui haremos las configuracion de E_PlanDeEstudio  con Fluent API y EF Core
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entidades.Modelos.PlanEstudios;

namespace Entidades.Configuraciones.PlanesDeEstudio
{
    public class PlanEstudioConfiguration : IEntityTypeConfiguration<E_PlanEstudio>
    {
        public void Configure(EntityTypeBuilder<E_PlanEstudio> builder)
        {
            builder.ToTable("PlanesEstudio", "CEF");
            builder.HasKey(pe => pe.IdPlanEstudio);
            builder.Property(pe => pe.IdPlanEstudio).ValueGeneratedOnAdd();
            builder.Property(pe => pe.PlanEstudio).IsRequired().HasMaxLength(100);
            builder.Property(pe => pe.FechaCreacion).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(pe => pe.TotalCreditos).IsRequired();
            builder.Property(pe => pe.CreditosOptativos).IsRequired();
            builder.Property(pe => pe.CreditosObligatorios).IsRequired();
            builder.Property(pe => pe.PerfilDeIngreso).IsRequired().HasMaxLength(2048);
            builder.Property(pe => pe.PerfelDeEgreso).HasMaxLength(2048);
            builder.Property(pe => pe.CampoOcupacional).HasMaxLength(2048);
            builder.Property(pe => pe.Comentarios).HasMaxLength(2048);
            builder.Property(pe => pe.EstadoPlanEstudio).IsRequired().HasDefaultValue(true);
            builder.Property(pe => pe.IdCarrera)
                   .IsRequired();
            builder.HasOne(pe => pe.Carrera)
                   .WithMany(c => c.PlanesEstudio)
                   .HasForeignKey(pe => pe.IdCarrera)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
