
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Entidades.Configuraciones.PlanesDeEstudio
{
    internal class CareraConfig : IEntityTypeConfiguration<E_Carrera>
    {
        public void Configure(EntityTypeBuilder<E_Carrera> builder)
        {
            builder.ToTable("Carreras", "CEF");

            builder.HasKey(c => c.IdCarrera);
            builder.Property(c => c.ClaveCarrera).ValueGeneratedOnAdd();

            builder.Property(c => c.NombreCarrera)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.ClaveCarrera)
                .IsUnique()
                .HasAnnotation("Relational:Name", "Uk_ClaveCarrera");

            //builder.

        }

    }
}
