using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs;
public class AdministradorConfig : IEntityTypeConfiguration<Administrador>
{
    public void Configure(EntityTypeBuilder<Administrador> builder)
    {
        builder.ToTable("administradores");

        builder.HasKey(ad => ad.Id);

        builder.Property(ad => ad.Id)
        .HasColumnName("id_administradores") // nombre real en MySQL
        .ValueGeneratedOnAdd();

        builder.Property(ad => ad.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ad => ad.Apellido)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ad => ad.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ad => ad.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(ad => ad.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.HasOne(ad => ad.Persona)
            .WithOne(pe => pe.Administrador)
            .HasForeignKey<Administrador>(ad => ad.Id);
    }
}
