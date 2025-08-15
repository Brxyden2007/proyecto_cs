using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace proyecto_cs.src.shared.utils.pdf;

public class VariedadPdfGenerator
{
  
  private readonly Variedad _variedad;
  public VariedadPdfGenerator(Variedad variedad) =>_variedad = variedad;
  public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
  public Task Compose(AppDbContext context)
  {
    var variedad = context.Variedades
        .Include(v => v.Rendimiento)
        .Include(v => v.TamanioGrano)
        .Include(v => v.Porte)
        .Include(v => v.Altitud)
        .Include(v => v.CalidadAltitud)
        .FirstOrDefault(v => v.IdVariedad == 1);
        
    QuestPDF.Settings.License = LicenseType.Community;
    Document.Create(container =>
      {
        container.Page(page =>
        {
          // esta parte es para definir la parte del diseño del pdf
          page.Size(PageSizes.A4);
          // pixeles de magin como si fuera un css
          page.Margin(20);
          // definir fuente 
          page.DefaultTextStyle(x => x.FontSize(12));
          page.Background();
          // bacground de la pagina
          page.PageColor("#F1f1f1");

          page.Content().Row(row =>
          {
            // ======= LADO IZQUIERDO =======
            row.RelativeItem(1).Column(col =>
            {
              // Caja de encabezado con nombre, descripción e imagen
              col.Item().Border(1).Background(Colors.Green.Medium)
                .Padding(15)
                .Column(c =>
                {
                  c.Item().Text(_variedad.NombreComun)
                    .FontSize(28)
                    .Bold()
                    .FontColor(Colors.White);

                  c.Item().Text(_variedad.Descripcion)
                    .FontColor(Colors.White)
                    .FontSize(12);
                });

              col.Spacing(10);

              // Yield potential
              col.Item().Column(c =>
              {
                c.Item().Text("YIELD POTENTIAL").Bold().FontColor(Colors.Green.Medium);
                c.Item().Text($"{_variedad.Rendimiento?.Nivel}").FontSize(14);
              });

              col.Spacing(5);

              // Bean size
              col.Item().Column(c =>
              {
                c.Item().Text("BEAN SIZE").Bold().FontColor(Colors.Green.Medium);
                c.Item().Text($"{_variedad.TamanioGrano?.Nombre.Clone()}");
              });

              // Coffee leaf rust
              col.Item().Column(c =>
              {
                c.Item().Text("COFFEE LEAF RUST").Bold().FontColor(Colors.Green.Medium);
                c.Item().Text("Resistant");
                c.Item().Border(1).Background(Colors.Green.Medium).Height(10);
              });

              // Nematode
              col.Item().Column(c =>
              {
                c.Item().Text("NEMATODE").Bold().FontColor(Colors.Green.Medium);
                c.Item().Text("Resistant");
                c.Item().Border(1).Background(Colors.Green.Medium).Height(10);
              });

              // Coffee berry borer
              col.Item().Column(c =>
              {
                c.Item().Text("COFFEE BERRY BORER").Bold().FontColor(Colors.Green.Medium);
                c.Item().Text("Susceptible");
                c.Item().Border(1).Background(Colors.Grey.Medium).Height(10);
              });
            });

            // ======= LADO DERECHO =======
            row.RelativeItem(1.2f).Column(col =>
            {
              // Imagen principal
              // col.Item().Image(_variedad.ImagenUrl, ImageScaling.FitArea);
              col.Item().Image(Image.FromFile(_variedad.ImagenUrl));

              // Características
              col.Item().PaddingTop(10).Text("CHARACTERISTICS")
                .FontSize(18)
                .Bold()
                .FontColor(Colors.White)
                .BackgroundColor(Colors.Green.Medium);
              // .Padding(5);

              col.Item().Column(c =>
              {
                AddCharacteristic(c, "YIELD POTENTIAL", $"{_variedad.Rendimiento?.Nivel}");
                AddCharacteristic(c, "COUNTRY OF RELEASE", "Indonesia");
                AddCharacteristic(c, "CONTENTS OF MUCILAGE IN THE CHERRY", "Average");
                AddCharacteristic(c, "COFFEE BERRY DISEASE", "Tolerant");
                AddCharacteristic(c, "SHOOT HOLE BORER", "Susceptible");
              });

              // Agronomía
              col.Item().PaddingTop(10).Text("AGRONOMICS")
                .FontSize(18)
                .Bold()
                .FontColor(Colors.Green.Medium);

              col.Item().Table(table =>
              {
                table.ColumnsDefinition(columns =>
                {
                  columns.RelativeColumn();
                });

                table.Cell().Text("Stature");
                table.Cell().Text("Year of first production");
                table.Cell().Text("Nutrition requirement");
                table.Cell().Text("Ripening of fruit");
                table.Cell().Text("Cherry to green bean outrun");
              });
            });
          });
        });
      })
    .GeneratePdf($"Variedad_{_variedad.IdVariedad}.pdf");
    return Task.CompletedTask;
  }

  private void AddCharacteristic(ColumnDescriptor col, string title, string value)
  {
    col.Item().Row(r =>
    {
      r.RelativeItem().Text(title).Bold();
      r.RelativeItem().Text(value);
    });
  }
}



// container
//     .Background(Colors.Grey.Lighten2)
//     .CornerRadius(25)
//     .Padding(25)
//     .Text("Content with rounded corners");