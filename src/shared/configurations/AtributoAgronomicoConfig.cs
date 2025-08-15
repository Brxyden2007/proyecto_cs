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

        builder.HasKey(a => a.IdAtributo);

        builder.Property(a => a.IdAtributo)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(a => a.IdVariedad)
            .IsRequired();

        builder.Property(a => a.TiempoCosecha)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Maduracion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Nutricion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.DensidadSiembra)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(a => a.Variedad)
            .WithMany(v => v.AtributosAgronomicos)
            .HasForeignKey(a => a.IdVariedad);
    }
}
