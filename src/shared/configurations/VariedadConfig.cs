using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using proyecto_cs.src.modules.variedades.domain.models;

namespace proyecto_cs;
public class VariedadConfig : IEntityTypeConfiguration<Variedad>
{
    public void Configure(EntityTypeBuilder<Variedad> builder)
    { 
        builder.ToTable("variedades");

        builder.HasKey(v => v.IdVariedad);

        builder.Property(v => v.IdVariedad)
            .HasColumnName("id_variedad")
            .ValueGeneratedOnAdd();

        builder.Property(v => v.NombreComun)
            .HasColumnName("nombre_comun")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.NombreCientifico)
            .HasColumnName("nombre_cientifico")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(v => v.Descripcion)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(v => v.ImagenUrl)
            .HasColumnName("imagen_url")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(v => v.IdPorte)
            .HasColumnName("id_porte")
            .IsRequired();

        builder.Property(v => v.IdTamanio)
            .HasColumnName("id_tamanio")
            .IsRequired();

        builder.Property(v => v.IdAltitud)
            .HasColumnName("id_altitud")
            .IsRequired();

        builder.Property(v => v.IdRendimiento)
            .HasColumnName("IdRendimiento")
            .IsRequired();

        builder.Property(v => v.IdCalidad)
            .HasColumnName("id_calidad")
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
