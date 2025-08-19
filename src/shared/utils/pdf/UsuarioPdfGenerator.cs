using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using proyecto_cs.src.modules.usuarios.domain.models;

namespace proyecto_cs.src.shared.utils.pdf
{
    public class UsuarioPdfGenerator
    {
        private readonly IEnumerable<Usuario> _usuarios;

        public UsuarioPdfGenerator(IEnumerable<Usuario> usuarios)
        {
            _usuarios = usuarios;
        }

        public void GenerarPdf(string filePath)
        {
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
                            .FontSize(18).Bold().FontColor(Colors.Green.Darken2);
                        row.ConstantItem(50).Height(40).Image(Placeholders.Image(50, 40));
                    });

                    // Contenido
                    page.Content().PaddingVertical(15).Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text($"Total de Usuarios: {_usuarios.Count()}")
                            .Bold().FontSize(12).FontColor(Colors.Grey.Darken1);

                        // Tabla de usuarios
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
                            foreach (var usuario in _usuarios)
                            {
                                table.Cell().Text(usuario.Id.ToString());
                                table.Cell().Text(usuario.Nombre ?? "-");
                                table.Cell().Text(usuario.Apellido ?? "-");
                                table.Cell().Text(usuario.Email ?? "-");
                                table.Cell().Text(usuario.Persona?.DocumentoIdentidad.ToString() ?? "-");
                                table.Cell().Text(usuario.CreatedAt.ToString("dd/MM/yyyy"));
                            }
                        });
                    });

                    // Pie de página
                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado automáticamente con QuestPDF | ").FontSize(9);
                        txt.Span($"{DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9).Italic();
                    });
                });
            }).GeneratePdf(filePath);
        }
    }
}
