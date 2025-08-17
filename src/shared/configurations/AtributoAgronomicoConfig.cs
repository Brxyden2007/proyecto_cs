using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using proyecto_cs.src.modules.atributos_agronomicos.domain.models;

namespace proyecto_cs;
public class AtributoAgronomicoConfig : IEntityTypeConfiguration<AtributoAgronomico>
{
    public void Configure(EntityTypeBuilder<AtributoAgronomico> builder)
    {
        builder.ToTable("atributos_agronomicos");

        builder.HasKey(ag => ag.IdAtributo);

        builder.Property(ag => ag.IdAtributo)
            .HasColumnName("id_atributo")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(ag => ag.IdVariedad)
            .HasColumnName("id_variedad")
            .IsRequired();

        builder.Property(ag => ag.TiempoCosecha)
            .HasColumnName("tiempo_cosecha")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ag => ag.Maduracion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ag => ag.Nutricion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ag => ag.DensidadSiembra)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(ag => ag.Variedad)
            .WithMany(v => v.AtributosAgronomicos)
            .HasForeignKey(ag => ag.IdVariedad);
    }
}
