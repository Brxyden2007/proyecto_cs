using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using proyecto_cs.src.modules.portes.domain.models;

namespace proyecto_cs;
public class PorteConfig : IEntityTypeConfiguration<Porte>
{
    public void Configure(EntityTypeBuilder<Porte> builder)
    {
        builder.ToTable("portes");

        builder.HasKey(po => po.IdPorte);
        builder.Property(po => po.IdPorte)
            .HasColumnName("id_porte")
            .ValueGeneratedOnAdd();

        builder.Property(po => po.Nombre)
            .IsRequired()
            .HasMaxLength(50);
    }
}
