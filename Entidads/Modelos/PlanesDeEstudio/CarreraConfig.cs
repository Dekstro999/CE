using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Entidades.Modelos.PlanesDeEstudio
{
    public class CarreraConfig : IEntityTypeConfiguration<E_Carrera>
    {
        public void Configure(EntityTypeBuilder<E_Carrera> builder)
        {
            builder.ToTable("Carreras", "CEF");

            builder.HasKey(c => c.IdCarrera);

            builder.Property(c => c.ClaveCarrera)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.Property(c => c.NombreCarrera)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.AliasCarrera)
                   .HasMaxLength(50);

            builder.HasIndex(c => c.ClaveCarrera)
                   .IsUnique()
                   .HasDatabaseName("UK_ClaveCarrera");
        }
    }
}
