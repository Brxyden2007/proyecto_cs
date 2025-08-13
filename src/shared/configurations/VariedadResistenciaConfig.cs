using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace proyecto_cs;
public class VariedadResistenciaConfig : IEntityTypeConfiguration<VariedadResistencia>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VariedadResistencia> builder)
    {
        builder.ToTable("variedad_resistencia");

        builder.HasKey(vr => new { vr.IdVariedad, vr.IdResistencia });

        builder.Property(vr => vr.IdVariedad)
            .IsRequired();

        builder.Property(vr => vr.IdResistencia)
            .IsRequired();

        builder.HasOne(vr => vr.Variedad)
            .WithMany(v => v.VariedadResistencias)
            .HasForeignKey(vr => vr.IdVariedad);

        builder.HasOne(vr => vr.Resistencia)
            .WithMany(r => r.VariedadResistencias)
            .HasForeignKey(vr => vr.IdResistencia);
    }
}
