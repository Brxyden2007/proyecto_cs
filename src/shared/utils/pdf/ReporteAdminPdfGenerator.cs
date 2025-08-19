using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.usuarios.domain.models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace proyecto_cs.src.shared.utils.pdf; // Corregido namespace

public class ReporteAdminPdfGenerator
{
    private readonly IEnumerable<Usuario> usuarios;

    public ReporteAdminPdfGenerator(IEnumerable<Usuario> usuarios)
    {
        this.usuarios = usuarios;
    }

    public byte[] Generate()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                
                page.Header().Padding(20).Column(col =>
                {
                    col.Item().Text("Reporte Administrativo - Usuarios")
                        .Style(TextStyle.Default.FontSize(18).Bold().FontColor(Colors.Green.Darken2));
                    col.Item().Text($"Total: {usuarios.Count()} usuarios | {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(11).FontColor(Colors.Grey.Darken1);
                });

                page.Content().Padding(20).Column(column =>
                {
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(40);  // ID - reducido
                            cols.RelativeColumn(2);   // Nombre - reducido
                            cols.RelativeColumn(3);   // Email - reducido
                            cols.RelativeColumn(1);   // Fecha - reducido
                        });

                        // Encabezado simplificado
                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Green.Lighten2).Padding(5).Text("ID").Bold().FontSize(11);
                            header.Cell().Background(Colors.Green.Lighten2).Padding(5).Text("Nombre").Bold().FontSize(11);
                            header.Cell().Background(Colors.Green.Lighten2).Padding(5).Text("Email").Bold().FontSize(11);
                            header.Cell().Background(Colors.Green.Lighten2).Padding(5).Text("Fecha").Bold().FontSize(11);
                        });

                        foreach (var u in usuarios)
                        {
                            table.Cell().Border(0.5f).Padding(5).Text(u.Id.ToString()).FontSize(9);
                            table.Cell().Border(0.5f).Padding(5).Text($"{u.Nombre} {u.Apellido}").FontSize(9);
                            table.Cell().Border(0.5f).Padding(5).Text(u.Email).FontSize(9);
                            table.Cell().Border(0.5f).Padding(5).Text(u.CreatedAt.ToString("dd/MM/yy")).FontSize(9);
                        }
                    });
                });

                // Simplificado el footer
                page.Footer().Padding(10).AlignCenter()
                    .Text("Sistema de GestiÃ³n - Reporte de Usuarios").FontSize(9);
            });
        }).GeneratePdf();
    }
}