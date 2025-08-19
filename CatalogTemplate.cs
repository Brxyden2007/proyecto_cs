using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedades.domain.models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace proyecto_cs;
public class CatalogTemplate : IDocument
{
    private static readonly Dictionary<string, byte[]> _imageCache = new();

    public void Compose(IDocumentContainer container)
    {
        // Aquí no usamos Compose directamente
        // porque tenemos CompositionCaratula y Tarjetas separados
    }

    public void CompositionCaratula(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(50);
            page.Size(PageSizes.A4);
            page.PageColor(Colors.Green.Lighten1);
            page.DefaultTextStyle(x => x.FontSize(12).FontFamily("verdana"));

            page.Content().AlignCenter().Column(column =>
            {
                column.Item().Text("Catálogo de Variedades de Café Colombiano")
                    .FontColor(Colors.Green.Darken4)
                    .FontSize(30)
                    .ExtraBold()
                    .Underline()
                    .AlignCenter();

                column.Item().PaddingTop(20).Text("Generado automáticamente con QuestPDF")
                    .FontSize(14).Italic();
            });
        });
    }

    public async Task Tarjetas(IContainer component, Variedad variedad,
        Dictionary<int, byte[]> imagenesVariedades)
    {
        component.Background(Colors.Green.Lighten5)
            .CornerRadius(10)
            .Border(1)
            .BorderColor(Colors.Green.Darken2)
            .Padding(20)
            .Column(variedadColumn =>
            {
                // === Imagen + Nombre ===
                variedadColumn.Item().Row(row =>
                {
                    if (imagenesVariedades.TryGetValue(variedad.IdVariedad, out var imagenBytes))
                    {
                        row.RelativeItem(1).AlignCenter().Image(imagenBytes).FitArea();
                    }
                    else
                    {
                        row.RelativeItem(1).Height(100).Width(100)
                            .Column(col =>
                            {
                                col.Item().Image(Placeholders.Image);
                                col.Item().Text("Imagen no disponible").FontSize(8);
                            });
                    }

                    row.RelativeItem(2).AlignMiddle().Text(
                        $"{variedad.NombreComun} ({variedad.NombreCientifico})") 
                        .ExtraBold().FontSize(20).FontColor(Colors.Green.Darken4);
                });

                // === Tabla detalles básicos ===
                variedadColumn.Item().PaddingTop(10).Table(table =>
                {
                    table.ColumnsDefinition(c =>
                    {
                        c.RelativeColumn();
                        c.RelativeColumn();
                    });

                    table.Cell().Text("Potencial de rendimiento:").SemiBold();
                    table.Cell().Text(variedad.Rendimiento?.Nivel ?? "N/A");

                    table.Cell().Text("Calidad de grano:").SemiBold();
                    table.Cell().Text(variedad.CalidadAltitud?.Nivel ?? "N/A"); 

                    if (variedad.AtributosAgronomicos != null)
                    {
                        table.Cell().Text("Tiempo de cosecha:").SemiBold();
                        table.Cell().Text(variedad.AtributosAgronomicos.IsReadOnly. ?? "N/A");

                        table.Cell().Text("Maduración:").SemiBold();
                        table.Cell().Text(variedad.InformacionAgronomica.maduracion ?? "N/A");
                    }
                });

                // === Resistencias ===
                variedadColumn.Item().PaddingTop(5).Text("Resistencias:").SemiBold();
                if (variedad.VariedadesResistencia?.Any() == true)
                {
                    foreach (var vr in variedad.VariedadesResistencia)
                    {
                        variedadColumn.Item().Text(
                            $"- {vr.TipoResistencia?.nombre_tipo ?? "Tipo desconocido"}: " +
                            $"{vr.NivelResistencia?.nombre_nivel ?? "Nivel desconocido"}");
                    }
                }
                else
                {
                    variedadColumn.Item().Text("No hay resistencias registradas.");
                }

                // === Descripción ===
                variedadColumn.Item().PaddingTop(5).Text("Descripción:").SemiBold();
                variedadColumn.Item().Text(variedad.descripcion_general ?? "Sin descripción.");
            });
    }

    public static async Task<byte[]> LoadImageFromUrl(string url)
    {
        if (_imageCache.TryGetValue(url, out var cachedImage))
            return cachedImage;

        using var httpClient = new HttpClient();
        var imageBytes = await httpClient.GetByteArrayAsync(url);
        _imageCache[url] = imageBytes;

        return imageBytes;
    }
}
