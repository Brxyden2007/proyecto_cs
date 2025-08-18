using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using proyecto_cs.src.modules.calidades_altitudes.domain.models;

namespace proyecto_cs;

public class CalidadAltitudConfig : IEntityTypeConfiguration<CalidadAltitud>
{
    public void Configure(EntityTypeBuilder<CalidadAltitud> builder)
    {
        // definir el nombre de la tabla
        builder.ToTable("calidades_altitudes");

        // define la llave principal
        builder.HasKey(ca => ca.IdCalidadAltitud);
        // define que es autoincrement
        builder.Property(ca => ca.IdCalidadAltitud)
            .HasColumnName("id_calidad")
            .ValueGeneratedOnAdd();

        builder.Property(ca => ca.Nivel)
            .IsRequired()
            .HasMaxLength(100);
    }
}