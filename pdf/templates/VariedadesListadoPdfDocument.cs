using proyecto_cs;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace proyecto_cs;
public class VariedadesListadoPdfDocument : IDocument
{
    private readonly IEnumerable<Variedad> _variedades;

    public VariedadesListadoPdfDocument(IEnumerable<Variedad> variedades)
    {
        _variedades = variedades;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(20);
            page.Header().Text("Catálogo de Variedades").FontSize(20).Bold();

            page.Content().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(150); // Nombre
                    columns.ConstantColumn(100); // Altitud
                    columns.RelativeColumn();    // Otra info
                });

                table.Header(header =>
                {
                    header.Cell().Text("Nombre").Bold();
                    header.Cell().Text("Altitud").Bold();
                    header.Cell().Text("Porte").Bold();
                });

                foreach (var v in _variedades)
                {
                    table.Cell().Text(v.NombreComun);
                    table.Cell().Text(v.Altitud.ToString());
                    table.Cell().Text(v.Porte.Nombre);
                }
            });
        });
    }
}
