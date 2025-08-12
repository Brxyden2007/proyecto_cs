using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace proyecto_cs;

public class PersonaConfig : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("personas");

        builder.HasKey(p => p.id);
        builder.Property(p => p.id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.apellido)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.edad)
            .IsRequired();

        builder.Property(p => p.nacionalidad)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.documento_identidad)
            .IsRequired();

        builder.Property(p => p.genero)
            .IsRequired()
            .HasMaxLength(50);
    }
}
