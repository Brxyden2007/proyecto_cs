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
        builder.ToTable("resistencias");

        // define la llave principal
        builder.HasKey(res => res.IdResistencia);
        // define que es autoincrement
        builder.Property(res => res.IdResistencia)
            .ValueGeneratedOnAdd();

        builder.Property(res => res.Enfermedad)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(res => res.Nivel)
            .IsRequired()
            .HasMaxLength(50);
    }
}
