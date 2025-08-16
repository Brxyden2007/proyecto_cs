using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using proyecto_cs.src.modules.altitudes.domain.models;

namespace proyecto_cs;
public class AltitudConfig : IEntityTypeConfiguration<Altitud>
{
    public void Configure(EntityTypeBuilder<Altitud> builder)
    {
        builder.ToTable("altitudes");

        builder.HasKey(a => a.IdAltitud);
        builder.Property(a => a.IdAltitud)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Rango)
            .IsRequired()
            .HasMaxLength(100);
    }
}
