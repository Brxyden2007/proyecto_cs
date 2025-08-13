using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace proyecto_cs;
public class TamanioGranoConfig
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TamanioGrano> builder)
    { 
        builder.ToTable("tamanios_grano");

        builder.HasKey(t => t.IdTamanio);
        builder.Property(t => t.IdTamanio)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Nombre)
            .IsRequired()
            .HasMaxLength(50);
    }
}
