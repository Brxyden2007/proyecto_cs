using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using proyecto_cs.src.modules.administradores.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace proyecto_cs.src.shared.utils.pdf;
public class ReporteAdminPdfGenerator
{
    public ReporteAdminPdfGenerator() {}    
    public Task Compose(AppDbContext context)
    {
        var administradores = context.Administradors
            .Include(a => a.Persona)
            .ToArray();


        if (!administradores.Any())
            throw new Exception("No hay administradores registrados en la base de datos.");

        QuestPDF.Settings.License = LicenseType.Community;
        Document.Create(container =>
        {
            container.Page(page =>
            {
                // Configuración de la página
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(11));

                // Encabezado
                page.Header().Row(row =>
                {
                    row.RelativeItem().Text("Reporte de Administradores")
                        .FontSize(18).Bold().FontColor(Colors.Blue.Darken2);
                    row.ConstantItem(50).Height(40).Image(Placeholders.Image(50, 40));
                });

                // Contenido
                page.Content().PaddingVertical(15).Column(column =>
                {
                    column.Spacing(10);

                    column.Item().Text($"Total de Administradores: {administradores.Count()}")
                        .Bold().FontSize(12).FontColor(Colors.Grey.Darken1);

                    // Tabla de administradores
                    column.Item().Table(table =>
                    {
                        // Definir columnas
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1); // ID
                            columns.RelativeColumn(2); // Nombre
                            columns.RelativeColumn(2); // Apellido
                            columns.RelativeColumn(3); // Email
                            columns.RelativeColumn(3); // Persona (Documento)
                            columns.RelativeColumn(3); // Fecha creación
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Text("ID").Bold();
                            header.Cell().Text("Nombre").Bold();
                            header.Cell().Text("Apellido").Bold();
                            header.Cell().Text("Email").Bold();
                            header.Cell().Text("Documento").Bold();
                            header.Cell().Text("Creado").Bold();
                        });

                        // Filas dinámicas
                        foreach (var admin in administradores)
                        {
                            table.Cell().Text(admin.Id.ToString());
                            table.Cell().Text(admin.Nombre ?? "-");
                            table.Cell().Text(admin.Apellido ?? "-");
                            table.Cell().Text(admin.Email ?? "-");
                            table.Cell().Text(admin.Persona?.DocumentoIdentidad.ToString() ?? "-");
                            table.Cell().Text(admin.CreatedAt.ToString("dd/MM/yyyy"));
                        }
                    });
                });

                // Pie de página
                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Generado automáticamente con QuestPDF | ").FontSize(9);
                    txt.Span($"{System.DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9).Italic();
                });
            });
        })
        .GeneratePdf("administradores.pdf");
        return Task.CompletedTask;
    }
}
