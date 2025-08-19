using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.variedades.domain.models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace proyecto_cs.src.shared.utils.pdf;
public class FichaTecnicaPdfGenerator
{
public FichaTecnicaPdfGenerator() {}
    // === Helpers de estilos ===
  private IContainer CellStyle(IContainer container) =>
    container.BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(5).Background(Colors.White);

  private IContainer CellStyleHeader(IContainer container) =>
    container.Background(Colors.Green.Lighten1).Padding(5).Border(1)
      .BorderColor(Colors.Green.Medium).DefaultTextStyle(x => x.Bold().FontColor(Colors.White));
  public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
  public Task Compose(AppDbContext context, int idVariedad)
  {
    var variedad = context.Variedades
        .Include(v => v.Rendimiento)
        .Include(v => v.TamanioGrano)
        .Include(v => v.Porte)
        .Include(v => v.Altitud)
        .Include(v => v.CalidadAltitud)
        .Include(v => v.AtributosAgronomicos)
        .Include(v => v.HistoriasGeneticas)
        .FirstOrDefault(v => v.IdVariedad == idVariedad);
    if (variedad == null)
        throw new Exception($"No se encontró la variedad con ID {idVariedad}");

    QuestPDF.Settings.License = LicenseType.Community;
    Document.Create(container =>
      {
        container.Page(page =>
        {
          // esta parte es para definir la parte del diseño del pdf
          page.Size(PageSizes.A4);
          // pixeles de magin como si fuera un css
          page.Margin(30);
          // definir fuente 
          page.DefaultTextStyle(x => x.FontSize(12));
          page.Background();
          // bacground de la pagina
          page.PageColor("#F1f1f1");

          // ===== ENCABEZADO =====
          page.Header().Column(col =>
          {
            col.Item().Background(Colors.Green.Medium).Padding(15).Row(row =>
            {
              row.RelativeItem().Text($"Ficha Técnica - {variedad.NombreComun}")
                .FontSize(22).Bold().FontColor(Colors.White);

              row.ConstantItem(60).Height(60).Background(Colors.White).AlignCenter().AlignMiddle()
                .Text("PDF").FontColor(Colors.Green.Medium).Bold();
            });
            // separador como "margin-bottom"
            col.Item().Height(20).Background("#F1f1f1"); 
          });
          
          // ===== CONTENIDO =====
          page.Content().Column(col =>
          {
            // Imagen
            col.Item().Border(1).Background(Colors.White).CornerRadius(15).Padding(10).Column(c =>
            {
              if (!string.IsNullOrWhiteSpace(variedad.ImagenUrl) && File.Exists(variedad.ImagenUrl))
              {
                var bytes = File.ReadAllBytes(variedad.ImagenUrl);
                c.Item().Image(bytes).FitWidth();
              }
              else
              {
                c.Item().Text("[Sin imagen disponible]").Italic().FontColor(Colors.Grey.Medium);
              }
            });

            col.Spacing(20);

            // ===== ATRIBUTOS AGRONÓMICOS =====
            if (variedad.AtributosAgronomicos.Any())
            {
              col.Item().PaddingBottom(5)
                .Text("Atributos Agronómicos")
                .FontSize(18).Bold().FontColor(Colors.Green.Darken2);
              col.Item().Table(table =>
              {
                table.ColumnsDefinition(columns =>
                {
                  columns.RelativeColumn();
                  columns.RelativeColumn();
                });

                // Cabecera
                table.Header(header =>
                {
                  header.Cell().Element(CellStyleHeader).Text("Parámetro");
                  header.Cell().Element(CellStyleHeader).Text("Valor");
                });

                foreach (var atr in variedad.AtributosAgronomicos)
                {
                  table.Cell().Element(CellStyle).Text("Tiempo de cosecha");
                  table.Cell().Element(CellStyle).Text(atr.TiempoCosecha);

                  table.Cell().Element(CellStyle).Text("Maduración");
                  table.Cell().Element(CellStyle).Text(atr.Maduracion);

                  table.Cell().Element(CellStyle).Text("Nutrición");
                  table.Cell().Element(CellStyle).Text(atr.Nutricion);

                  table.Cell().Element(CellStyle).Text("Densidad de siembra");
                  table.Cell().Element(CellStyle).Text(atr.DensidadSiembra);
                }
              });
            }

            col.Spacing(20);

            // ===== HISTORIAS GENÉTICAS =====
            if (variedad.HistoriasGeneticas.Any())
            {
              col.Item().PaddingBottom(5)
                .Text("Historia Genética")
                .FontSize(18).Bold().FontColor(Colors.Green.Darken2);
              col.Item().Table(table =>
              {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3); // Obtentor (más ancho)
                    columns.RelativeColumn(2); // Familia
                    columns.RelativeColumn(1); // Grupo
                    columns.RelativeColumn(4); // Descripción
                });

                // Cabecera
                table.Header(header =>
                {
                  header.Cell().Element(CellStyleHeader).Text("Obtentor");
                  header.Cell().Element(CellStyleHeader).Text("Familia");
                  header.Cell().Element(CellStyleHeader).Text("Grupo");
                  header.Cell().Element(CellStyleHeader).Text("Descripción");
                });

                foreach (var h in variedad.HistoriasGeneticas)
                {
                  table.Cell().Element(CellStyle).Text(h.Obtentor);
                  table.Cell().Element(CellStyle).Text(h.Familia);
                  table.Cell().Element(CellStyle).Text(h.Grupo);
                  table.Cell().Element(CellStyle).Text(h.Descripcion);
                }
              });
            }
          });
            // ===== PIE DE PÁGINA =====
          page.Footer().AlignCenter().Text($"Generado el {DateTime.Now:dd/MM/yyyy}")
            .FontSize(10).FontColor(Colors.Grey.Medium);
        });
      })
    .GeneratePdf($"Variedad_{variedad?.IdVariedad}.pdf");

    return Task.CompletedTask;
  }
}
