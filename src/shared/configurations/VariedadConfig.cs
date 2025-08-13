using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace proyecto_cs;
public class VariedadConfig : IEntityTypeConfiguration<Variedad>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Variedad> builder)
    { 
        builder.ToTable("variedades");

        builder.HasKey(v => v.IdVariedad);

        builder.Property(v => v.IdVariedad)
            .ValueGeneratedOnAdd();

        builder.Property(v => v.NombreComun)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.NombreCientifico)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(v => v.Descripcion)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(v => v.ImagenUrl)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(v => v.IdPorte)
            .IsRequired();

        builder.Property(v => v.IdTamanio)
            .IsRequired();

        builder.Property(v => v.IdAltitud)
            .IsRequired();

        builder.Property(v => v.IdRendimiento)
            .IsRequired();

        builder.Property(v => v.IdCalidad)
            .IsRequired();

        builder.HasOne(v => v.Porte)
            .WithMany(p => p.Variedades)
            .HasForeignKey(v => v.IdPorte);

        builder.HasOne(v => v.TamanioGrano)
            .WithMany(t => t.Variedades)
            .HasForeignKey(v => v.IdTamanio);

        builder.HasOne(v => v.Altitud)
            .WithMany(a => a.Variedades)
            .HasForeignKey(v => v.IdAltitud);

        builder.HasOne(v => v.Rendimiento)
            .WithMany(r => r.Variedades)
            .HasForeignKey(v => v.IdRendimiento);

        builder.HasOne(v => v.CalidadAltitud)
            .WithMany(c => c.Variedades)
            .HasForeignKey(v => v.IdCalidad);

        builder.HasMany(v => v.HistoriasGeneticas)
            .WithOne(hg => hg.Variedad)
            .HasForeignKey(v => v.IdVariedad);

        builder.HasMany(v => v.AtributosAgronomicos)
            .WithOne(a => a.Variedad)
            .HasForeignKey(v => v.IdVariedad);

        builder.HasMany(v => v.VariedadResistencias)
            .WithOne(vr => vr.Variedad)
            .HasForeignKey(v => v.IdVariedad);   
    }
}
