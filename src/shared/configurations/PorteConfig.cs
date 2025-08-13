using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace proyecto_cs;
public class PorteConfig : IEntityTypeConfiguration<Porte>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Porte> builder)
    {
        builder.ToTable("portes");

        builder.HasKey(p => p.IdPorte);
        builder.Property(p => p.IdPorte)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Nombre)
            .IsRequired()
            .HasMaxLength(50);
    }
}
