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
        builder.HasKey(r => r.IdRendimiento);
        // define que es autoincrement
        builder.Property(r => r.IdRendimiento)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Nivel)
            .IsRequired()
            .HasMaxLength(100);
    }
}
