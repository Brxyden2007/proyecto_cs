using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs;

public class PersonaConfig : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("personas");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Apellido)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Edad)
            .IsRequired();

        builder.Property(p => p.Nacionalidad)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.DocumentoIdentidad)
            .HasColumnName("documento_identidad")
            .IsRequired();

        builder.Property(p => p.Genero)
            .IsRequired()
            .HasMaxLength(50);
    }
}
