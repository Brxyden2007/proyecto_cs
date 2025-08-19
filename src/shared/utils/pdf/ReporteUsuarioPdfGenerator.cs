using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace proyecto_cs.src.shared.utils.pdf;
public class ReporteUsuarioPdfGenerator
{
    public ReporteUsuarioPdfGenerator() { }

    public Task Compose(AppDbContext context)
    {
        var usuarios = context.Usuarios
            .Include(u => u.Persona) // si existe relación
            .ToArray();

        if (!usuarios.Any())
            throw new Exception("No hay usuarios registrados en la base de datos.");

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
                    row.RelativeItem().Text("Reporte de Usuarios")
                        .FontSize(18).Bold().FontColor(Colors.Blue.Darken2);
                    row.ConstantItem(50).Height(40).Image(Placeholders.Image(50, 40));
                });

                // Contenido
                page.Content().PaddingVertical(15).Column(column =>
                {
                    column.Spacing(10);

                    column.Item().Text($"Total de Usuarios: {usuarios.Count()}")
                        .Bold().FontSize(12).FontColor(Colors.Grey.Darken1);

                    // Tabla de usuarios
                    column.Item().Table(table =>
                    {
                        // Definir columnas
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1); // ID
                            columns.RelativeColumn(2); // Nombre
                            columns.RelativeColumn(3); // Email
                            columns.RelativeColumn(2); // Rol
                            columns.RelativeColumn(2); // Estado
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Text("ID").Bold();
                            header.Cell().Text("Nombre").Bold();
                            header.Cell().Text("Email").Bold();
                        });

                        // Filas dinámicas
                        foreach (var user in usuarios)
                        {
                            table.Cell().Text(user.Id.ToString());
                            table.Cell().Text(user.Persona != null 
                                ? $"{user.Persona.Nombre} {user.Persona.Apellido}" 
                                : "-");
                            table.Cell().Text(user.Email ?? "-");
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
        .GeneratePdf("usuarios.pdf");
        return Task.CompletedTask;
    }
}
