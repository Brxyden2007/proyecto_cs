using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.usuarios.domain.models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace proyecto_cs;
public class ReporteAdminPdfGenerator
{
    private readonly IEnumerable<Usuario> usuarios;

    public ReporteAdminPdfGenerator(IEnumerable<Usuario> usuarios)
    {
        this.usuarios = usuarios;
    }

    public byte[] Generate()
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Header().Row(row =>
                {
                    row.RelativeItem().Text("Reporte Administrativo - Usuarios")
                        .Style(TextStyle.Default.FontSize(20).Bold().FontColor(Colors.Green.Darken2));
                    row.RelativeItem(60).Height(40).Background(Colors.Green.Lighten2);
                });

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(cols =>
                    {
                        cols.RelativeColumn(1); // ID
                        cols.RelativeColumn(2); // Nombre
                        cols.RelativeColumn(3); // Email
                        cols.RelativeColumn(2); // Fecha creación
                    });

                    // Encabezado
                    table.Header(header =>
                    {
                        header.Cell().Text("ID").Bold();
                        header.Cell().Text("Nombre").Bold();
                        header.Cell().Text("Email").Bold();
                        header.Cell().Text("Creado en").Bold();
                    });

                    // Filas
                    foreach (var u in usuarios)
                    {
                        table.Cell().Text(u.Id.ToString());
                        table.Cell().Text($"{u.Nombre} {u.Apellido}");
                        table.Cell().Text(u.Email);
                        table.Cell().Text(u.CreatedAt.ToString("dd/MM/yyyy"));
                    }
                });
            });
        }).GeneratePdf();
    }
}