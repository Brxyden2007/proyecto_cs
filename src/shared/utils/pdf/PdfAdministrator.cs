using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedades.infrastructure.repository;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace proyecto_cs;    
public class PdfAdministrator
{
    private static readonly ConcurrentDictionary<string, byte[]> _imageCache = new();

    public static async Task GenerateCatalogPdf(AppDbContext context)
    {
        try
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var repo = new VariedadRepository(context);
            var variedades = await repo.GetAllAsync();

            // Pre-cargar imágenes
            var imagenesVariedades = new Dictionary<int, byte[]>();
            foreach (var variedad in variedades)
            {
                if (!string.IsNullOrEmpty(variedad?.ImagenUrl))
                {
                    try
                    {
                        var imageBytes = await CatalogTemplate.LoadImageFromUrl(variedad.imagen_referencia_url);
                        imagenesVariedades[variedad.id] = imageBytes;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error cargando imagen para {variedad.nombre_comun}: {ex.Message}");
                    }
                }
            }

            // Crear documento
            Document.Create(container =>
            {
                // Carátula
                new CatalogTemplate().CompositionCaratula(container);

                // Páginas del catálogo
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("verdana"));

                    page.Header()
                        .AlignCenter()
                        .Text("Catálogo de Variedades de Café Colombiano")
                        .ExtraBold()
                        .FontSize(20)
                        .FontColor(Colors.Green.Darken2);

                    page.Content().Column(column =>
                    {
                        column.Spacing(15);

                        foreach (var variedad in variedades)
                        {
                            new CatalogTemplate().Tarjetas(column.Item(), variedad, imagenesVariedades).Wait();
                        }
                    });

                    page.Footer()
                        .AlignRight()
                        .Text(x =>
                        {
                            x.Span("Pg: ");
                            x.CurrentPageNumber();
                        });
                });
            }).GeneratePdf("catalogo_cafe_colombiano.pdf");

            Console.WriteLine("✅ PDF generado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error generando PDF: {ex.Message}");
            throw;
        }
    }
}