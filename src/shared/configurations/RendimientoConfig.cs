using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace proyecto_cs;
public class RendimientoConfig : IEntityTypeConfiguration<Rendimiento>
{
    public void Configure(EntityTypeBuilder<Rendimiento> builder)
    {
        // definir el nombre de la tabla
        builder.ToTable("rendimientos");

        // define la llave principal
        builder.HasKey(ren => ren.IdRendimiento);
        // define que es autoincrement
        builder.Property(ren => ren.IdRendimiento)
            .ValueGeneratedOnAdd();

        builder.Property(ren => ren.Nivel)
            .IsRequired()
            .HasMaxLength(100);
    }
}
