using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace proyecto_cs;
public class AltitudConfig : IEntityTypeConfiguration<Altitud>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Altitud> builder)
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
