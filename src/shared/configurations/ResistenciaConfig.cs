using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace proyecto_cs;
public class ResistenciaConfig: IEntityTypeConfiguration<Resistencia>
{
    public void Configure(EntityTypeBuilder<Resistencia> builder)
    {
        // definir el nombre de la tabla
        builder.ToTable("rendimientos");

        // define la llave principal
        builder.HasKey(r => r.IdResistencia);
        // define que es autoincrement
        builder.Property(r => r.IdResistencia)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Enfermedad)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(r => r.Nivel)
            .IsRequired()
            .HasMaxLength(50);
    }
}
