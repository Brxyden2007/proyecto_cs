using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using proyecto_cs.src.modules.historias_geneticas.domain.models;

namespace proyecto_cs;
public class HistoriaGeneticaConfig : IEntityTypeConfiguration<HistoriaGenetica>
{
    public void Configure(EntityTypeBuilder<HistoriaGenetica> builder)
    {
        // nombre de la tabla
        builder.ToTable("historias_geneticas");
        // establece el id autoincrement
        builder.HasKey(hg => hg.IdHistoria);
        builder.Property(hg => hg.IdHistoria).ValueGeneratedOnAdd();

        builder.Property(hg => hg.IdVariedad).IsRequired();
        builder.Property(hg => hg.Obtentor).IsRequired();
        builder.Property(hg => hg.Familia).IsRequired();
        builder.Property(hg => hg.Grupo).IsRequired();
        builder.Property(hg => hg.Descripcion).IsRequired();

        // relaciones
        builder.HasOne(hg => hg.Variedad)
            .WithMany(v => v.HistoriasGeneticas)
            .HasForeignKey(hg => hg.IdVariedad);
    }
}
