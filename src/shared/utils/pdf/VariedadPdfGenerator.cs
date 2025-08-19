using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.variedades.domain.models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace proyecto_cs.src.shared.utils.pdf;
public class VariedadPdfGenerator
{
  public VariedadPdfGenerator() {}

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
      .FirstOrDefault(v => v.IdVariedad == idVariedad);

    if (variedad == null)
      throw new Exception($"No se encontró la variedad con ID {idVariedad}");

    QuestPDF.Settings.License = LicenseType.Community;

    Document.Create(container =>
    {
      container.Page(page =>
      {
        // === Configuración base ===
        page.Size(PageSizes.A4);
        page.Margin(30);
        page.DefaultTextStyle(x => x.FontSize(12));
        page.PageColor("#F1f1f1");

        // ===== ENCABEZADO =====
        page.Header().Column(col =>
        {
          col.Item().Background(Colors.Green.Medium).Padding(15).Row(row =>
          {
            row.RelativeItem().Text($"Variedad - {variedad.NombreComun}")
              .FontSize(22).Bold().FontColor(Colors.White);

            row.ConstantItem(60).Height(60).Background(Colors.White).AlignCenter().AlignMiddle()
              .Text("PDF").FontColor(Colors.Green.Medium).Bold();
          });

          // Espacio inferior para que no se pegue al contenido
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

          // ===== INFORMACIÓN GENERAL =====
          col.Item().PaddingBottom(5)
            .Text("Información General")
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
              header.Cell().Element(CellStyleHeader).Text("Campo");
              header.Cell().Element(CellStyleHeader).Text("Valor");
            });

            table.Cell().Element(CellStyle).Text("ID");
            table.Cell().Element(CellStyle).Text(variedad.IdVariedad.ToString());

            table.Cell().Element(CellStyle).Text("Nombre Común");
            table.Cell().Element(CellStyle).Text(variedad.NombreComun);

            table.Cell().Element(CellStyle).Text("Nombre Científico");
            table.Cell().Element(CellStyle).Text(variedad.NombreCientifico ?? "-");

            table.Cell().Element(CellStyle).Text("Descripción");
            table.Cell().Element(CellStyle).Text(variedad.Descripcion ?? "-");

          });

          col.Spacing(20);

          // ===== CARACTERÍSTICAS AGRONÓMICAS =====
          col.Item().PaddingBottom(5)
            .Text("Características de la variedad")
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

            if (variedad.Rendimiento != null)
            {
              table.Cell().Element(CellStyle).Text("Rendimiento");
              table.Cell().Element(CellStyle).Text(variedad.Rendimiento.Nivel);
            }

            if (variedad.TamanioGrano != null)
            {
              table.Cell().Element(CellStyle).Text("Tamaño de grano");
              table.Cell().Element(CellStyle).Text(variedad.TamanioGrano.Nombre);
            }

            if (variedad.Porte != null)
            {
              table.Cell().Element(CellStyle).Text("Porte");
              table.Cell().Element(CellStyle).Text(variedad.Porte.Nombre);
            }

            if (variedad.Altitud != null)
            {
              table.Cell().Element(CellStyle).Text("Altitud");
              table.Cell().Element(CellStyle).Text(variedad.Altitud.Rango);
            }

            if (variedad.CalidadAltitud != null)
            {
              table.Cell().Element(CellStyle).Text("Calidad por altitud");
              table.Cell().Element(CellStyle).Text(variedad.CalidadAltitud.Nivel);
            }
          });
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
