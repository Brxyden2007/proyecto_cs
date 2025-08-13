using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs;
public class AdministradorConfig : IEntityTypeConfiguration<Administrador>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Administrador> builder)
    {
        builder.ToTable("administradores");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.HasOne(a => a.Persona)
            .WithOne(p => p.Administrador)
            .HasForeignKey<Administrador>(a => a.Id);
    }
}
