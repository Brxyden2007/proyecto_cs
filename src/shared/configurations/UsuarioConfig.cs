using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace proyecto_cs;

public class UsuarioConfig : IEntityTypeConfiguration<Usuario> 
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(u => u.id);

        builder.Property(u => u.nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.password_hash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.created_at)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
