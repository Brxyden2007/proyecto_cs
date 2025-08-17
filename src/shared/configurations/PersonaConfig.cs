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

        builder.HasKey(pe => pe.Id);
        builder.Property(pe => pe.Id)
            .ValueGeneratedOnAdd();

        builder.Property(pe => pe.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pe => pe.Apellido)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pe => pe.Edad)
            .IsRequired();

        builder.Property(pe => pe.Nacionalidad)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pe => pe.DocumentoIdentidad)
            .HasColumnName("documento_identidad")
            .IsRequired();

        builder.Property(pe => pe.Genero)
            .IsRequired()
            .HasMaxLength(50);
    }
}
