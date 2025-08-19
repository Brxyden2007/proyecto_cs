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
public class FichasTecnicasTodasPdfGenerator
{
  public FichasTecnicasTodasPdfGenerator() {}

  // === Helpers de estilos ===
  private IContainer CellStyle(IContainer container) =>
    container.BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(5).Background(Colors.White);

  private IContainer CellStyleHeader(IContainer container) =>
    container.Background(Colors.Green.Lighten1).Padding(5).Border(1)
      .BorderColor(Colors.Green.Medium).DefaultTextStyle(x => x.Bold().FontColor(Colors.White));

  public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

  public Task Compose(AppDbContext context)
  {
    var variedades = context.Variedades
      .Include(v => v.Rendimiento)
      .Include(v => v.TamanioGrano)
      .Include(v => v.Porte)
      .Include(v => v.Altitud)
      .Include(v => v.CalidadAltitud)
      .Include(v => v.AtributosAgronomicos)
      .Include(v => v.HistoriasGeneticas)
      .ToList();

    if (!variedades.Any())
      throw new Exception("No hay variedades registradas en la base de datos.");

    QuestPDF.Settings.License = LicenseType.Community;

    Document.Create(container =>
    {
      container.Page(page =>
      {
        // CONFIGURACIÓN GENERAL DE CADA PÁGINA
        page.Size(PageSizes.A4);
        page.Margin(30);
        page.DefaultTextStyle(x => x.FontSize(12));
        page.PageColor("#F1f1f1");

        page.Content().Column(col =>
        {
          foreach (var variedad in variedades)
          {
            col.Item().PageBreak(); // 📄 Salto de página para cada variedad

            // ===== ENCABEZADO =====
            col.Item().Background(Colors.Green.Medium).Padding(15).Row(row =>
            {
              row.RelativeItem().Text($"Ficha Técnica - {variedad.NombreComun}")
                .FontSize(22).Bold().FontColor(Colors.White);

              row.ConstantItem(60).Height(60).Background(Colors.White).AlignCenter().AlignMiddle()
                .Text("PDF").FontColor(Colors.Green.Medium).Bold();
            });

            col.Item().Height(20).Background("#F1f1f1"); // Separador

            // ===== IMAGEN =====
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
                  columns.RelativeColumn(3);
                  columns.RelativeColumn(2);
                  columns.RelativeColumn(1);
                  columns.RelativeColumn(4);
                });

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

            col.Item().PaddingTop(10).AlignCenter().Text($"Generado el {DateTime.Now:dd/MM/yyyy}")
              .FontSize(10).FontColor(Colors.Grey.Medium);
          }
        });
      });
    })
    .GeneratePdf("Todas_Fichas_Tecnicas.pdf");

    return Task.CompletedTask;
  }
}

