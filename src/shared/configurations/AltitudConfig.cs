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

        builder.HasKey(al => al.IdAltitud);

        builder.Property(al => al.IdAltitud)
            .HasColumnName("id_altitud")
            .ValueGeneratedOnAdd();

        builder.Property(al => al.Rango)
            .IsRequired()
            .HasMaxLength(100);
    }
}
